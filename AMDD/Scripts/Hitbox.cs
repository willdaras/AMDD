using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace AMDD;

public struct Hitbox
{
	public Rectangle hitBoxBounds { get; private set; }

	public Hitbox(Rectangle rectangle)
	{
		hitBoxBounds = rectangle;
	}
	public Hitbox(int x, int y, int width, int height)
	{
		hitBoxBounds = new Rectangle(x, y, width, height);
	}
}