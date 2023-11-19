using Microsoft.Xna.Framework;

namespace AMDD.ECS.Components;

public class Position : Component
{
	public Vector2 position;

	public Position() => position = new Vector2(0, 0);
	public Position(Vector2 position) => this.position = position;
}