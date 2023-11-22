using AMDD.ECS.Components;
using System;

namespace AMDD.ECS.Systems;

public class BulletSystem : System
{
	public override Type[] RequiredComponents { get; } = new Type[] { typeof(Bullet), typeof(Physics), typeof(Collider) };

	public override void Update(EntityMap entities, float deltaTime)
	{
		foreach (SceneEntity entity in entities.GetEntitiesWithComponents(RequiredComponents))
		{
			Collider collider = entity.GetComponent<Collider>();
			if (collider.collidingWith.enabled)
			{
				DamageEntity(entity);
				entities.instantiationBuffer.entitiesToDestroy.Add(entity);
			}
		}
	}

	private void DamageEntity(Entity entity)
	{
		
	}
}
