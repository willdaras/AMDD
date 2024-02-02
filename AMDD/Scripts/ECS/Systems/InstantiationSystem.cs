using AMDD.ECS.Components;
using System;
using System.Collections.Generic;

namespace AMDD.ECS.Systems;

/// <summary>
/// A system to instantiate SceneEntities, instantiates all entities in the EntityMap's InstantiationBuffer.
/// </summary>
public class InstantiationSystem : System
{
	public override Type[] RequiredComponents { get; } = new Type[] { typeof(InstantiationBuffer) };

	public override void Update(EntityMap entities, float deltaTime)
	{
		foreach (SceneEntity sceneEntity in entities.instantiationBuffer.entitiesToInstantiate)
		{
			entities.AddNewEntity(sceneEntity);
		}
		entities.instantiationBuffer.entitiesToInstantiate = new List<SceneEntity>();
		foreach (SceneEntity entity in entities.instantiationBuffer.entitiesToDestroy)
		{
			entities.RemoveEntity(entity);
		}
		entities.instantiationBuffer.entitiesToDestroy = new List<SceneEntity>();
	}
}
