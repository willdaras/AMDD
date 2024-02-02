using AMDD.ECS.Components;
using Microsoft.Xna.Framework;
using System;

namespace AMDD.ECS.Systems;

/// <summary>
/// A System that updates the player's AnimationTree with the current player state.
/// </summary>
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
			Shooting shooting = entity.GetComponent<Shooting>();
			FacingDirection direction = entity.GetComponent<FacingDirection>();
			animated.tree.currentState.Set("xVel", (int)physics.velocity.X);
			animated.tree.currentState.SetBool("grounded", grounded.grounded);
			animated.tree.currentState.SetBool("facingRight", direction.facingRight);
			animated.tree.currentState.SetBool("shooting", shooting.shooting);
			animated.tree.currentState.Set("shootAngle", (int)shooting.shootDir);
		}
	}
}
