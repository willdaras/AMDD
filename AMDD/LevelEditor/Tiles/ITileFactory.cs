using AMDD.ECS;
using AMDD.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AMDD.LevelEditor.Tiles;

public interface ITileFactory
{
	SceneEntity GetTile(Vector2 position, Texture2D texture, string texAddress, int layer = (int)Sprite.Layer.Sprites);
}