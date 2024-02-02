using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation.Nodes;

/// <summary>
/// An directional animation node to differentiate between left and right animations.
/// </summary>
/// <remarks> Uses the facingRight parameter in the AnimatorState to decide its direction. </remarks>
public class DirectionalAnimationNode : TerminationNode
{
	private Animator.Animation _animationL;
	private Animator.Animation _animationR;

	public DirectionalAnimationNode(Animator.Animation animationL, Animator.Animation animationR)
	{
		_animationL = animationL;
		_animationR = animationR;
	}

	public override Animator.IAnimationFrame ResolveAnimation(IAnimatorState state)
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