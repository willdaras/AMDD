namespace AMDD.ECS.Components;

/// <summary>
/// Indicates an entity is a bullet.
/// </summary>
public class Bullet : Component
{
	public int damage { get; set; } = 1;
}