using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AMDD.ECS.Systems.Input;

public class KeyboardInputManager : IInputProvider
{
	public InputState GetInputState(InputState inputState = new InputState())
	{
		inputState.movementDir = GetMoveDir();
		inputState.jumping = Keyboard.GetState().IsKeyDown(Keys.Z);
		return inputState;
	}

	private Vector2 GetMoveDir()
	{
		Vector2 moveDir = new Vector2(0, 0);
		moveDir.X += Keyboard.GetState().IsKeyDown(Keys.Right) ? 1 : 0;
		moveDir.X += Keyboard.GetState().IsKeyDown(Keys.Left) ? -1 : 0;
		moveDir.Y += Keyboard.GetState().IsKeyDown(Keys.Up) ? -1 : 0;
		moveDir.Y += Keyboard.GetState().IsKeyDown(Keys.Down) ? 1 : 0;
		return moveDir;
	}
}