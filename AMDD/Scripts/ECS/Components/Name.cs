using Microsoft.Xna.Framework;

namespace AMDD.ECS.Components;

/// <summary>
/// The name of an Entity.
/// </summary>
public class Name : Component
{
	public string name { get; set; }

	public Name() => name = "default";
	public Name(string name) => this.name = name;
}