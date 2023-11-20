using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AMDD.ECS;
using AMDD.ECS.Components;

namespace AMDD;

public class SceneEntity : Entity
{
	public Position position;
	public Name name;

	public SceneEntity()
	{
		position = AddComponent<Position>();
		name = AddComponent<Name>();
	}
	public SceneEntity(Vector2 position)
	{
		this.position = new Position(position);
		AddComponent(this.position);
		name = AddComponent<Name>();
	}
}