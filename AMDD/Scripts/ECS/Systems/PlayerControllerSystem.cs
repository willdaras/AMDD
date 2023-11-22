using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using AMDD.ECS.Components;
using System.Diagnostics;
using AMDD.ObjectPooling;

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

	private ObjectPool _bulletPool = new ObjectPool(new BulletConstructor(), 20);

	public override Type[] RequiredComponents { get; } = new Type[] { typeof(Physics), typeof(Components.Input), typeof(Grounded), typeof(Player) };

	public override void Update(EntityMap entities, float deltaTime)
	{
		foreach (Entity entity in entities.GetEntitiesWithComponents(RequiredComponents))
		{
			Components.Input input = entity.GetComponent<Components.Input>();
			Physics physics = entity.GetComponent<Physics>();
			Grounded grounded = entity.GetComponent<Grounded>();

			MovePlayer(entity, deltaTime, input, physics);
			Jump(entity, deltaTime, input, physics, grounded);
			Shoot(entity, entities, input, grounded);
		}
	}

	public void MovePlayer(Entity player, float deltatime, Components.Input input, Physics physics)
	{
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
	public void Jump(Entity player, float deltatime, Components.Input input, Physics physics, Grounded grounded)
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
	public void Shoot(Entity player, EntityMap entities, Components.Input input, Grounded grounded)
	{
		if (!input.shooting) return;

		Collider collider = player.GetComponent<Collider>();
		FacingDirection dir = player.GetComponent<FacingDirection>();

		Vector2 aimDir = input.aimDir;
		if (aimDir == Vector2.Zero || (grounded.grounded && aimDir == new Vector2(0, 1)))
		{
			aimDir = new Vector2(dir.facingRight ? 1 : -1, 0);
		}
		aimDir.Normalize();

		Vector2 spawnPos = new Vector2();
		spawnPos.FromPoint(collider.collider.Location);
		spawnPos.Y = collider.collider.Top + 6;
		spawnPos.X = dir.facingRight ? collider.collider.Right + 4 : collider.collider.Left - 6;


		SceneEntity entity = _bulletPool.GetEntity(spawnPos);


		entity.GetComponent<Physics>().velocity = aimDir * 400;

		entities.instantiationBuffer.entitiesToInstantiate.Add(entity);
	}
}