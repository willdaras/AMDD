using AMDD.Common;
using Microsoft.Xna.Framework;

namespace AMDD.ECS.Components;

public class Collider : Component
{
	public Vector2 lastValidPos = Vector2.Zero;
	public bool continuous = false;

	public Vector2 offset = Vector2.Zero;
	public Rectangle collider = Rectangle.Empty;
	public Optional<Entity> collidingWith;

	public int collisionLayer = 1;
}