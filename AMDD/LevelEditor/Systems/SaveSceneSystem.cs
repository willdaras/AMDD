using AMDD.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using AMDD.ECS;
using AMDD.SaveSystem.Loading;
using AMDD.SaveSystem.Storage;
using System.Net.Http.Headers;
using System.Diagnostics;
using AMDD.Rendering;
using AMDD.ECS.Components.UI;

namespace AMDD.LevelEditor.Systems;

/// <summary>
/// A System to save the scene to a file.
/// </summary>
/// <remarks> Currently saves to the file test_editor_level.json. </remarks>
public class SaveSceneSystem : ECS.System
{
	public override Type[] RequiredComponents { get; }

	private ILevelLoader _levelLoader;
	private ISaveInterpreter _saveInterpreter;

	private bool _savePressed;

	private bool _loadPressed;

	public SaveSceneSystem()
	{
		_levelLoader = new FileLevelLoader(GameData.contentManager);
		_saveInterpreter = new SaveInterpreter(_levelLoader);
	}

	public override void Update(EntityMap entities, float deltaTime)
	{
		if (Keyboard.GetState().IsKeyDown(Keys.D0))
			_savePressed = true;
		else if (_savePressed)
		{
			_savePressed = false;
			Save(entities);
		}
		if (Keyboard.GetState().IsKeyDown(Keys.D9))
			_loadPressed = true;
		else if (_loadPressed)
		{
			_loadPressed = false;
			Load(entities);
		}
	}

	/// <summary>
	/// Saves the EntityMap to the file test_editor_level.json.
	/// </summary>
	/// <param name="entities"> The EntityMap to save. </param>
	public void Save(EntityMap entities)
	{
		Debug.WriteLine("Attempting to save");
		entities = SaveProcessor.ProcessSaveForSerialization(entities);
		Debug.WriteLine("Save processed");
		_saveInterpreter.Write("test_editor_level", entities);
	}
	/// <summary>
	/// Loads the level test_editor_level.json to a new Scene and sets it to the active scene.
	/// </summary>
	/// <param name="entities"></param>
	public void Load(EntityMap entities)
	{
		Debug.WriteLine("Loading Level");
		EntityMap loaded = _saveInterpreter.Interpret("test_editor_level");
		loaded = SaveProcessor.ProcessSaveForDeserialization(loaded);
		bool editor = Keyboard.GetState().IsKeyDown(Keys.D8);
		Scene loadedScene = editor ? new LevelEditorSceneConstructor().ConstructScene() : new ObjectCreation.Scenes.GameSceneConstructor().ConstructScene();
		loadedScene.entityMap = loaded;
		loadedScene.entityMap.AddNewEntity(new ObjectCreation.PlayerConstructor().ConstructObject());
		foreach (Entity entity in loadedScene.entityMap)
		{
			if (entity is BasicCameraStack)
			{
				((BasicCameraStack)entity).SetActive();
			}
		}
		Scene.SetActiveScene(loadedScene);
		Debug.WriteLine(Rendering.Camera.activeCamera);
	}
}