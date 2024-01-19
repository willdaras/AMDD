using AMDD.ECS;
using AMDD.ECS.Components;
using AMDD.ECS.Components.UI;
using AMDD.LevelEditor.Components;
using AMDD.ObjectCreation;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace AMDD.LevelEditor;

public class SaveProcessor
{
	public static EntityMap ProcessSaveForSerialization(EntityMap entities)
	{
		List<Entity> entitiesList = entities.ToList();
		for (int i = 0; i < entitiesList.Count; i++)
		{
			if (entitiesList[i].TryGetComponent(out Player player))
			{
				/*SceneEntity newPlayer = new SceneEntity();
				newPlayer.position.position = entitiesList[i].GetComponent<Position>().position;
				newPlayer.AddComponent(new Player());
				entitiesList[i] = newPlayer;*/
				entitiesList.RemoveAt(i);
			}
			if (entitiesList[i].HasComponent<UIText>())
			{
				entitiesList.RemoveAt(i);
			}
			if (entitiesList[i].HasComponent<LevelPainter>())
			{
				entitiesList.RemoveAt(i);
			}
		}
		entities = new EntityMap(entitiesList);
		return entities;
	}
	public static EntityMap ProcessSaveForDeserialization(EntityMap entities)
	{
		List<Entity> entitiesList = entities.ToList();
		for (int i = 0; i < entitiesList.Count; i++)
		{
			if (entitiesList[i].TryGetComponent(out LevelPainter levelPainter))
			{
				entitiesList.RemoveAt(i);
			}
			if (entitiesList[i].TryGetComponent(out Name name))
			{
				if (name.name == "Cursor")
				{
					entitiesList.RemoveAt(i);
				}
			}
			if (entitiesList[i].TryGetComponent(out Sprite sprite))
			{
				sprite.image = GameData.contentManager.Load<Texture2D>(sprite.address);
			}
/*			if (entitiesList[i].TryGetComponent(out Player player))
			{
				SceneEntity newPlayer = new PlayerConstructor().ConstructObject();
				newPlayer.position.position = entitiesList[i].GetComponent<Position>().position;
				entitiesList[i] = newPlayer;
			}*/
		}
		entities = new EntityMap(entitiesList);
		return entities;
	}
}