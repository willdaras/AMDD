using AMDD;
using AMDD.ECS;
using AMDD.SaveSystem.Loading;
using AMDD.SaveSystem.Storage;
using System.Diagnostics;

namespace AMDDTesting
{
	public class LevelLoaderTest
	{
		[Fact]
		public void TestLoad()
		{
			EntityMap entityMap = new EntityMap();
			Entity testEntity = new SceneEntity();
			entityMap.AddNewEntity(testEntity);

			ILevelLoader loader = new StringLevelLoader();
			ISaveInterpreter saveInterpreter = new SaveInterpreter(loader);

			loader.Save("testLevel", saveInterpreter.Write("testLevel", entityMap));

			Debug.WriteLine(loader.Load("testLevel"));

			EntityMap result = saveInterpreter.Interpret("testLevel");
			Assert.NotNull(result);
			Assert.Equivalent(result, entityMap);
		}

		private class StringLevelLoader : ILevelLoader
		{
			private string _level = "";

			public string Load(string levelName)
			{
				return _level;
			}

			public void Save(string levelName, string data)
			{
				_level = data;
			}
		}
	}
}
