using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TetrLang
{
    public class Parser
    {
        static JsonSerializerOptions opts;
        static Parser()
        {
            opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            opts.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
            opts.Converters.Add(new EventDataConverter());
        }
        public static bool TryParseFile(Stream fs, out Replay? data)
        {
            try
            {
                data = JsonSerializer.Deserialize<Replay>(fs, opts);
                return true;
            }
            catch (JsonException)
            {
                data = null;
                return false;
            }
        }
    }
}
