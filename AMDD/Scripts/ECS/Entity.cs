using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AMDD.ECS;

public abstract class Entity
{
	public List<Component> Components { get; private set;} = new List<Component>();

	public static event Action<Entity, Type> OnComponentRemoved;
	public static event Action<Entity, Type> OnComponentAdded;

	public void AddComponent(Component component)
	{
		Components.Add(component);
		//onChange.Invoke();
	}
	public T AddComponent<T>() where T : Component, new()
	{
		T component = new T();
		Components.Add(component);
		//onChange.Invoke();
		return component;
	}

	public void RemoveComponent<T>() where T : Component
	{
		if (TryGetComponent(out T component))
		{
			Components.Remove(component);
			OnComponentRemoved(this, typeof(T));
		}
	}

	public T GetComponent<T>() where T : Component
	{
		return Components.OfType<T>().FirstOrDefault();
	}
	public bool TryGetComponent<T>(out T component) where T : Component
	{
		component = Components.OfType<T>().FirstOrDefault();
		return component != null;
	}
	public bool HasComponent<T>() where T : Component
	{
		return Components.OfType<T>().FirstOrDefault() != null;
	}
	public bool HasComponent(Type componentType)
	{
		return Components.Any(c => c.GetType() == componentType);
	}

}