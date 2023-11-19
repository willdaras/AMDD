using Microsoft.Xna.Framework;

namespace AMDD.ECS.Components;

public class Physics : Component
{
	public Vector2 velocity = Vector2.Zero;
	public Vector2 acceleration = Vector2.Zero;
	public float dragScale = 1;
	public float xDragScale = 1;
	public float yDragScale = 1;
	public float mass = 1;
}