using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrLang
{
    internal struct KeyData
    {
        public enum KeyType
        {
            // Jump to next hold? cond jump?
            Hold,
            // Decrement data pointer (N held frames)
            MoveLeft,
            // Increment data pointer (N held frames)
            MoveRight,
            // Decrease data value (N held frames)
            RotateCCW,
            // Increase data value (N held frames)
            RotateCW,
            Rotate180,
            SoftDrop,
            // Output (N characters and increment data)
            HardDrop
        }
        public KeyType Key { get; set; }
        public float Subframe { get; set; }
    }
}
