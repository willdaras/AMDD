using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation;

/// <summary>
/// Represents the end of a branch in the behaviour tree.
/// </summary>
public abstract class TerminationNode : Node
{
	public override TerminationNode Resolve(IAnimatorState state)
	{
		return this;
	}

	/// <summary>
	/// Resolves the animation and returns the current Animation Frame.
	/// </summary>
	/// <param name="state"> The current state of the animator. </param>
	/// <returns> The current animation frame. </returns>
	public abstract Animator.IAnimationFrame ResolveAnimation(IAnimatorState state);
}