using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation.Animator;

/// <summary>
/// A basic implementation of an animation frame
/// </summary>
public class BasicAnimationFrame : IAnimationFrame
{
	private Texture2D _texture;

	public BasicAnimationFrame(Texture2D texture)
	{
		_texture = texture;
	}

	public Texture2D GetImage()
	{
		return _texture;
	}
}