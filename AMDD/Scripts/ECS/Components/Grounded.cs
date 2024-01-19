namespace AMDD.ECS.Components;

public class Grounded : Component
{
	public bool grounded { get; set; } = false;

	public int groundedBuffer { get; set; } = 0;
	public const int groundBufferLength = 5;
}