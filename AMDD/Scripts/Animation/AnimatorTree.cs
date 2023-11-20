using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation;

/// <summary>
/// A representation of an animator behaviour tree, stores the root node and resolves the tree.
/// </summary>
public class AnimatorTree : TerminationNode
{
	public IAnimatorState currentState;

	public Node treeRoot;

	public AnimatorTree()
	{
		currentState = new AnimatorState(new System.Collections.Generic.Dictionary<string, int>());
	}

	public override Texture2D ResolveAnimation(IAnimatorState state)
	{
		TerminationNode node = treeRoot.Resolve(state);

		currentState = state;

		return node.ResolveAnimation(state);
	}
}