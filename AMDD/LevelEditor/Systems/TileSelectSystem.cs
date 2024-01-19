using AMDD.ECS;
using AMDD.ECS.Components.UI;
using AMDD.LevelEditor.Components;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AMDD.LevelEditor.Systems;

public class TileSelectSystem : ECS.System
{
	public override Type[] RequiredComponents { get; }

	private ContentManager _contentManager;
	public SceneEntity addressDisplay;

	private List<DirectoryInfo> _directories;

	private int _selectedDirectory = 0;
	private int _selectedFile = 1;

	private string _address;
	private Texture2D _tileTexture;

	private bool _switchedDirLastFrame;
	private bool _switchedFileLastFrame;

	public TileSelectSystem()
	{
		_contentManager = GameData.contentManager;
		DirectoryInfo dir = new DirectoryInfo(_contentManager.RootDirectory + "/Sprites/Tiles");
		_directories = dir.GetDirectories().ToList();
		RecalculateFiles();
	}

	public override void Update(EntityMap entities, float deltaTime)
	{
		if (addressDisplay == null)
		{
			addressDisplay = new SceneEntity();
			addressDisplay.AddComponent(new UIText());
			addressDisplay.position.position = new Microsoft.Xna.Framework.Vector2(0, 0);
			entities.AddNewEntity(addressDisplay);
		}
		KeyboardState keyboardState = Keyboard.GetState();
		if (keyboardState.IsKeyDown(Keys.D1))
			_switchedDirLastFrame = true;
		else if (_switchedDirLastFrame)
		{
			_switchedDirLastFrame = false;
			_selectedDirectory += 1;
			RecalculateFiles();
		}
		if (keyboardState.IsKeyDown(Keys.D2))
			_switchedFileLastFrame = true;
		else if (_switchedFileLastFrame)
		{
			_switchedFileLastFrame = false;
			_selectedFile += 1;
			RecalculateFiles();
		}

		addressDisplay.GetComponent<UIText>().text = _address;

		foreach (Entity entity in entities.GetEntitiesWithComponents(typeof(LevelPainter)))
		{
			LevelPainter painter = entity.GetComponent<LevelPainter>();
			painter.currentTexture = _tileTexture;
			//Debug.WriteLine(_address);
			painter.address = _address;
		}
	}

	private void RecalculateFiles()
	{
		_selectedDirectory %= _directories.Count;
		List<string> addresses = new DirectoryInfo(_directories[_selectedDirectory].FullName).GetFiles("*.xnb")
											.Select(f => f.FullName).Select(Path.GetFileName)
											.Select(f => String.Concat(f.Reverse().Skip(4).Reverse())).ToList();

		_selectedFile %= addresses.Count;
		_address = "Sprites/Tiles/" + _directories[_selectedDirectory].Name + "/" + addresses[_selectedFile];
		Debug.WriteLine("Sprites/Tiles/" + _directories[_selectedDirectory].Name + "/" + addresses[_selectedFile]);

		_tileTexture = _contentManager.Load<Texture2D>(_address);
	}
}
