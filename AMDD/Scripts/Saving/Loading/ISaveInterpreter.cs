using AMDD.ECS;
using AMDD.SaveSystem.Storage;

namespace AMDD.SaveSystem.Loading;

public interface ISaveInterpreter
{
	public EntityMap Interpret(string levelName);

	public string Write(string levelName, EntityMap map);
}