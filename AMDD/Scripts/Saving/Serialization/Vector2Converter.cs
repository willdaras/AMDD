using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AMDD.SaveSystem.Serialization;

public class Vector2Converter : JsonConverter<Vector2>
{
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

/*[JsonPolymorphic(TypeDiscriminatorPropertyName = "Vector2")]
public class VectorSubstitute
{
	public float X { get; set; }
	public float Y { get; set; }

	public static explicit operator Vector2(VectorSubstitute substitute)
	{
		Vector2 newVector = new Vector2(substitute.X, substitute.Y);
		return newVector;
	}

	public static explicit operator VectorSubstitute(Vector2 vector)
	{
		return new VectorSubstitute() { X = vector.X, Y = vector.Y };
	}
}*/