using AMDD.ECS.Components;
using System;
using System.Diagnostics;

namespace AMDD.ECS.Systems;

/// <summary>
/// Destroys objects with 0 or less health by adding them to the InstantiationBuffer destroy list.
/// </summary>
public class DestroyOnHealthZeroSystem : System
{
	public override Type[] RequiredComponents { get; } = new Type[] { typeof(Health) };

	public override void Update(EntityMap entities, float deltaTime)
	{
		foreach (SceneEntity entity in entities.GetEntitiesWithComponents(RequiredComponents))
		{
			if (entity.GetComponent<Health>().health <= 0)
			{
				entities.instantiationBuffer.entitiesToDestroy.Add(entity);
			}
		}
	}
}
