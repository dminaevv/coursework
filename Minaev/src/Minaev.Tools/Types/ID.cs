using System.Text.Json.Serialization;
using System.Text.Json;

namespace Minaev.Tools.Types;

public record ID(Int64 Value)
{
    public static implicit operator ID(Int64 id) => new(id);
    public static implicit operator Int64(ID id) => id.Value;
    public static implicit operator ID(String id) => new(Convert.ToInt64(id));

    public static ID New()
    {
        Random rnd = new();
        return rnd.NextInt64(1, Int64.MaxValue);
    }

    public static ID Robot()
    {
        return 1_000_000_000_000_000_000;
    }
}

public class IDConverter : JsonConverter<ID>
{
    public override ID Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new ID(reader.GetInt64());
    }

    public override void Write(Utf8JsonWriter writer, ID id, JsonSerializerOptions options)
    {
        writer.WriteStringValue(id.Value.ToString());
    }
}