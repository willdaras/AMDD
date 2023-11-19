using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation.Animator;

public class BasicAnimationFrame : AnimationFrame
{
	private Texture2D _texture;

	public BasicAnimationFrame(Texture2D texture)
	{
		_texture = texture;
	}

	public override Texture2D GetImage()
	{
		return _texture;
	}
}