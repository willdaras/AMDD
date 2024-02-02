using AMDD.ECS;
using Microsoft.Xna.Framework.Graphics;

namespace AMDD.LevelEditor.Components;

/// <summary>
/// A Component for storing the LevelPainter texture to paint and the file's address.
/// </summary>
public class LevelPainter : Component
{
	public Texture2D currentTexture;
	public string address;
}