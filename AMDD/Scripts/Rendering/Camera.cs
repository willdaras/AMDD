using AMDD.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AMDD.Rendering;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$camera")]
[JsonDerivedType(typeof(Rendering.BasicCameraStack), typeDiscriminator: "camerastack")]
[JsonDerivedType(typeof(Rendering.BasicLayerCamera), typeDiscriminator: "layercamera")]
[JsonDerivedType(typeof(Rendering.UICamera), typeDiscriminator: "uicamera")]
public abstract class Camera : SceneEntity
{
	public static Camera activeCamera { get; private set; }

	public abstract Rectangle bounds { get; }

	public void SetActive()
	{
		activeCamera = this;
	}
	public void SetActive(Camera camera)
	{
		activeCamera = camera;
	}

	public abstract void Draw(SpriteBatch spriteBatch, List<Entity> entitiesToDraw);

	public static bool WithinCamera(Rectangle rectangle)
	{
		return activeCamera.bounds.Intersects(rectangle);
	}
	public static bool WithinCamera(Rectangle rectangle, int multiplier)
	{
		Rectangle bounds = activeCamera.bounds;
		bounds.X -= bounds.Width / 2;
		bounds.Y -= bounds.Height / 2;
		bounds.Width *= 2;
		bounds.Height *= 2;
		return bounds.Intersects(rectangle);
	}
}