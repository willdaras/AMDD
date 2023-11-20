using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using AMDD.ECS;
using AMDD.ECS.Systems;
using System;

namespace AMDD;

/// <summary>
/// A scene, used to represent a level / room, contains a physics and draw system, and a list of extra systems.
/// </summary>
public class Scene
{
	/// <summary>
	/// The scene's entitymap, keeps track of entities and archetypes.
	/// </summary>
	public EntityMap entityMap = new EntityMap();

	/// <summary>
	/// All extra systems in the scene.
	/// </summary>
	public List<ECS.System> systems = new List<ECS.System>();
	/// <summary>
	/// The scene's physics system.
	/// </summary>
	public ECS.System physicsSystem = new PhysicsSystem();
	/// <summary>
	/// The scene's draw system.
	/// </summary>
	public ECS.System drawSystem = new DrawSystem();

	/// <summary>
	/// Triggered when the scene is loaded, used for initialization.
	/// </summary>
	/// <remarks> TODO - this doesn't get triggered yet. </remarks>
	public void OnEnable()
	{
		Entity.OnComponentRemoved += entityMap.ComponentRemoved;
		Entity.OnComponentAdded += entityMap.ComponentAdded;
	}
	/// <summary>
	/// Triggered when the scene is unload, used for deinitialization.
	/// </summary>
	/// <remarks> TODO - this doesn't get triggered yet. </remarks>
	public void OnDisable()
	{
		Entity.OnComponentRemoved -= entityMap.ComponentRemoved;
		Entity.OnComponentAdded -= entityMap.ComponentAdded;
	}

	/// <summary>
	/// The main update loop, updates all systems in the scene.
	/// </summary>
	/// <param name="gameTime"> Used to calculate deltatime. </param>
	public void Update(GameTime gameTime)
	{
		double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
		UpdateSystems(deltaTime);
	}

	/// <summary>
	/// Draws the scene, uses the draw system.
	/// </summary>
	/// <param name="gameTime"> Used to calculate deltatime. </param>
	public void Draw(GameTime gameTime)
	{
		double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
		drawSystem.Update(entityMap, (float)deltaTime);
	}

	/// <summary>
	/// Updates all extra systems in the scene, followed by updating physics.
	/// </summary>
	/// <param name="deltaTime"> The time between since the last frame. </param>
	private void UpdateSystems(double deltaTime)
	{
		foreach (ECS.System system in systems)
		{
			system.Update(entityMap, (float)deltaTime);
		}
		physicsSystem.Update(entityMap, (float)deltaTime);
	}
}