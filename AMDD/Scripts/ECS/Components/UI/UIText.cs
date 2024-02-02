using System.Text.Json.Serialization;

namespace AMDD.ECS.Components.UI;

/// <summary>
/// Indicates that text should be drawn as UI.
/// </summary>
public class UIText : Component
{
	public string text { get; set; } = "";
}