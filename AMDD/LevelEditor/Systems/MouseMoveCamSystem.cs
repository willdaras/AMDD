using AMDD.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using AMDD.ECS;

namespace AMDD.LevelEditor.Systems;

public class MouseMoveCamSystem : ECS.System
{
	public override Type[] RequiredComponents { get; } = new Type[] { typeof(Camera) };

	private Point _prevPos = Point.Zero;

	public override void Update(EntityMap entities, float deltaTime)
	{
		MoveByMouse(entities);
	}

	private void MoveByMouse(EntityMap entities)
	{
		MouseState state = Mouse.GetState();

		if (state.MiddleButton == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Y))
		{
			foreach (Entity entity in entities.GetEntitiesWithComponents(RequiredComponents))
			{
				entity.GetComponent<Position>().position += (_prevPos - state.Position).ToVector2();
			}
		}

		_prevPos = state.Position;
	}
}