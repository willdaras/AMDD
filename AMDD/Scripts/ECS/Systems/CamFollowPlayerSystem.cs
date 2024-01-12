using AMDD.ECS.Components;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace AMDD.ECS.Systems;

public class CamFollowPlayerSystem : System
{
	public override Type[] RequiredComponents { get; } = new Type[] { typeof(Camera) };

	private float _offsetX = 0;
	private float _offsetY = 0;

	private float _maxDistX = 25;
	private float _maxDistY = 25;

	public override void Update(EntityMap entities, float deltaTime)
	{
		SceneEntity player = (SceneEntity)entities.GetEntitiesWithComponents(typeof(Player))[0];
		//player.position.position = new Vector2((int)player.position.position.X, (int)player.position.position.Y);
		foreach (Entity camera in entities.GetEntitiesWithComponents(RequiredComponents))
		{
			Components.Input input = player.GetComponent<Components.Input>();
			Physics physics = player.GetComponent<Physics>();

			Vector2 velocity = physics.velocity;
			Debug.WriteLine(physics.velocity);
			if (MathF.Abs(velocity.X) < 20)
				velocity.X = 0;
			if (MathF.Abs(velocity.Y) < 20)
				velocity.Y = 0;

			if (!input.aiming)
			{
				_offsetX = MathHelper.SmoothStep(_offsetX, velocity.X / 7, 0.3f);
				_offsetY = MathHelper.SmoothStep(_offsetY, velocity.Y / 10, 0.2f);
			}
			else
			{
			    _offsetX = MathHelper.SmoothStep(_offsetX, _maxDistX * input.aimDir.X, 0.3f);
			    _offsetY = MathHelper.SmoothStep(_offsetY, _maxDistY * input.aimDir.Y, 0.2f);
			}
			camera.GetComponent<Position>().position = player.position.position + new Vector2(5 + _offsetX, 10 + _offsetY);
		}
	}
}