using Microsoft.Xna.Framework;

namespace AMDD.ECS.Components;

public class Camera : Component
{
	public int width;
	public int height;
	public Vector2 size => new Vector2(width, height);
}