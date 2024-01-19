using System;
using System.Collections.Generic;
using System.Diagnostics;
using AMDD.ECS;
using AMDD.ECS.Components;
using AMDD.ECS.Components.UI;
using AMDD.LevelEditor.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Formats.Asn1.AsnWriter;

namespace AMDD.LevelEditor.Systems;

public class TilePaintSystem : ECS.System
{
	public override Type[] RequiredComponents { get; }

	private Dictionary<int, Dictionary<Vector2, Entity>> _positions = new Dictionary<int, Dictionary<Vector2, Entity>>
	{
		{ 1, new Dictionary<Vector2, Entity>() },
		{ 2, new Dictionary<Vector2, Entity>() },
		{ 4, new Dictionary<Vector2, Entity>() },
		{ 8, new Dictionary<Vector2, Entity>() }
	};

	private Components.LevelPainter _levelPainter;

	private Texture2D _cursorTexture;
	private SceneEntity _cursor;

	private List<ITileFactory> _tileFactories = new List<ITileFactory>() { new CollisionTileFactory(), new TileFactory() };
	private List<Color> _cursorColors = new List<Color>() { Color.Green, Color.Blue };
	private int _factoryIndex = 0;
	public ITileFactory tileFactory => _tileFactories[_factoryIndex];
	public Color cursorColor => _cursorColors[_factoryIndex];

	private int _prevScrollValue = 0;

	private bool _layerKeyDown;
	private int _layer = 1;
	private SceneEntity _layerDisplay;

	public override void Update(EntityMap entities, float deltaTime)
	{
		if (_levelPainter == null)
		{
			Entity painter = new ManagerEntity();
			painter.AddComponent(new Components.LevelPainter() { currentTexture = GameData.contentManager.Load<Texture2D>("Sprites/Tiles/basic_ground_tiles/basic_ground_tile1") });
			entities.AddNewEntity(painter);

			_levelPainter = entities.GetEntitiesWithComponents(typeof(Components.LevelPainter))[0].GetComponent<Components.LevelPainter>();
			SceneEntity entity = new SceneEntity();
			_cursorTexture = GameData.contentManager.Load<Texture2D>("Sprites/Engine/LevelEditor/Cursor");
			entity.name.name = "Cursor";
			entity.AddComponent(new Sprite(_cursorTexture) { layer = Sprite.Layer.UI, color = cursorColor, address = "Sprites/Engine/LevelEditor/Cursor" });
			entities.AddNewEntity(entity);
			_cursor = entity;

			_layerDisplay = new SceneEntity();
			_layerDisplay.AddComponent(new UIText() { text = ((Sprite.Layer)_layer).ToString() });
			_layerDisplay.position.position = new Vector2(0, 10);
			entities.AddNewEntity(_layerDisplay);

			foreach (Entity tile in entities)
			{
				if (!tile.TryGetComponent(out Name name))
					continue;
				if (name.name != "ColliderTile" && name.name != "Tile")
					continue;
				_positions[(int)tile.GetComponent<Sprite>().layer].Add(tile.GetComponent<Position>().position, tile);
			}
		}

		MouseState state = Mouse.GetState();

		Vector2 position = state.Position.ToVector2() / GameData.upscaleScale;
		position -= new Vector2(GameData.screenX / 2, GameData.screenY / 2);
		position += Rendering.Camera.activeCamera.position.position;
		position.X = MathF.Floor(position.X / 16) * 16;
		position.Y = MathF.Floor(position.Y / 16) * 16;
		//Debug.WriteLine($"position is {position}");

		_cursor.position.position = position;

		if (state.ScrollWheelValue - _prevScrollValue != 0)
		{
			Debug.WriteLine($"index is {_factoryIndex}, difference is {state.ScrollWheelValue - _prevScrollValue}");
			_factoryIndex = (_factoryIndex + 1) % (_tileFactories.Count);
			_cursor.GetComponent<Sprite>().color = cursorColor;
		}
		_prevScrollValue = state.ScrollWheelValue;

		if (state.LeftButton == ButtonState.Pressed)
		{
			if (!_positions[_layer].ContainsKey(position))
			{
				Entity entity = tileFactory.GetTile(position, _levelPainter.currentTexture, _levelPainter.address, _layer);
				Debug.WriteLine($"placed tile at {position}");
				entities.AddNewEntity(entity);
				_positions[_layer].Add(position, entity);
			}
		}
		if (state.RightButton == ButtonState.Pressed)
		{
			if (_positions[_layer].TryGetValue(position, out Entity entity))
			{
				Debug.WriteLine($"removed tile at {position}");
				entities.RemoveEntity(entity);
				_positions[_layer].Remove(position);
			}
		}

		KeyboardState keyboardState = Keyboard.GetState();
		if (keyboardState.IsKeyDown(Keys.D3))
		{
			_layerKeyDown = true;
		}
		else if (_layerKeyDown)
		{
			_layerKeyDown = false;
			_layer *= 2;
			if (_layer >= 16)
			{
				_layer = 1;
			}
			Debug.WriteLine((Sprite.Layer)_layer);
			_layerDisplay.GetComponent<UIText>().text = ((Sprite.Layer)_layer).ToString();
		}
	}
}
