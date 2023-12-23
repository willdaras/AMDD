using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation.Animator;

/// <summary>
/// Represents an animation, a series of sprites
/// </summary>
public abstract class Animation
{
	public int frame { get; protected set; }

	public abstract IAnimationFrame GetFrame(IAnimatorState state);
}