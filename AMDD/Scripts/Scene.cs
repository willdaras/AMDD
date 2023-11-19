using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using AMDD.ECS;
using AMDD.ECS.Systems;
using System;

namespace AMDD;

public class Scene
{
	public List<SceneEntity> entities;
	// public List<Actor> actors => entities.OfType<Actor>().ToList();

	public EntityMap entityMap = new EntityMap();

	public List<ECS.System> systems = new List<ECS.System>();
	public ECS.System physicsSystem = new PhysicsSystem();
	public ECS.System drawSystem = new DrawSystem();

	public void OnEnable()
	{
		Entity.OnComponentRemoved += entityMap.ComponentRemoved;
		Entity.OnComponentAdded += entityMap.ComponentAdded;
	}
	public void OnDisable()
	{
		Entity.OnComponentRemoved -= entityMap.ComponentRemoved;
		Entity.OnComponentAdded -= entityMap.ComponentAdded;
	}

	public void Update(GameTime gameTime)
	{
		double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
		UpdateSystems(deltaTime);
	}

	public void Draw(GameTime gameTime)
	{
		double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
		drawSystem.Update(entityMap, (float)deltaTime);
	}

	private void UpdateSystems(double deltaTime)
	{
		foreach (ECS.System system in systems)
		{
			system.Update(entityMap, (float)deltaTime);
		}
		physicsSystem.Update(entityMap, (float)deltaTime);
	}
}