using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation.Animator;

public abstract class Animation
{
	public int frame { get; protected set; }

	public abstract Texture2D GetFrame(IAnimatorState state);
}