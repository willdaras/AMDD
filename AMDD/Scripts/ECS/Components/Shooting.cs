using System.Text.Json.Serialization;

namespace AMDD.ECS.Components;

/// <summary>
/// Shooting stats for an Entity.
/// </summary>
public class Shooting : Component
{
	public ObjectCreation.ObjectPool bulletPool;

	[JsonIgnore] public bool shooting => shootFrameCount <= shootFrames;
	[JsonIgnore] public int shootFrames => 15;
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