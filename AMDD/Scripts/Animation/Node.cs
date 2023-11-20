using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation;

/// <summary>
/// An animator node, used to represent a branch in the behaviour tree.
/// </summary>
public abstract class Node
{
	/// <summary>
	/// Resolves the node, returning the TerminationNode at the end of the branch.
	/// </summary>
	/// <param name="state"> The current state of the animator. </param>
	/// <returns> The TerminationNode at the end of the branch. </returns>
	public abstract TerminationNode Resolve(IAnimatorState state);
}