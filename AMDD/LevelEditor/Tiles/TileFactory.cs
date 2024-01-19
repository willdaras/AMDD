using AMDD.ECS.Components;
using AMDD.LevelEditor.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AMDD.LevelEditor.Tiles;

public class TileFactory : ITileFactory
{
	public SceneEntity GetTile(Vector2 position, Texture2D texture, string texAddress, int layer = (int)Sprite.Layer.Sprites)
	{
		SceneEntity entity = new SceneEntity();
		entity.position.position = position;
		entity.name.name = "Tile";
		entity.AddComponent(new Sprite(texture) { layer = (Sprite.Layer)layer, address = texAddress });
		return entity;
	}
}