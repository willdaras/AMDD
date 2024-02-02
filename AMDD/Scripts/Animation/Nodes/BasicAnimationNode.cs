using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation.Nodes;

/// <summary>
/// A TerminationNode that stores an animation.
/// </summary>
public class BasicAnimationNode : TerminationNode
{
	private Animator.Animation _animation;

	public BasicAnimationNode(Animator.Animation animation)
	{
		_animation = animation;
	}

	public override Animator.IAnimationFrame ResolveAnimation(IAnimatorState state)
	{
		return _animation.GetFrame(state);
	}
}