using AMDD.ECS;
using AMDD.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AMDD.LevelEditor.Tiles;

/// <summary>
/// Constructs more complex tile objects.
/// </summary>
public interface ITileFactory
{
	/// <summary>
	/// Returns a constructed tile Entity.
	/// </summary>
	/// <param name="position"> The tile's position. </param>
	/// <param name="texture"> The tile texture. </param>
	/// <param name="texAddress"> The file address of the texture. </param> <remarks> For serialization purposes. </remarks>
	/// <param name="layer"> The layer the tile should be drawn on. </param>
	/// <returns></returns>
	SceneEntity GetTile(Vector2 position, Texture2D texture, string texAddress, int layer = (int)Sprite.Layer.Sprites);
}