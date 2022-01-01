
namespace TetrLang
{
    internal class KeyEvent : EventData
    {
        public KeyData Data { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" - {Data.Key} at: {Data.Subframe}";
        }
    }
}
