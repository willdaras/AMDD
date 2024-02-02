namespace AMDD.ECS.Components;

/// <summary>
/// A buffer for the damage an Entity accumulates over a frame.
/// </summary>
public class DamageBuffer : Component
{
	public int damageBuffer { get; set; }
}