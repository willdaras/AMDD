using AMDD.ECS.Components;
using System;
using System.Diagnostics;

namespace AMDD.ECS.Systems;

/// <summary>
/// Applies damage from an Entity's DamageBuffer to its health, also handles invincibility.
/// </summary>
public class HealthSystem : System
{
	public override Type[] RequiredComponents { get; } = new Type[] { typeof(Health), typeof(DamageBuffer) };

	public override void Update(EntityMap entities, float deltaTime)
	{
		foreach (Entity entity in entities.GetEntitiesWithComponents(RequiredComponents))
		{
			Health health = entity.GetComponent<Health>();
			DamageBuffer damageBuffer = entity.GetComponent<DamageBuffer>();

			health.iFrameCount++;

			if (health.invincible) { continue; }

			health.health -= damageBuffer.damageBuffer;
			damageBuffer.damageBuffer = 0;
			health.iFrameCount = 0;

			if (health.health <= 0)
			{
				Debug.WriteLine($"{entity} health is 0");
			}
		}
	}
}