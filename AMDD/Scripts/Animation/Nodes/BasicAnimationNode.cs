using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation.Nodes;

public class BasicAnimationNode : TerminationNode
{
	private Animator.Animation _animation;

	public BasicAnimationNode(Animator.Animation animation)
	{
		_animation = animation;
	}

	public override Texture2D ResolveAnimation(IAnimatorState state)
	{
		return _animation.GetFrame(state);
	}
}