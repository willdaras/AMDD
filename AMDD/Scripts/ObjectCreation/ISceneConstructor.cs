namespace AMDD.ObjectCreation;

/// <summary>
/// Constructs a Scene - populates a scene with Entities and Systems.
/// </summary>
/// <remarks> Used for templates for more complex Scenes </remarks>
public interface ISceneConstructor
{
	Scene ConstructScene();
}