using Microsoft.Xna.Framework;

namespace AMDD.ECS.Components;

public class Input : Component
{
	public Vector2 moveDir;
	public bool jumping;
	public bool shooting;
	public Vector2 aimDir;
}