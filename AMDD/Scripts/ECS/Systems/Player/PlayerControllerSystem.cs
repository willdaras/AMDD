using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using AMDD.ECS.Components;
using System.Diagnostics;
using AMDD.ObjectCreation;

namespace AMDD.ECS.Systems;

/// <summary>
/// Handles player movement.
/// </summary>
public class PlayerControllerSystem : System
{
	/// <summary>
	/// The player's speed.
	/// </summary>
	public float speed = 500;
	/// <summary>
	/// The scale of extra drag to apply to the player when not pressing a key - improves feel.
	/// </summary>
	public float dragScale = 3;

	private bool _prevJumpValue = false;
	private bool _jumpStarted = false;
	/// <summary>
	/// The initial force of the jump - for shorter jumps.
	/// </summary>
	public float initialJumpForce = 10000;
	/// <summary>
	/// The sustained force while holding jump - allows for variable jump height.
	/// </summary>
	public float jumpForce = 800;
	/// <summary>
	/// The maximum time the player can jump for.
	/// </summary>
	public float jumpTime = 0.2f;
	private float _jumpTimer = 0;
	private bool _jumping => _jumpTimer < jumpTime;

	public override Type[] RequiredComponents { get; } = new Type[] { typeof(Physics), typeof(Components.Input), typeof(Grounded), typeof(Player) };

	public override void Update(EntityMap entities, float deltaTime)
	{
		foreach (Entity entity in entities.GetEntitiesWithComponents(RequiredComponents))
		{
			Components.Input input = entity.GetComponent<Components.Input>();
			Physics physics = entity.GetComponent<Physics>();
			Grounded grounded = entity.GetComponent<Grounded>();
			FacingDirection dir = entity.GetComponent<FacingDirection>();

			MovePlayer(entity, deltaTime, input, physics, dir);
			Jump(entity, deltaTime, input, physics, grounded);
			Shoot(entity, deltaTime, entities, input, physics, grounded, dir);
		}
	}

	private void MovePlayer(Entity player, float deltatime, Components.Input input, Physics physics, FacingDirection dir)
	{
		if (input.moveDir.X == 1 || input.aimDir.X == 1)
		{
			dir.facingRight = true;
		}
		else if (input.moveDir.X == -1 || input.aimDir.X == -1)
		{
			dir.facingRight = false;
		}

		if (input.aiming) { input.moveDir = Vector2.Zero; }

		physics.acceleration += new Vector2(input.moveDir.X * (input.moveDir.X != MathF.Sign(physics.velocity.X) ? 5 : 1), 0) * speed;

		if (input.moveDir.X == 0)
		{
			physics.velocity = new Vector2(physics.velocity.X + MathHelper.Clamp(-MathF.Sign(physics.velocity.X) * dragScale, -MathF.Abs(physics.velocity.X), MathF.Abs(physics.velocity.X)), physics.velocity.Y);
		}
	}
	private void Jump(Entity player, float deltatime, Components.Input input, Physics physics, Grounded grounded)
	{
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
	private void Shoot(Entity player, float deltaTime, EntityMap entities, Components.Input input, Physics physics, Grounded grounded, FacingDirection dir)
	{
		Shooting shooting = player.GetComponent<Shooting>();
		shooting.shootFrameCount++;
		Vector2 aimDir = AimDir(input, shooting, dir.facingRight, grounded.grounded);

		if (!input.shooting) return;

		Collider collider = player.GetComponent<Collider>();

		if (player.GetComponent<Physics>().velocity.X != 0 && aimDir == new Vector2(0, -1))
			aimDir = new Vector2(dir.facingRight ? 1 : -1, -1);

		Vector2 spawnPos = new Vector2(dir.facingRight? collider.collider.Right : collider.collider.Left, collider.collider.Top);
		Vector2 spawnOffset = GetSpawnOffset(aimDir, dir.facingRight, MathF.Round(physics.velocity.X) != 0);
		spawnPos += spawnOffset;

		aimDir.Normalize();

		SceneEntity entity = shooting.bulletPool.GetEntity(spawnPos);

		entity.GetComponent<Physics>().velocity = aimDir * 400;

		entities.instantiationBuffer.entitiesToInstantiate.Add(entity);

		shooting.shootFrameCount = 0;
	}

	private Vector2 AimDir(Components.Input input, Shooting shooting, bool facingRight, bool grounded)
	{
		Vector2 aimDir = input.aimDir;

		if (aimDir == Vector2.Zero)
		{
			aimDir = new Vector2(facingRight ? 1 : -1, 0);
		}
		if (aimDir == new Vector2(0, 1))
		{
			aimDir = new Vector2(facingRight ? 1 : -1, 1);
		}

		if (aimDir == new Vector2(1, 0) || aimDir == new Vector2(-1, 0))
			shooting.shootDir = Shooting.ShootDirections.Forward;
		if (aimDir == new Vector2(1, -1) || aimDir == new Vector2(-1, -1))
			shooting.shootDir = Shooting.ShootDirections.DiagonalUp;
		if (aimDir == new Vector2(1, 1) || aimDir == new Vector2(-1, 1))
			shooting.shootDir = Shooting.ShootDirections.DiagonalDown;
		if (aimDir == new Vector2(0, -1))
			shooting.shootDir = Shooting.ShootDirections.Up;
		if (aimDir == new Vector2(0, 1))
			shooting.shootDir = Shooting.ShootDirections.Down;

		return aimDir;
	}

	private Vector2 GetSpawnOffset(Vector2 aimDir, bool facingRight, bool moving)
	{
		if ((aimDir == new Vector2(1, -1)) || (aimDir == new Vector2(-1, -1)))
			return new Vector2(facingRight ? (moving ? 6 : 2) : (moving ? -6 : -2), 3);
		if ((aimDir == new Vector2(1, 1)) || (aimDir == new Vector2(-1, 1)))
			return new Vector2(facingRight ? (moving ? 5 : 1) : (moving ? -6 : -1), 13);
		if (aimDir == new Vector2(0, -1))
			return new Vector2(facingRight ? -10 : 4, -7);
		if (aimDir == new Vector2(0, 1))
			return new Vector2(facingRight ? -2 : 2, 35);
		return new Vector2(facingRight ? 5 : -6, 6);
	}
}