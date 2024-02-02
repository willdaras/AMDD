namespace AMDD.ECS.Components;

/// <summary>
/// Whether the Entity is grounded.
/// </summary>
public class Grounded : Component
{
	public bool grounded { get; set; } = false;

	public int groundedBuffer { get; set; } = 0;
	public const int groundBufferLength = 5;
}