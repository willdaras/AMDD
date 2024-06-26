using System.Collections.Generic;

namespace AMDD.ECS.Components;

/// <summary>
/// A buffer of objects to instantiate before the next frame.
/// </summary>
public class InstantiationBuffer : Component
{
	public List<SceneEntity> entitiesToInstantiate { get; set; } = new List<SceneEntity>();

	public List<SceneEntity> entitiesToDestroy { get; set; } = new List<SceneEntity>();
}