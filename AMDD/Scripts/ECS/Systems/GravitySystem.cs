using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using AMDD.ECS.Components;

namespace AMDD.ECS.Systems;

/// <summary>
/// Applies gravity to all objects with a Gravity component.
/// </summary>
public class GravitySystem : System
{
	/// <summary>
	/// The strength of gravity.
	/// </summary>
	public const float Gravity = 98f;

	public override Type[] RequiredComponents { get; } = new Type[] { typeof(Physics), typeof(Gravity) };

	public override void Update(EntityMap entities, float deltaTime)
	{
		foreach (Entity entity in entities.GetEntitiesWithComponents(RequiredComponents))
		{
			if (entity.TryGetComponent(out Grounded grounded))
			{
				//if (grounded.grounded) { continue; }
			}
			Physics physics = entity.GetComponent<Physics>();
			physics.acceleration += new Vector2(0, Gravity) * entity.GetComponent<Gravity>().gravityScale;
		}
	}
}