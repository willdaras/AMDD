using Microsoft.Xna.Framework;
using System.Text.Json.Serialization;

namespace AMDD.ECS.Components;

/// <summary>
/// Stores camera data.
/// </summary>
public class Camera : Component
{
	public int width { get; set; }
	public int height { get; set; }
	[JsonIgnore] public Vector2 size => new Vector2(width, height);
}