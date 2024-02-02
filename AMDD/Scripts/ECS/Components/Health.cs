using System.Text.Json.Serialization;

namespace AMDD.ECS.Components;

/// <summary>
/// The health of an Entity and whether they are invincible.
/// </summary>
public class Health : Component
{
	public int health { get; set; } = 10;
	public int maxHealth { get; set; } = 10;

	[JsonIgnore] public bool invincible => iFrameCount < iFrames;
	public int iFrames { get; set; } = 0;
	public int iFrameCount { get; set; } = 0;
}