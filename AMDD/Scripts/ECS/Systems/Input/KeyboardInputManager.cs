using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AMDD.ECS.Systems.Input;

public class KeyboardInputManager : IInputProvider
{
	private bool _shootingLastFrame;
	public InputState GetInputState(InputState inputState = new InputState())
	{
		inputState.movementDir = GetDir();
		inputState.jumping = Keyboard.GetState().IsKeyDown(Keys.Z);
		inputState.shooting = Shooting();
		inputState.aimDir = AimDir();
		return inputState;
	}
	
	private bool Shooting()
	{
		bool shooting = Keyboard.GetState().IsKeyDown(Keys.X);
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

	public Vector2 AimDir()
	{
		Vector2 dir = GetDir();
		KeyboardState state = Keyboard.GetState();
		return dir;
	}

	private Vector2 GetDir()
	{
		Vector2 moveDir = new Vector2(0, 0);
		KeyboardState state = Keyboard.GetState();
		moveDir.X += state.IsKeyDown(Keys.Right) ? 1 : 0;
		moveDir.X += state.IsKeyDown(Keys.Left) ? -1 : 0;
		moveDir.Y += state.IsKeyDown(Keys.Up) ? -1 : 0;
		moveDir.Y += state.IsKeyDown(Keys.Down) ? 1 : 0;
		return moveDir;
	}
}