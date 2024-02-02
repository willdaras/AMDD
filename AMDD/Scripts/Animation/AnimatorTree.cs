using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation;

/// <summary>
/// A representation of an animator behaviour tree, stores the root node and resolves the tree.
/// </summary>
/// <remarks> Is a TerminationNode, so a tree can end in another tree. </remarks>
public class AnimatorTree : TerminationNode
{
	public IAnimatorState currentState;

	public Node treeRoot;

	public AnimatorTree()
	{
		currentState = new AnimatorState(new System.Collections.Generic.Dictionary<string, int>());
	}

	public override Animator.IAnimationFrame ResolveAnimation(IAnimatorState state)
	{
		TerminationNode node = treeRoot.Resolve(state);

		currentState = state;

		return node.ResolveAnimation(state);
	}
}