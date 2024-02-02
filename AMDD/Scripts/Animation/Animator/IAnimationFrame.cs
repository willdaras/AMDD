using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation.Animator;

/// <summary>
/// A representation of a frame in an animation.
/// </summary>
public interface IAnimationFrame
{
	/// <summary>
	/// Get the frame that should be displayed.
	/// </summary>
	/// <returns> The frame to display. </returns>
	public Texture2D GetImage();

	/// <summary>
	/// The sprite's offset from its position
	/// </summary>
	/// <remarks> Mostly used for sprites that don't line up nicely. </remarks>
	public Vector2 offset { get; }
}