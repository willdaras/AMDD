using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using AMDD.ECS.Components;
using System.Diagnostics;

namespace AMDD.ECS.Systems;

public class PlayerControllerSystem : System
{
	public float speed = 500;
	public float dragScale = 3;

	private bool _prevJumpValue = false;
	private bool _jumpStarted = false;
	public float initialJumpForce = 10000;
	public float jumpForce = 800;
	public float jumpTime = 0.2f;
	private float _jumpTimer = 0;
	private bool _jumping => _jumpTimer < jumpTime;

	public override Type[] RequiredComponents { get; } = new Type[] { typeof(Physics), typeof(Components.Input), typeof(Grounded) };

	public override void Update(EntityMap entities, float deltaTime)
	{
		foreach (Entity entity in entities.GetEntitiesWithComponents(RequiredComponents))
		{
			MovePlayer(entity, deltaTime);
			Jump(entity, deltaTime);
		}
	}

	public void MovePlayer(Entity player, float deltatime)
	{
		Physics physics = player.GetComponent<Physics>();
		Components.Input input = player.GetComponent<Components.Input>();
		if (input.moveDir.X == 1)
		{
			player.GetComponent<FacingDirection>().facingRight = true;
		}
		else if (input.moveDir.X == -1)
		{
			player.GetComponent<FacingDirection>().facingRight = false;
		}

		//if (float.IsNaN(physics.velocity.X)) physics.velocity.X = 0;
		//if (float.IsNaN(physics.velocity.Y)) physics.velocity.Y = 0;

		physics.acceleration += new Vector2(input.moveDir.X * (input.moveDir.X != MathF.Sign(physics.velocity.X) ? 5 : 1), 0) * speed;

		if (input.moveDir.X == 0)
		{
			physics.velocity.X += MathHelper.Clamp(-MathF.Sign(physics.velocity.X) * dragScale, -MathF.Abs(physics.velocity.X), MathF.Abs(physics.velocity.X));
		}
	}
	public void Jump(Entity player, float deltatime)
	{
		Physics physics = player.GetComponent<Physics>();
		Components.Input input = player.GetComponent<Components.Input>();
		Grounded grounded = player.GetComponent<Grounded>();
		//Debug.WriteLine(grounded.grounded);
		if (grounded.grounded && !input.jumping)
		{
			_jumpTimer = 0;
			_jumpStarted = false;
		}
		if (!_prevJumpValue && input.jumping && grounded.grounded)
		{
			_jumpTimer = 0;
			grounded.grounded = false;
			physics.acceleration += new Vector2(0, -1) * initialJumpForce;
			_jumpStarted = true;
		}
		if (input.jumping && _jumping && _jumpStarted)
		{
			physics.acceleration += new Vector2(0, -1) * jumpForce;
			_jumpTimer += deltatime;
		}
		if (_jumping && !input.jumping)
		{
			_jumpTimer = jumpTime + 1;
		}
		_prevJumpValue = input.jumping;
	}
}