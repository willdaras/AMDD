using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Text.Json.Serialization;

namespace AMDD.ECS.Components;

/// <summary>
/// An Entity's sprite, letting it be drawn.
/// </summary>
public class Sprite : Component
{
	/// <summary>
	/// The sprite to draw.
	/// </summary>
	[JsonIgnore] public Texture2D image;
	/// <summary>
	/// The offset from the Entity's position to draw the sprite at.
	/// </summary>
	public Vector2 offset { get; set; } = Vector2.Zero;
	/// <summary>
	/// The sprite layer to be drawn on.
	/// </summary>
	public Layer layer { get; set; } = Layer.Default;
	/// <summary>
	/// The colour to tint the sprite by. White means unchanged.
	/// </summary>
	public Color color { get; set; } = Color.White;
	/// <summary>
	/// The sprite's file address, used in serialization.
	/// </summary>
	public string address { get; set; } = "";

	public Sprite() { }
	public Sprite(Texture2D texture) => this.image = texture;

	[Flags]
	public enum Layer
	{
		Default = 0,
		BackgroundParalax = 1,
		Background = 2,
		Sprites = 4,
		Foreground = 8,
		UI = 16,
	}
}