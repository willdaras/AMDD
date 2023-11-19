using Microsoft.Xna.Framework.Graphics;

namespace AMDD.Animation;

public abstract class Node
{
	public abstract TerminationNode Resolve(IAnimatorState state);
}