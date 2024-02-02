using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AMDD.SaveSystem.Serialization;

/// <summary>
/// A custom converter for Vector2 object as the JsonSerializer has trouble serializing and deserializing them.
/// </summary>
public class Vector2Converter : JsonConverter<Vector2>
{
	/// <summary>
	/// Converts the JSON to a Vector2.
	/// </summary>
	/// <returns> The deserialized Vector2. </returns>
	/// <exception cref="JsonException"></exception>
	public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		Debug.WriteLine("Trying to deserialize Vector2");
		if (reader.TokenType != JsonTokenType.StartObject)
		{
			throw new JsonException();
		}

		float x = 0;
		float y = 0;

		string typeDiscriminator = null;

		while (reader.Read())
		{
			if (reader.TokenType == JsonTokenType.EndObject)
			{
				if (typeDiscriminator != "Vector2")
				{
					throw new JsonException($"Unexpected type discriminator: {typeDiscriminator}");
				}

				Vector2 deserializedVector = new Vector2(x, y);
				Debug.WriteLine($"Deserialized Vector: {deserializedVector}");
				return deserializedVector;
			}

			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				string propertyName = reader.GetString();
				reader.Read(); // Move to the value of the property

				if (propertyName == "$TypeDiscriminator")
				{
					typeDiscriminator = reader.GetString();
				}
				else if (propertyName == "X")
				{
					x = reader.GetSingle();
				}
				else if (propertyName == "Y")
				{
					y = reader.GetSingle();
				}
			}
		}

		throw new JsonException("Unexpected JSON structure");
	}

	/// <summary>
	/// Writes the Vector2 to JSON.
	/// </summary>
	public override void Write(Utf8JsonWriter writer, Vector2 value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		Debug.WriteLine($"Serialized Vector: {value}");
		writer.WritePropertyName("$TypeDiscriminator");
		JsonSerializer.Serialize(writer, "Vector2", options);
		writer.WritePropertyName("X");
		JsonSerializer.Serialize(writer, value.X, options);
		writer.WritePropertyName("Y");
		JsonSerializer.Serialize(writer, value.Y, options);
		writer.WriteEndObject();
	}
}