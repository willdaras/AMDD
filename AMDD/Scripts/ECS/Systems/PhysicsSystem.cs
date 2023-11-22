using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using AMDD.ECS.Components;
using System.Data;

namespace AMDD.ECS.Systems
{
	/// <summary>
	/// A physics system, updates the position and velocity of entities with the Physics component.
	/// </summary>
	public class PhysicsSystem : System
	{
		public float DragCoefficient { get; set; } = 0.1f;

		public override Type[] RequiredComponents { get; } = new Type[] { typeof(Position), typeof(Physics) };

		public override void Update(EntityMap entities, float deltaTime)
		{
			List<Entity> colliders = entities.GetEntitiesWithComponents(typeof(Collider));

			MoveColliders(colliders);

			foreach (Entity entity in entities.GetEntitiesWithComponents(RequiredComponents))
			{
				if (entity.HasComponent<Static>()) { continue; }
				UpdateEntityPhysics(entity, colliders, deltaTime);
			}
		}

		private void UpdateEntityPhysics(Entity entity, List<Entity> colliders, float deltaTime)
		{
			Physics physics = entity.GetComponent<Physics>();
			Position position = entity.GetComponent<Position>();

			ApplyAcceleration(physics, deltaTime);
			ApplyDrag(physics, deltaTime);

			if (ShouldStopMoving(physics))
			{
				physics.velocity = Vector2.Zero;
			}

			CheckGrounded(entity, physics, colliders);
			HandleGroundedState(entity, physics);

			MoveEntity(entity, colliders, deltaTime);

			physics.acceleration = Vector2.Zero;
		}

		private void ApplyAcceleration(Physics physics, float deltaTime)
		{
			Vector2 acceleration = physics.acceleration / physics.mass;
			physics.velocity += acceleration * deltaTime;
		}

		private void ApplyDrag(Physics physics, float deltaTime)
		{
			Vector2 velocityDir = (physics.velocity != Vector2.Zero) ? Vector2.Normalize(physics.velocity) : physics.velocity;
			Vector2 dragForce = velocityDir * (physics.velocity.LengthSquared() * DragCoefficient * (physics.dragScale / 10) / 2);
			dragForce.X *= physics.xDragScale; dragForce.Y *= physics.yDragScale;
			physics.velocity -= dragForce * deltaTime;
		}

		private bool ShouldStopMoving(Physics physics)
		{
			return physics.velocity.Length() < 0.1;
		}

		private void CheckGrounded(Entity entity, Physics physics, List<Entity> colliders)
		{
			if (!entity.TryGetComponent(out Grounded grounded))
				return;
			Collider collider = entity.GetComponent<Collider>();
			Rectangle groundedCheck = new Rectangle(collider.collider.Location.X, collider.collider.Bottom, collider.collider.Width, 1);
			foreach (Entity other in colliders)
			{
				Collider otherCollider = other.GetComponent<Collider>();
				if (otherCollider.collider.Intersects(groundedCheck))
				{
					grounded.grounded = true;
					return;
				}
				else
				{
					grounded.grounded = false;
				}
			}
		}

		private void HandleGroundedState(Entity entity, Physics physics)
		{
			if (entity.TryGetComponent(out Grounded grounded) && grounded.grounded && physics.velocity.Y > 0)
			{
				//physics.velocity.Y = 0;
			}
		}

		private void MoveEntity(Entity entity, List<Entity> colliders, float deltaTime)
		{
			Position position = entity.GetComponent<Position>();
			Physics physics = entity.GetComponent<Physics>();
			Vector2 velocity = physics.velocity;

			Collider collider = entity.GetComponent<Collider>();
			collider.collidingWith.enabled = false;

			MoveAxis(entity, position, physics, collider, colliders, velocity, deltaTime, true);
			MoveAxis(entity, position, physics, collider, colliders, velocity, deltaTime, false);
		}

		private void MoveAxis(Entity entity, Position position, Physics physics, Collider collider, List<Entity> colliders, Vector2 velocity, float deltaTime, bool isXAxis)
		{
			float axisVelocity = isXAxis ? velocity.X : velocity.Y;
			if (collider.continuous)
			{
				for (int i = 0; i < MathF.Abs(axisVelocity); i++)
				{
					position.position += new Vector2(isXAxis ? MathF.Sign(axisVelocity) : 0, isXAxis ? 0 : MathF.Sign(axisVelocity)) * deltaTime;
					MoveCollider(entity);
					if (CollisionAxis(entity, position, colliders, physics, MathF.Sign(axisVelocity), isXAxis))
						return;
				}
			}
			else
			{
				position.position += new Vector2(isXAxis ? axisVelocity : 0, isXAxis ? 0 : axisVelocity) * deltaTime;
				MoveCollider(entity);
				CollisionAxis(entity, position, colliders, physics, axisVelocity, isXAxis);
			}

		}

		private bool CollisionAxis(Entity entity, Position position, List<Entity> colliders, Physics physics, float axisVelocity, bool isXAxis)
		{
			Collider collider = entity.GetComponent<Collider>();
			foreach (Entity otherEntity in colliders)
			{
				if (otherEntity == entity) { continue; }
				Collider otherCollider = otherEntity.GetComponent<Collider>();
				if (otherCollider.collisionLayer != collider.collisionLayer) { continue; }
				if (collider.collider.Intersects(otherCollider.collider))
				{
					HandleCollisionResponse(entity, position, collider, otherCollider, physics, axisVelocity, isXAxis);
					return true; // No need to continue checking other colliders once collision is detected
				}
			}

			// No collision, update the last valid position
			if (isXAxis)
			{
				collider.lastValidPos.X = position.position.X;
			}
			else
			{
				collider.lastValidPos.Y = position.position.Y;
			}
			return false;
		}

		private void HandleCollisionResponse(Entity entity, Position position, Collider collider, Collider otherCollider, Physics physics, float axisVelocity, bool isXAxis)
		{
			position.position = new Vector2(isXAxis ? ((MathF.Sign(axisVelocity) > 0) ? MathF.Floor(collider.lastValidPos.X) : MathF.Ceiling(collider.lastValidPos.X)) : position.position.X,
				isXAxis ? position.position.Y : ((MathF.Sign(axisVelocity) > 0) ? MathF.Floor(collider.lastValidPos.Y) : MathF.Ceiling(collider.lastValidPos.Y)));

			physics.velocity = isXAxis ? new Vector2(0, physics.velocity.Y) : new Vector2(physics.velocity.X, 0);

			collider.collidingWith.value = entity;
			collider.collidingWith.enabled = true;
		}

		private void MoveCollider(Entity entity)
		{
			Position position = entity.GetComponent<Position>();
			Collider collider = entity.GetComponent<Collider>();
			collider.collider = new Rectangle(new Point().FromVector(position.position + collider.offset), collider.collider.Size);
		}
		private void MoveColliders(List<Entity> colliders)
		{
			foreach (Entity entity in colliders)
			{
				//if (entity.HasComponent<Static>()) { continue; }
				Position position = entity.GetComponent<Position>();
				Collider collider = entity.GetComponent<Collider>();
				collider.collider = new Rectangle(new Point().FromVector(position.position + collider.offset), collider.collider.Size);
			}
		}
	}
}
