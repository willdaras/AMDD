using Microsoft.Xna.Framework;
using System.Collections;
using System.Collections.Generic;

namespace AMDD.ObjectCreation;

/// <summary>
/// Represents a pool of objects - used so complex Entities don't need to be created during gameplay.
/// </summary>
public class ObjectPool : IEnumerable<SceneEntity>
{
	private Queue<SceneEntity> _objects = new Queue<SceneEntity>();
	
	public ObjectPool(IObjectConstructor objectConstructor, int count)
	{
		for (int i = 0; i < count; i++)
		{
			_objects.Enqueue(objectConstructor.ConstructObject());
		}
	}

	/// <summary>
	/// Returns the next object.
	/// </summary>
	/// <param name="position"> The position to spawn the object at. </param>
	/// <returns> The next object in the pool. </returns>
	public SceneEntity GetEntity(Vector2 position)
	{
		SceneEntity entity = _objects.Dequeue();
		entity.position.position = position;
		_objects.Enqueue(entity);
		return entity;
	}


	#region Enumerable
	public IEnumerator<SceneEntity> GetEnumerator()
	{
		return _objects.GetEnumerator();
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
	#endregion
}