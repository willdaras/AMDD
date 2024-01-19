using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Text.Json.Serialization;

namespace AMDD.ECS.Components;

public class Sprite : Component
{
	[JsonIgnore] public Texture2D image;
	public Vector2 offset { get; set; } = Vector2.Zero;
	public Layer layer { get; set; } = Layer.Default;
	public Color color { get; set; } = Color.White;
	public string address { get; set; } = "";

	public Sprite() { }
	public Sprite(Texture2D texture) => this.image = texture;

	[Flags]
	public enum Layer
	{
		Default = 0,
		Background = 1,
		Sprites = 2,
		Foreground = 4,
		UI = 8,
	}
}