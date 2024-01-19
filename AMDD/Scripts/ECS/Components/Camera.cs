using Microsoft.Xna.Framework;

namespace AMDD.ECS.Components;

public class Camera : Component
{
	public int width { get; set; }
	public int height { get; set; }
	public Vector2 size => new Vector2(width, height);
}