using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AMDD.ECS;
using AMDD.ECS.Components;
using System.Text.Json.Serialization;

namespace AMDD;

/// <summary>
/// A default entity with a Position and Name.
/// </summary>
public class SceneEntity : Entity
{
	[JsonIgnore] public Position position => GetComponent<Position>();
	[JsonIgnore] public Name name => GetComponent<Name>();

	public SceneEntity()
	{
		AddComponent<Position>();
		AddComponent<Name>();
	}
	public SceneEntity(Vector2 position)
	{
		AddComponent(new Position(position));
		AddComponent<Name>();
	}
}