using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace AMDD.ECS;

public abstract class System
{
	public abstract Type[] RequiredComponents { get; }
	public abstract void Update(EntityMap entities, float deltaTime);
}