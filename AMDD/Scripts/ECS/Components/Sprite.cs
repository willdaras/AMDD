using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AMDD.ECS.Components;

public class Sprite : Component
{
	public Texture2D image;
	public Vector2 offset = Vector2.Zero;
	public Sprite() { }
	public Sprite(Texture2D texture) => this.image = texture;
}