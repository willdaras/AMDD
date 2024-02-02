using Microsoft.Xna.Framework;

namespace AMDD.ECS.Components;

/// <summary>
/// Lets an entity read input.
/// </summary>
public class Input : Component
{
	public Vector2 moveDir { get; set; }
	public bool jumping { get; set; }
	public bool shooting { get; set; }
	public Vector2 aimDir { get; set; }
	public bool aiming { get; set; }
}