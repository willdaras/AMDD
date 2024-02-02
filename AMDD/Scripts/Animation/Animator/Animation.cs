using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation.Animator;

/// <summary>
/// Represents an animation, a series of sprites.
/// </summary>
public abstract class Animation
{
	/// <summary>
	/// The frame the animation is on.
	/// </summary>
	public int frame { get; protected set; }

	/// <summary>
	/// Returns the current frame of the animation.
	/// </summary>
	/// <param name="state"> The state of the animator to read from. </param>
	/// <returns> The current animation frame. </returns>
	public abstract IAnimationFrame GetFrame(IAnimatorState state);
}