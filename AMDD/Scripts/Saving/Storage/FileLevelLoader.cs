using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace AMDD.SaveSystem.Storage;

/// <summary>
/// A basic LevelLoader that saves and loads to a JSON file.
/// </summary>
/// <remarks> Saves files to the Levels folder inside content. </remarks>
public class FileLevelLoader : ILevelLoader
{
	private ContentManager _contentManager;

	public FileLevelLoader(ContentManager contentManager)
	{
		_contentManager = contentManager;
	}

	public string Load(string levelName)
	{
		string text;
		var filePath = Path.Combine(_contentManager.RootDirectory, "Levels/", levelName + ".json");
		using (var stream = TitleContainer.OpenStream(filePath))
		{
			using (var reader = new StreamReader(stream))
			{
				text = reader.ReadToEnd();
			}
		}
		return text;
	}

	public void Save(string levelName, string data)
	{
		var filePath = Path.Combine(_contentManager.RootDirectory, "Levels/", levelName + ".json");
		File.WriteAllText(filePath, data);
	}
}
