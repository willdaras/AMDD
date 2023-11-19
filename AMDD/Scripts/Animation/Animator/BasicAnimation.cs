using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AMDD.Animation.Animator;

public class BasicAnimation : Animation
{
	private List<AnimationFrame> _frames = new List<AnimationFrame>();

	public BasicAnimation(List<AnimationFrame> frames)
	{
		_frames = frames;
	}

	public override Texture2D GetFrame(IAnimatorState state)
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
		return _frames[frame].GetImage();
	}
}