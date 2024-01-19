using AMDD.ECS;
using AMDD.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AMDD.Rendering;

public class ParalaxCamera : BasicLayerCamera
{
	private float _scaleX = 0.5f;
	private float _scaleY = 0.9f;

	public ParalaxCamera(Vector2 size)
	{
		ECS.Components.Camera camera = new ECS.Components.Camera();
		camera.width = (int)size.X;
		camera.height = (int)size.Y;
		AddComponent(camera);
	}
	public ParalaxCamera(int width, int height)
	{
		ECS.Components.Camera camera = new ECS.Components.Camera();
		camera.width = width;
		camera.height = height;
		AddComponent(camera);
	}

	public ParalaxCamera() { }

	protected override void DrawEntity(Entity entity, SpriteBatch spriteBatch)
	{
		Sprite sprite = entity.GetComponent<Sprite>();
		if ((sprite.layer & renderLayers) == 0)
		{
			//Debug.WriteLine($"sprite not within layers {(int)sprite.layers}, {renderLayers}");
			return;
		}
		//this.position.position.X *= _scaleX;
		//this.position.position.Y *= _scaleY;
		Position position = entity.GetComponent<Position>();
		Vector2 pos = position.position + sprite.offset;
		//pos.Floor();
		Rectangle imageBounds = new Rectangle((int)MathF.Round(pos.X), (int)MathF.Round(pos.Y), sprite.image.Width, sprite.image.Height);
		if (!bounds.Intersects(imageBounds))
			return;
		pos -= this.position.position - (camera.size / 2);
		pos.X *= _scaleX;
		pos.Y *= _scaleY;
		spriteBatch.Draw(sprite.image, pos, sprite.color);
	}
}