using AMDD.ECS.Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace AMDD.ECS.Systems;

public class AnimationSystem : System
{
	private float _timer = 0;
	public float fps = 12;
	private float _interval => 1 / fps;

	public override Type[] RequiredComponents { get; } = new Type[] { typeof(Sprite), typeof(Animated) };

	public override void Update(EntityMap entities, float deltaTime)
	{
		_timer += deltaTime;
		if (_timer < _interval)
		{
			return;
		}
		_timer = 0;

		//Debug.WriteLine("animationFrame");
		foreach (Entity entity in entities.GetEntitiesWithComponents(RequiredComponents))
		{
			Animated animator = entity.GetComponent<Animated>();
			Sprite sprite = entity.GetComponent<Sprite>();
			Animation.Animator.IAnimationFrame frame = animator.tree.ResolveAnimation(animator.tree.currentState);
			sprite.image = frame.GetImage();
			sprite.offset = frame.offset;
		}
	}
}
