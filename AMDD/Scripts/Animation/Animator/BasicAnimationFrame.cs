using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation.Animator;

/// <summary>
/// A basic implementation of an animation frame
/// </summary>
public class BasicAnimationFrame : IAnimationFrame
{
	private Texture2D _texture;

	public Vector2 offset { get; private set; }

	public BasicAnimationFrame(Texture2D texture)
	{
		_texture = texture;
		offset = Vector2.Zero;
	}
	public BasicAnimationFrame(Texture2D texture, Vector2 offset)
	{
		this._texture = texture;
		this.offset = offset;
	}

	public Texture2D GetImage()
	{
		return _texture;
	}
}