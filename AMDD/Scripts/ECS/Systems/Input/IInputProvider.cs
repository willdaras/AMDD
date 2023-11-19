namespace AMDD.ECS.Systems.Input;

public interface IInputProvider
{
	public InputState GetInputState(InputState inputState = new InputState());
}