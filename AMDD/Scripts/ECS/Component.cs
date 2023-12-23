using AMDD.ECS.Components;
using System.Text.Json.Serialization;

namespace AMDD.ECS;

/// <summary>
/// A component, a container for data.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "$component")]
[JsonDerivedType(typeof(Position), typeDiscriminator: "position")]
[JsonDerivedType(typeof(Name), typeDiscriminator: "name")]
[JsonDerivedType(typeof(Physics), typeDiscriminator: "physics")]
[JsonDerivedType(typeof(Collider), typeDiscriminator: "collider")]
[JsonDerivedType(typeof(Sprite), typeDiscriminator: "sprite")]
[JsonDerivedType(typeof(InstantiationBuffer), typeDiscriminator: "instantiation")]
public abstract class Component { }