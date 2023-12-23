using AMDD.ECS.Components;
using System;
using System.Diagnostics;

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
				DamageEntity(collider.collidingWith.value, entity.GetComponent<Bullet>());
				entities.instantiationBuffer.entitiesToDestroy.Add(entity);
			}
		}
	}

	private void DamageEntity(Entity entity, Bullet bullet)
	{
		if (entity.TryGetComponent(out DamageBuffer damageBuffer))
		{
			Debug.WriteLine($"damaging {entity} for {bullet.damage} damage");
			damageBuffer.damageBuffer += bullet.damage;
		}
	}
}
