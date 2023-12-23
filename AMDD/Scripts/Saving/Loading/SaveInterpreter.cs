using AMDD.ECS;
using AMDD.SaveSystem.Storage;
using System.Collections.Generic;
using System.Text.Json;

namespace AMDD.SaveSystem.Loading;

public class SaveInterpreter : ISaveInterpreter
{
	private ILevelLoader _levelLoader;

	public SaveInterpreter(ILevelLoader levelLoader)
	{
		_levelLoader = levelLoader;
	}

	public EntityMap Interpret(string levelName)
	{
		string data = _levelLoader.Load(levelName);
		IList<Entity> result = JsonSerializer.Deserialize<List<Entity>>(data);

		EntityMap entities = new EntityMap(result);

		return entities;
	}

	public string Write(string levelName, EntityMap map)
	{
		List<Entity> entities = new List<Entity>(map);

		string data = JsonSerializer.Serialize(entities);

		_levelLoader.Save(levelName, data);

		return data;
	}
}
