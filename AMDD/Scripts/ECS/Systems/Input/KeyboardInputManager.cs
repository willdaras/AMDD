using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AMDD.ECS.Systems.Input;

public class KeyboardInputManager : IInputProvider
{
	private bool _shootingLastFrame;
	public InputState GetInputState(InputState inputState = new InputState())
	{
		KeyboardState state = Keyboard.GetState();
		inputState.movementDir = MoveDir(state);
		inputState.jumping = state.IsKeyDown(Keys.Z);
		inputState.shooting = Shooting(state);
		inputState.aimDir = AimDir(state);
		inputState.aiming = state.IsKeyDown(Keys.LeftShift);
		return inputState;
	}
	
	private bool Shooting(KeyboardState state)
	{
		bool shooting = state.IsKeyDown(Keys.X);
		if (shooting)
		{
			if (!_shootingLastFrame)
			{
				_shootingLastFrame = shooting;
				return true;
			}
		}
		_shootingLastFrame = shooting;
		return false;
	}

	public Vector2 MoveDir(KeyboardState state)
	{
		if (state.IsKeyDown(Keys.Space)) return Vector2.Zero;
		return GetDir(state);
	}
	public Vector2 AimDir(KeyboardState state)
	{
		Vector2 dir = GetDir(state);
		return dir;
	}

	private Vector2 GetDir(KeyboardState state)
	{
		Vector2 moveDir = new Vector2(0, 0);
		moveDir.X += state.IsKeyDown(Keys.Right) ? 1 : 0;
		moveDir.X += state.IsKeyDown(Keys.Left) ? -1 : 0;
		moveDir.Y += state.IsKeyDown(Keys.Up) ? -1 : 0;
		moveDir.Y += state.IsKeyDown(Keys.Down) ? 1 : 0;
		return moveDir;
	}
}