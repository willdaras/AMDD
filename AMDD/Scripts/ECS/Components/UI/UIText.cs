using System.Text.Json.Serialization;

namespace AMDD.ECS.Components.UI;

public class UIText : Component
{
	public string text { get; set; } = "";
}