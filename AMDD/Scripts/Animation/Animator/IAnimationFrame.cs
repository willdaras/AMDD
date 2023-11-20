using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation.Animator;

/// <summary>
/// A representation of a basic frame in an animation.
/// </summary>
public interface IAnimationFrame
{
	/// <summary>
	/// Get the frame that should be displayed.
	/// </summary>
	/// <returns> The frame to display. </returns>
	public abstract Texture2D GetImage();
}