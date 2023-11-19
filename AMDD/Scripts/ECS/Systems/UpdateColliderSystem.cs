using AMDD.ECS.Components;
using Microsoft.Xna.Framework;
using System;

namespace AMDD.ECS;

public class UpdateColliderSystem : System
{
	public override Type[] RequiredComponents { get; } = new Type[] { typeof(Position), typeof(Collider) };

	public override void Update(EntityMap entities, float deltaTime)
	{
		foreach (Entity entity in entities.GetEntitiesWithComponents(RequiredComponents))
		{
			Position position = entity.GetComponent<Position>();
			Collider collider = entity.GetComponent<Collider>();
			collider.collider = new Rectangle(new Point().FromVector(position.position), collider.collider.Size);
		}
	}
}