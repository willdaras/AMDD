using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AMDD.Animation.Animator;

/// <summary>
/// A basic animation.
/// </summary>
public class BasicAnimation : Animation
{
	private List<IAnimationFrame> _frames = new List<IAnimationFrame>();

	public BasicAnimation(List<IAnimationFrame> frames)
	{
		_frames = frames;
	}

	public override IAnimationFrame GetFrame(IAnimatorState state)
	{
		if (state.playingAnim == this)
		{
			frame += 1;
		}
		else
		{
			frame = 0;
		}
		if (frame >= _frames.Count)
		{
			frame = 0;
		}
		state.playingAnim = this;
		return _frames[frame];
	}
}