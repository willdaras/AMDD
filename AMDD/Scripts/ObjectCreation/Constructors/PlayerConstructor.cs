using AMDD.Animation;
using AMDD.ECS.Components;
using AMDD.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AMDD.ObjectCreation;

public class PlayerConstructor : IObjectConstructor
{
	public SceneEntity ConstructObject()
	{
		var Content = GameData.contentManager;

		SceneEntity entity = new SceneEntity();
		entity.GetComponent<Name>().name = "player";
		entity.position.position = new Vector2(70, 0);
		entity.AddComponent<Player>();
		entity.AddComponent<FacingDirection>();
		entity.AddComponent(new Sprite() { image = Content.Load<Texture2D>("Sprites/Player/right/player_raised_right"), layer = Sprite.Layer.Sprites }); entity.AddComponent<Input>(); entity.AddComponent(new Physics() { dragScale = 2f, xDragScale = 4f });
		entity.AddComponent(new Shooting() { bulletPool = new ObjectPool(new BulletConstructor(), 20) });
		entity.AddComponent(new Gravity() { gravityScale = 5f }); entity.AddComponent(new Collider() { collider = new Rectangle(0, 0, 14, 31), offset = new Vector2(3, 0), continuous = true });
		entity.AddComponent(new Animated() { tree = GetPlayerTree.ConstructPlayerTree(Content) });
		entity.AddComponent<Grounded>();
		return entity;
	}
}