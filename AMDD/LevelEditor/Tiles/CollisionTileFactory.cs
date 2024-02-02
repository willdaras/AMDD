using AMDD.ECS.Components;
using AMDD.LevelEditor.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AMDD.LevelEditor.Tiles;

/// <summary>
/// A TileFactory that constructs tiles with Colliders.
/// </summary>
public class CollisionTileFactory : ITileFactory
{
	public SceneEntity GetTile(Vector2 position, Texture2D texture, string texAddress, int layer = (int)Sprite.Layer.Sprites)
	{
		SceneEntity entity = new SceneEntity();
		entity.position.position = position;
		entity.name.name = "ColliderTile";
		entity.AddComponent(new Sprite(texture) { layer = (Sprite.Layer)layer, address = texAddress});
		entity.AddComponent(new Collider() { collider = new Rectangle(entity.position.position.ToPoint(), new Point(16, 16)) });
		entity.AddComponent(new Static());
		return entity;
	}
}