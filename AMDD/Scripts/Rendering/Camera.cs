using AMDD.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AMDD.Rendering;

/// <summary>
/// Represents an abstract Camera.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "$camera")]
[JsonDerivedType(typeof(Rendering.BasicCameraStack), typeDiscriminator: "camerastack")]
[JsonDerivedType(typeof(Rendering.BasicLayerCamera), typeDiscriminator: "layercamera")]
[JsonDerivedType(typeof(Rendering.UICamera), typeDiscriminator: "uicamera")]
[JsonDerivedType(typeof(Rendering.ParalaxCamera), typeDiscriminator: "paralaxcamera")]
public abstract class Camera : SceneEntity
{
	/// <summary>
	/// The current Camera actively rendering.
	/// </summary>
	public static Camera activeCamera { get; private set; }

	/// <summary>
	/// The Camera's bounds - the space it covers.
	/// </summary>
	public abstract Rectangle bounds { get; }

	/// <summary>
	/// Makes the camera the current active Camera.
	/// </summary>
	public void SetActive()
	{
		activeCamera = this;
	}
	/// <summary>
	/// Sets the active Camera.
	/// </summary>
	/// <param name="camera"> The Camera to become the active Camera. </param>
	public void SetActive(Camera camera)
	{
		activeCamera = camera;
	}

	/// <summary>
	/// Draws the Entities passed in to the screen.
	/// </summary>
	/// <param name="spriteBatch"> The SpriteBatch to draw with. </param>
	/// <param name="entitiesToDraw"> The list of Entities to draw. </param>
	public abstract void Draw(SpriteBatch spriteBatch, List<Entity> entitiesToDraw);

	/// <summary>
	/// Checks if a rectangle is within the Camera bounds.
	/// </summary>
	/// <param name="rectangle"> The rectangle to check the bounds of. </param>
	/// <returns> Whether the rectangle is within the Camera bounds. </returns>
	public static bool WithinCamera(Rectangle rectangle)
	{
		return activeCamera.bounds.Intersects(rectangle);
	}
	/// <summary>
	/// Checks if a rectangle is within the Camera bounds.
	/// </summary>
	/// <param name="rectangle"> The rectangle to check the bounds of. </param>
	/// <returns> Whether the rectangle is within the Camera bounds. </returns>
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