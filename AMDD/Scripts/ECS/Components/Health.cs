namespace AMDD.ECS.Components;

public class Health : Component
{
	public int health = 10;
	public int maxHealth = 10;

	public bool invincible => iFrameCount < iFrames;
	public int iFrames = 0;
	public int iFrameCount = 0;
}