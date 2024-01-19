namespace AMDD.ECS.Components;

public class Health : Component
{
	public int health { get; set; } = 10;
	public int maxHealth { get; set; } = 10;

	public bool invincible => iFrameCount < iFrames;
	public int iFrames { get; set; } = 0;
	public int iFrameCount { get; set; } = 0;
}