using AMDD.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AMDD.Rendering;

public abstract class Camera : SceneEntity
{
	public static Camera activeCamera { get; private set; }

	public void SetActive()
	{
		activeCamera = this;
	}
	public void SetActive(Camera camera)
	{
		activeCamera = camera;
	}

	public abstract void Draw(SpriteBatch spriteBatch, List<Entity> entitiesToDraw);
}