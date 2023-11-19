using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation.Nodes;

public class DirectionalAnimationNode : TerminationNode
{
	private Animator.Animation _animationL;
	private Animator.Animation _animationR;

	public DirectionalAnimationNode(Animator.Animation animationL, Animator.Animation animationR)
	{
		_animationL = animationL;
		_animationR = animationR;
	}

	public override Texture2D ResolveAnimation(IAnimatorState state)
	{
		if (state.GetBool("facingRight"))
		{
			return _animationR.GetFrame(state);
		}
		else
		{
			return _animationL.GetFrame(state);
		}
		
	}
}