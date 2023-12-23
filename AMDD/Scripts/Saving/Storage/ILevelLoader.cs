namespace AMDD.SaveSystem.Storage;
public interface ILevelLoader
{
	public string Load(string levelName);

	public void Save(string levelName, string data);
}