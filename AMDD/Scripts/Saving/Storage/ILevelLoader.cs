namespace AMDD.SaveSystem.Storage;

/// <summary>
/// A LevelLoader handles storing and retrieving the serialized save data.
/// </summary>
public interface ILevelLoader
{
	/// <summary>
	/// Loads a level's data.
	/// </summary>
	/// <param name="levelName"> The name of the level to load. </param>
	/// <returns> The serialized level data. </returns>
	public string Load(string levelName);

	/// <summary>
	/// Saves a level's data.
	/// </summary>
	/// <param name="levelName"> The name of the level to save as. </param>
	/// <param name="data"> The serialized level data to save. </param>
	public void Save(string levelName, string data);
}