namespace AMDD.ECS.Components;

public class Shooting : Component
{
	public ObjectPooling.ObjectPool bulletPool;

	public bool shooting => shootFrameCount <= shootFrames;
	public int shootFrames => 15;
	public int shootFrameCount;

	public ShootDirections shootDir = ShootDirections.Forward;

	public enum ShootDirections
	{
		Forward,
		DiagonalUp,
		DiagonalDown,
		Up,
		Down
	}
}