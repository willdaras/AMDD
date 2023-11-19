using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation;

public abstract class TerminationNode : Node
{
	public override TerminationNode Resolve(IAnimatorState state)
	{
		return this;
	}

	public abstract Texture2D ResolveAnimation(IAnimatorState state);
}