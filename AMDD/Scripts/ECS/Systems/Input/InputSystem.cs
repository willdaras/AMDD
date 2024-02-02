using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using AMDD.ECS.Systems.Input;
using System;
using System.Diagnostics;

namespace AMDD.ECS.Systems;

/// <summary>
/// A System to handle input and populate Input Components.
/// </summary>
public class InputSystem : System
{
	private IInputProvider _inputProvider = new KeyboardInputManager();
	/// <summary>
	/// The InputProvider being used. Default is Keyboard.
	/// </summary>
	public IInputProvider inputProvider { get { return _inputProvider; } set { if (value != null) _inputProvider = value; } }

	public override Type[] RequiredComponents { get; } = new Type[] { typeof(Components.Input) };

	public override void Update(EntityMap entities, float deltaTime)
	{
		InputState inputState = _inputProvider.GetInputState();
		foreach (Entity entity in entities.GetEntitiesWithComponents(RequiredComponents))
		{
			Components.Input input = entity.GetComponent<Components.Input>();
			input.moveDir = inputState.movementDir;
			input.jumping = inputState.jumping;
			input.shooting = inputState.shooting;
			input.aimDir = inputState.aimDir;
			input.aiming = inputState.aiming;
		}
	}
}