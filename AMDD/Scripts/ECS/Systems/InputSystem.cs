using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using AMDD.ECS.Systems.Input;
using System;

namespace AMDD.ECS.Systems;

public class InputSystem : System
{
	private IInputProvider _inputProvider = new KeyboardInputManager();

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
		}
	}
}