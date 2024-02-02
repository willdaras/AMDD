namespace AMDD.ObjectCreation;

/// <summary>
/// Constructs a SceneObject of some kind - for more complex Entities.
/// </summary>
public interface IObjectConstructor
{
	SceneEntity ConstructObject();
}