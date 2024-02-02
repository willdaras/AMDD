namespace AMDD.ECS.Systems.Input;

/// <summary>
/// An input provider.
/// </summary>
public interface IInputProvider
{
	/// <summary>
	/// Gets the current state of input.
	/// </summary>
	/// <param name="inputState"> An optional parameter allowing an existing inputstate to be modified. </param>
	/// <returns> The current of input as an InputState. </returns>
	public InputState GetInputState(InputState inputState = new InputState());
}