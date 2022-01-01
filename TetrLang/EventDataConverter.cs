using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TetrLang
{
    internal class EventDataConverter : JsonConverter<EventData>
    {
        public override EventData? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Read left brace
            reader.Read();
            // Skip frame name
            reader.Read();
            var frame = reader.GetInt32();
            reader.Read();
            // Skip type name
            reader.Read();
            var type = reader.GetString();
            reader.Read();
            // Read past 'data' label
            reader.Read();
            EventData inst;
            switch (type)
            {
                case "keydown":
                    inst = new KeyDownEvent() { Data = JsonSerializer.Deserialize<KeyData>(ref reader, options) };
                    break;
                case "keyup":
                    inst = new KeyUpEvent() { Data = JsonSerializer.Deserialize<KeyData>(ref reader, options) };
                    break;
                default:
                    inst = new EventData();
                    JsonSerializer.Deserialize<JsonElement>(ref reader, options);
                    break;
            }
            inst.Frame = frame;
            inst.Type = type;
            // Read right brace
            reader.Read();
            return inst;
        }

        public override void Write(Utf8JsonWriter writer, EventData value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
