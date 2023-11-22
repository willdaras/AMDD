using Microsoft.Xna.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace AMDD.ObjectPooling;

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