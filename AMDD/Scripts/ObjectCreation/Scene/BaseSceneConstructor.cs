using System.Collections.Generic;
using AMDD.ECS.Systems;
using AMDD.Rendering;

namespace AMDD.ObjectCreation.Scenes;

public class BaseSceneConstructor : ISceneConstructor
{
	public Scene ConstructScene()
	{
		Scene scene = new Scene();

		scene.systems.Add(new InstantiationSystem());
		scene.systems.Add(new InputSystem());
		scene.systems.Add(new GravitySystem());
		scene.systems.Add(new PlayerControllerSystem());
		scene.systems.Add(new PlayerAnimationSystem());
		scene.systems.Add(new BulletSystem());
		scene.systems.Add(new AnimationSystem());
		scene.systems.Add(new HealthSystem());
		scene.systems.Add(new DestroyOnHealthZeroSystem());
		//scene.preDrawSystems.Add(new CamFollowPlayerSystem());

		Camera camera = new BasicCameraStack(GameData.screenX, GameData.screenY)
		{
			stack = new List<Camera>() 
			{
				new BasicLayerCamera(GameData.screenX, GameData.screenY)
				{
					renderLayers = ECS.Components.Sprite.Layer.Background
				},
				new BasicLayerCamera(GameData.screenX, GameData.screenY) 
				{
					renderLayers = ECS.Components.Sprite.Layer.Default | ECS.Components.Sprite.Layer.Sprites
				},
				new BasicLayerCamera(GameData.screenX, GameData.screenY)
				{
					renderLayers = ECS.Components.Sprite.Layer.Foreground
				},
				new BasicLayerCamera(GameData.screenX, GameData.screenY)
				{
					renderLayers = ECS.Components.Sprite.Layer.UI
				}
			}
		};
		camera.SetActive();
		scene.entityMap.AddNewEntity(camera);

		return scene;
	}
}