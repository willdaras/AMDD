using Microsoft.Xna.Framework;

namespace AMDD.ECS.Systems.Input;

public struct InputState
{
	public Vector2 movementDir;
	public bool jumping;

	public bool shooting;
	public Vector2 aimDir;
}