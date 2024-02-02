using AMDD.Animation;

namespace AMDD.ECS.Components;

/// <summary>
/// Indicates an entity can be animated, and stores the AnimationTree.
/// </summary>
public class Animated : Component
{
	public AnimatorTree tree;
}