using AMDD.Common;
using Microsoft.Xna.Framework;
using System.Text.Json.Serialization;

namespace AMDD.ECS.Components;

public class Collider : Component
{
	public Vector2 lastValidPos { get; set; } = Vector2.Zero;
	public bool continuous { get; set; } = false;

	public Vector2 offset { get; set; } = Vector2.Zero;
	public Rectangle collider { get; set; } = Rectangle.Empty;
	[JsonIgnore] public Optional<Entity> collidingWith;

	public int collisionLayer { get; set; } = 1;
}