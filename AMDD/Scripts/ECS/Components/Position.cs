using Microsoft.Xna.Framework;
using System.Text.Json.Serialization;

namespace AMDD.ECS.Components;

public class Position : Component
{
	public Vector2 position;// = new Vector2(0, 0);

	public Position() { }
	public Position(Vector2 position) => this.position = position;
}