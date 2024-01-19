using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AMDD;

public static class GameData
{
	public static SpriteBatch spriteBatch;
	public static ContentManager contentManager;

	public static int screenX = 240;
	public static int screenY = 160;
	public static float upscaleScale = 3.0f;
}