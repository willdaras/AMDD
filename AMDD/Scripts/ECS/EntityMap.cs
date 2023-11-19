using System;
using System.Collections;
using System.Collections.Generic;

namespace AMDD.ECS;

public class EntityMap : IEnumerable<Entity>
{
	private List<Entity> _entities = new List<Entity>();
	private Dictionary<Type, HashSet<Entity>> _entityLists = new Dictionary<Type, HashSet<Entity>>();

	public void AddNewEntity(Entity entity)
	{
		if (entity == null || _entities.Contains(entity)) return;
		_entities.Add(entity);
		foreach (Component component in entity.Components)
		{
			if (!_entityLists.ContainsKey(component.GetType()))
			{
				_entityLists.Add(component.GetType(), new HashSet<Entity> { entity });
			}
			else
			{
				_entityLists[component.GetType()].Add(entity);
			}
		}
	}
	public void RemoveEntity(Entity entity)
	{
		if (!_entities.Contains(entity)) { return; }
		_entities.Remove(entity);
		foreach (Component component in entity.Components)
		{
			_entityLists[component.GetType()].Remove(entity);
		}
	}

	public void ComponentRemoved(Entity entity, Type componentType)
	{
		if (_entityLists.ContainsKey(componentType))
		{
			_entityLists[componentType].Remove(entity);
		}
	}
	public void ComponentAdded(Entity entity, Type componentType)
	{
		if (entity == null) return;
		if (!_entityLists.TryGetValue(componentType, out var componentSet))
		{
			_entityLists[componentType] = new HashSet<Entity>();
		}
		componentSet.Add(entity);
	}

	public List<Entity> GetEntitiesWithComponents(params Type[] componentTypes)
	{
		if (!_entityLists.TryGetValue(componentTypes[0], out HashSet<Entity> firstSet))
			return new List<Entity>();
		HashSet<Entity> entities = new HashSet<Entity>(firstSet);
		for (int i = 1; i < componentTypes.Length; i++)
		{
			if (!_entityLists.TryGetValue(componentTypes[i], out HashSet<Entity> entitySet))
				return new List<Entity>();
			entities.IntersectWith(entitySet);
		}
		return new List<Entity>(entities);
	}

	#region Enumerator
	public IEnumerator<Entity> GetEnumerator()
	{
		return _entities.GetEnumerator();
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
	#endregion
}