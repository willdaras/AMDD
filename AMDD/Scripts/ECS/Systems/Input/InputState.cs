using Microsoft.Xna.Framework;

namespace AMDD.ECS.Systems.Input;

/// <summary>
/// A state of input, storing input data.
/// </summary>
public struct InputState
{
	public Vector2 movementDir;
	public bool jumping;

	public bool shooting;
	public Vector2 aimDir;
	public bool aiming;
}