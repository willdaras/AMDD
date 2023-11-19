using System;
using AMDD.ECS.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AMDD.ECS.Systems;

public class DrawSystem : System
{
	public override Type[] RequiredComponents { get; } = new Type[] { typeof(Sprite) };

	public override void Update(EntityMap entities, float deltaTime)
	{
		SpriteBatch spriteBatch = GameData.spriteBatch;
		spriteBatch.Begin(samplerState: SamplerState.PointClamp);
		foreach (Entity entity in entities.GetEntitiesWithComponents(RequiredComponents))
		{
			Sprite sprite = entity.GetComponent<Sprite>();
			Position position = entity.GetComponent<Position>();
			Vector2 pos = position.position;
			pos.Floor();
			spriteBatch.Draw(sprite.image, pos, Color.White);
		}
		spriteBatch.End();
	}
}