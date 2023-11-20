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
	/// Resolves the animation and returns the current sprite.
	/// </summary>
	/// <param name="state"> The current state of the animator. </param>
	/// <returns> The current sprite as a Texture2D. </returns>
	public abstract Texture2D ResolveAnimation(IAnimatorState state);
}