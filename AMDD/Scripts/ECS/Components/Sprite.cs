using Microsoft.Xna.Framework.Graphics;

namespace AMDD.ECS.Components;

public class Sprite : Component
{
	public Texture2D image;

	public Sprite() { }
	public Sprite(Texture2D texture) => this.image = texture;
}