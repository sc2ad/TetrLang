namespace TetrLang
{
    public class ReplayData
    {
        public int Frames { get; set; }
        public List<EventData> Events { get; set; } = new();
    }
}