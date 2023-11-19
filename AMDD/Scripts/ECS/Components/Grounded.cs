namespace AMDD.ECS.Components;

public class Grounded : Component
{
	public bool grounded = false;

	public int groundedBuffer = 0;
	public const int groundBufferLength = 5;
}