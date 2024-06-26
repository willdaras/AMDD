using AMDD.ECS;
using AMDD.ECS.Systems;
using AMDD.ObjectCreation;
using Microsoft.Xna.Framework.Graphics;

namespace AMDD.ObjectCreation.Scenes;

/// <summary>
/// Constructs a gameplay scene.
/// </summary>
public class GameSceneConstructor : ISceneConstructor
{
	public Scene ConstructScene()
	{
		Scene scene = new BaseSceneConstructor().ConstructScene();
		scene.preDrawSystems.Add(new CamFollowPlayerSystem());
		//scene.entityMap.AddNewEntity(new PlayerConstructor().ConstructObject());

		return scene;
	}
}