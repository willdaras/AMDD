using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace AMDD.ECS;

/// <summary>
/// An entity, a container for components.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "$entity")]
[JsonDerivedType(typeof(SceneEntity), typeDiscriminator: "scene")]
[JsonDerivedType(typeof(ManagerEntity), typeDiscriminator: "manager")]
[JsonDerivedType(typeof(Rendering.BasicCameraStack), typeDiscriminator: "camerastack")]
[JsonDerivedType(typeof(Rendering.BasicLayerCamera), typeDiscriminator: "layercamera")]
[JsonDerivedType(typeof(Rendering.UICamera), typeDiscriminator: "uicamera")]
[JsonDerivedType(typeof(Rendering.ParalaxCamera), typeDiscriminator: "paralaxcamera")]
public abstract class Entity
{
	/// <summary>
	/// The components in the entity.
	/// </summary>
	public List<Component> Components { get; set;} = new List<Component>();

	/// <summary>
	/// Called when a component is added to the entity.
	/// </summary>
	/// <remarks> Used by the scene to notify the entitymap. </remarks>
	public static event Action<Entity, Type> OnComponentRemoved;
	/// <summary>
	/// Called when a component is removed from the entity.
	/// </summary>
	/// <remarks> Used by the scene to notify the entitymap. </remarks>
	public static event Action<Entity, Type> OnComponentAdded;

	/// <summary>
	/// Adds a component to the entity.
	/// </summary>
	/// <remarks> Adds the exact component passed in. </remarks>
	/// <param name="component"> The component to be added. </param>
	public void AddComponent(Component component)
	{
		Components.Add(component);
	}
	/// <summary>
	/// Adds a component to the entity.
	/// </summary>
	/// <remarks> Creates a new T and adds it to the entity. </remarks>
	/// <typeparam name="T"> The type of component to add to the entity. </typeparam>
	/// <returns> The component added. </returns>
	public T AddComponent<T>() where T : Component, new()
	{
		T component = new T();
		Components.Add(component);
		return component;
	}

	/// <summary>
	/// Removes the component T from the entity.
	/// </summary>
	/// <typeparam name="T"> The type of component to remove. </typeparam>
	public void RemoveComponent<T>() where T : Component
	{
		if (TryGetComponent(out T component))
		{
			Components.Remove(component);
			OnComponentRemoved(this, typeof(T));
		}
	}

	/// <summary>
	/// Finds the component T in the entity.
	/// </summary>
	/// <typeparam name="T"> The type of component to find. </typeparam>
	/// <returns> The component if found, can return null. </returns>
	public T GetComponent<T>() where T : Component
	{
		return Components.OfType<T>().FirstOrDefault();
	}
	/// <summary>
	/// Checks if the entity has the specified component and returns it.
	/// </summary>
	/// <remarks> Safer than GetComponent, mainly used in if statement. </remarks>
	/// <typeparam name="T"> The type of component to find. </typeparam>
	/// <param name="component"> The component if found, can be null. </param>
	/// <returns> A bool representing whether the entity has the specified component.. </returns>
	public bool TryGetComponent<T>(out T component) where T : Component
	{
		component = Components.OfType<T>().FirstOrDefault();
		return component != null;
	}
	/// <summary>
	/// Checks if an entity has a specified component.
	/// </summary>
	/// <typeparam name="T"> The type of component to find. </typeparam>
	/// <returns> A bool representing whether the entity has the specified component. </returns>
	public bool HasComponent<T>() where T : Component
	{
		return Components.OfType<T>().FirstOrDefault() != null;
	}
	/// <summary>
	/// Checks if an entity has a specified component.
	/// </summary>
	/// <param name="componentType"> The type of component to find. </param>
	/// <returns> A bool representing whether the entity has the specified component. </returns>
	public bool HasComponent(Type componentType)
	{
		return Components.Any(c => c.GetType() == componentType);
	}
}