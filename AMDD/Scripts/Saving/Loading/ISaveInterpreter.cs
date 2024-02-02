using AMDD.ECS;
using AMDD.SaveSystem.Storage;

namespace AMDD.SaveSystem.Loading;

/// <summary>
/// A SaveInterpreter handles loading the EntityMap from a save.
/// </summary>
public interface ISaveInterpreter
{
	/// <summary>
	/// Returns the level specified as an EntitymMap.
	/// </summary>
	/// <param name="levelName"> The name of the level to load. </param>
	/// <returns> The EntityMap of the level specified. </returns>
	public EntityMap Interpret(string levelName);

	/// <summary>
	/// Writes an EntityMap to a save.
	/// </summary>
	/// <param name="levelName"> The name of the level to save as. </param>
	/// <param name="map"> The EntityMap to save. </param>
	/// <returns> The serialized EntityMap as a string. </returns>
	public string Write(string levelName, EntityMap map);
}