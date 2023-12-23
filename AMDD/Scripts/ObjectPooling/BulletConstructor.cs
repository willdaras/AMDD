using AMDD.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AMDD.ObjectPooling;

public class BulletConstructor : IObjectConstructor
{
	private Texture2D _sprite;

	public BulletConstructor()
	{
		_sprite = GameData.contentManager.Load<Texture2D>("Sprites/FX/Bullet/basic_bullet");
	}

	public SceneEntity ConstructObject()
	{
		SceneEntity entity = new SceneEntity();
		entity.AddComponent(new Sprite() { image = _sprite });
		entity.AddComponent(new Physics() { dragScale = 0 });
		entity.AddComponent(new Collider() { collider = new Rectangle(0, 0, 2, 2) });
		entity.AddComponent(new Bullet() { damage = 2 });
		return entity;
	}
}