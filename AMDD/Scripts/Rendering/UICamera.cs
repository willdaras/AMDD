using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AMDD.ECS.Components;
using AMDD.ECS;
using AMDD.ECS.Components.UI;

namespace AMDD.Rendering;

public class UICamera : Camera
{
	public ECS.Components.Camera camera;

	public override Rectangle bounds => new Rectangle((int)MathF.Round(position.position.X - (camera.size / 2).X), (int)MathF.Round(position.position.Y - (camera.size / 2).Y), camera.width, camera.height);

	private SpriteFont _font;

	public UICamera(Vector2 size)
	{
		camera = new ECS.Components.Camera();
		camera.width = (int)size.X;
		camera.height = (int)size.Y;
		AddComponent(camera);
		_font = GameData.contentManager.Load<SpriteFont>("Sprites/Engine/Font");
	}
	public UICamera(int width, int height)
	{
		camera = new ECS.Components.Camera();
		camera.width = width;
		camera.height = height;
		AddComponent(camera);
		_font = GameData.contentManager.Load<SpriteFont>("Sprites/Engine/Font");
	}

	public UICamera() { }

	public override void Draw(SpriteBatch spriteBatch, List<Entity> entitiesToDraw)
	{
		foreach (Entity entity in entitiesToDraw)
		{
			DrawEntity(entity, spriteBatch);
		}
	}

	private void DrawEntity(Entity entity, SpriteBatch spriteBatch)
	{
		UIText sprite = entity.GetComponent<UIText>();
		Position position = entity.GetComponent<Position>();
		spriteBatch.DrawString(_font, sprite.text, position.position, Color.White);
	}
}