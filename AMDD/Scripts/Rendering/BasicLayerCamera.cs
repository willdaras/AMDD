using AMDD.ECS;
using AMDD.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AMDD.Rendering;

public class BasicLayerCamera : Camera
{
	public ECS.Components.Camera camera => GetComponent<ECS.Components.Camera>();

	public override Rectangle bounds => new Rectangle((int)MathF.Round(position.position.X - (camera.size / 2).X), (int)MathF.Round(position.position.Y - (camera.size / 2).Y), camera.width, camera.height);

	public Sprite.Layer renderLayers = Sprite.Layer.Default;

	public BasicLayerCamera(Vector2 size)
	{
		ECS.Components.Camera camera = new ECS.Components.Camera();
		camera.width = (int)size.X;
		camera.height = (int)size.Y;
		AddComponent(camera);
	}
	public BasicLayerCamera(int width, int height)
	{
		ECS.Components.Camera camera = new ECS.Components.Camera();
		camera.width = width;
		camera.height = height;
		AddComponent(camera);
	}

	public BasicLayerCamera() { }

	public override void Draw(SpriteBatch spriteBatch, List<Entity> entitiesToDraw)
	{
		foreach (Entity entity in entitiesToDraw)
		{
			DrawEntity(entity, spriteBatch);
		}
	}

	private void DrawEntity(Entity entity, SpriteBatch spriteBatch)
	{
		Sprite sprite = entity.GetComponent<Sprite>();
		if ((sprite.layer & renderLayers) == 0)
		{
			//Debug.WriteLine($"sprite not within layers {(int)sprite.layers}, {renderLayers}");
			return;
		}
		Position position = entity.GetComponent<Position>();
		Vector2 pos = position.position + sprite.offset;
		//pos.Floor();
		Rectangle imageBounds = new Rectangle((int)MathF.Round(pos.X), (int)MathF.Round(pos.Y), sprite.image.Width, sprite.image.Height);
		if (!bounds.Intersects(imageBounds))
			return;
		pos -= this.position.position - (camera.size / 2);
		spriteBatch.Draw(sprite.image, pos, sprite.color);
	}
}