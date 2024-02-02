namespace AMDD.ECS.Components;

/// <summary>
/// Indicates whether an Entity should be affected by gravity, and the scale of the gravity.
/// </summary>
public class Gravity : Component
{
	public float gravityScale { get; set; } = 1f;
}