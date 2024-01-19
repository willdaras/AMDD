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

		Entity entity = new ManagerEntity();
		entity.AddComponent(new Components.LevelPainter() { currentTexture = GameData.contentManager.Load<Texture2D>("Sprites/Tiles/basic_ground_tiles/basic_ground_tile1") });
		scene.entityMap.AddNewEntity(entity);

		return scene;
	}
}