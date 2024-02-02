using AMDD.ECS;
using AMDD.SaveSystem.Serialization;
using AMDD.SaveSystem.Storage;
using System.Collections.Generic;
using System.Text.Json;

namespace AMDD.SaveSystem.Loading;

/// <summary>
/// A basic implementation of a SaveInterpreter that reads the EntityMap and saves and loads to JSON.
/// </summary>
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
		var options = new JsonSerializerOptions
		{
			WriteIndented = true,
			IncludeFields = true,
			Converters = { new Vector2Converter() }
		};
		List<Entity> result = JsonSerializer.Deserialize<List<Entity>>(data, options);

		EntityMap entities = new EntityMap(result);

		return entities;
	}

	public string Write(string levelName, EntityMap map)
	{
		List<Entity> entities = new List<Entity>(map);

		var options = new JsonSerializerOptions
		{
			WriteIndented = true,
			IncludeFields = true,
			Converters = { new Vector2Converter() }
		};

		string data = JsonSerializer.Serialize(entities, options);

		_levelLoader.Save(levelName, data);

		return data;
	}
}
