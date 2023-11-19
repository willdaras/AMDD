using AMDD.ECS.Components;
using System;

namespace AMDD.ECS.Systems;

public class PlayerAnimationSystem : System
{
	public override Type[] RequiredComponents { get; } = new Type[] { typeof(Player) };

	public override void Update(EntityMap entities, float deltaTime)
	{
		foreach (Entity entity in entities.GetEntitiesWithComponents(RequiredComponents))
		{
			Physics physics = entity.GetComponent<Physics>();
			Animated animated = entity.GetComponent<Animated>();
			Grounded grounded = entity.GetComponent<Grounded>();
			FacingDirection direction = entity.GetComponent<FacingDirection>();
			animated.tree.currentState.Set("xVel", (int)physics.velocity.X);
			animated.tree.currentState.SetBool("grounded", grounded.grounded);
			animated.tree.currentState.SetBool("facingRight", direction.facingRight);
		}
	}
}
