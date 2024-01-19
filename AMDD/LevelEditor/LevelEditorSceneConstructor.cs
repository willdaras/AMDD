using AMDD.ECS;
using AMDD.ObjectCreation;
using Microsoft.Xna.Framework.Graphics;

namespace AMDD.LevelEditor;

public class LevelEditorSceneConstructor : Scene
{
	public Scene ConstructScene()
	{
		Scene scene = new ObjectCreation.Scenes.BaseSceneConstructor().ConstructScene();
		scene.preDrawSystems.Add(new Systems.MouseMoveCamSystem());
		scene.systems.Add(new Systems.TilePaintSystem());
		scene.systems.Add(new Systems.TileSelectSystem());
		//scene.systems.Add(new Systems.TileSelectSystem());
		scene.systems.Add(new Systems.SaveSceneSystem());
		scene.entityMap.AddNewEntity(new PlayerConstructor().ConstructObject());

		return scene;
	}
}