
using System.Text.Json;

namespace TetrLang
{
    public class EventData
    {
        public int Frame { get; set; }
        public string? Type { get; set; }

        public override string ToString()
        {
            return $"{Frame}: {Type}";
        }
    }
}
