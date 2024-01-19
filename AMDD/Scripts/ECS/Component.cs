using AMDD.ECS.Components;
using AMDD.ECS.Components.UI;
using System.Text.Json.Serialization;

namespace AMDD.ECS;

/// <summary>
/// A component, a container for data.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "$component")]
[JsonDerivedType(typeof(Position), typeDiscriminator: "position")]
[JsonDerivedType(typeof(Name), typeDiscriminator: "name")]
[JsonDerivedType(typeof(Physics), typeDiscriminator: "physics")]
[JsonDerivedType(typeof(Gravity), typeDiscriminator: "gravity")]
[JsonDerivedType(typeof(Collider), typeDiscriminator: "collider")]
[JsonDerivedType(typeof(Static), typeDiscriminator: "static")]
[JsonDerivedType(typeof(Sprite), typeDiscriminator: "sprite")]
[JsonDerivedType(typeof(InstantiationBuffer), typeDiscriminator: "instantiation")]
[JsonDerivedType(typeof(Camera), typeDiscriminator: "camera")]
[JsonDerivedType(typeof(Player), typeDiscriminator: "player")]
[JsonDerivedType(typeof(Health), typeDiscriminator: "health")]
[JsonDerivedType(typeof(DamageBuffer), typeDiscriminator: "damagebuffer")]
[JsonDerivedType(typeof(FacingDirection), typeDiscriminator: "facingdirection")]
[JsonDerivedType(typeof(UIText), typeDiscriminator: "uitext")]
public abstract class Component { }