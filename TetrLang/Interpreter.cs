using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrLang
{
    public class Interpreter
    {
        private readonly Action<string> Write;
        private readonly Replay replay;

        // Data is a 10k byte sequence
        private readonly byte[] data = new byte[10000];
        // Ptr will start in the middle.
        private int dataIndex;
        public Interpreter(Replay replay, Action<string> write)
        {
            this.replay = replay;
            Write = write;
            dataIndex = data.Length / 2;
        }
        private void ParseCommand(KeyData.KeyType type, int fDelta)
        {
            switch (type)
            {
                case KeyData.KeyType.MoveLeft:
                    dataIndex -= fDelta;
                    dataIndex = (dataIndex % data.Length + data.Length) % data.Length;
                    break;
                case KeyData.KeyType.MoveRight:
                    dataIndex += fDelta;
                    dataIndex %= data.Length;
                    break;
                case KeyData.KeyType.RotateCCW:
                    data[dataIndex] -= (byte)fDelta;
                    break;
                case KeyData.KeyType.RotateCW:
                    data[dataIndex] += (byte)fDelta;
                    break;
                case KeyData.KeyType.HardDrop:
                    var charData = new char[fDelta];
                    for (int i = 0; i < fDelta; i++)
                    {
                        charData[i] = Convert.ToChar(data[dataIndex++]);
                    }
                    Write(new string(charData));
                    break;
                default:
                    // DO nothing
                    break;
            }
        }
        public void Run()
        {
            Write($"Running TETR Program from: {replay.Ts}!");
            if (replay.Data is null)
                throw new InvalidOperationException("Cannot run an interpretation of a null data replay!");

            // Now we need to walk our events and create our commands
            Dictionary<KeyData.KeyType, int> frameData = new();
            
            foreach (var e in replay.Data.Events)
            {
                if (e is KeyDownEvent ked)
                {
                    // Key down events are simply added to frame data
                    frameData.Add(ked.Data.Key, ked.Frame);
                }
                else if (e is KeyUpEvent keu)
                {
                    int delta = keu.Frame - frameData[keu.Data.Key];
                    ParseCommand(keu.Data.Key, delta);
                    frameData.Remove(keu.Data.Key);
                }
            }
            Write("Program Complete!");
        }
    }
}
