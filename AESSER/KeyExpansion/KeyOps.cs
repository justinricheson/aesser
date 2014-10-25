using System.Collections;
using System.Linq;

namespace AESSER.KeyExpansion
{
    internal static class KeyOps
    {
        internal static BitArray Next(this BitArray prev, int round)
        {
            return new BitArray(128)
                .RotWord(prev)
                .SubBytes()
                .Rcon(prev, round)
                .XorColumns(prev);
        }

        internal static BitArray RotWord(this BitArray curr, BitArray prev)
        {
            var byte1 = prev.SubArray(24, 8);
            var byte2 = prev.SubArray(56, 8);
            var byte3 = prev.SubArray(88, 8);
            var byte4 = prev.SubArray(120, 8);

            byte1.CopyTo(curr, 96);
            byte2.CopyTo(curr, 0);
            byte3.CopyTo(curr, 32);
            byte4.CopyTo(curr, 64);

            return curr;
        }

        internal static BitArray SubBytes(this BitArray curr)
        {
            SubByte(curr, 0);
            SubByte(curr, 32);
            SubByte(curr, 64);
            SubByte(curr, 96);

            return curr;
        }
        private static void SubByte(BitArray curr, int start)
        {
            var xy1 = curr.SubArray(start, 8).Split(4)
                .Select(n => n.ToInt()).ToArray();
            var sub1 = Sbox.GetForward(xy1[0], xy1[1]);
            sub1.ToBitArray().CopyTo(curr, start);
        }

        internal static BitArray Rcon(this BitArray curr, BitArray prev, int round)
        {
            var zero = 0.ToBitArray();
            var rcon = AESSER.KeyExpansion.Rcon.Get(round).ToBitArray();

            Rcon(curr, prev, rcon, 0);
            Rcon(curr, prev, zero, 32);
            Rcon(curr, prev, zero, 64);
            Rcon(curr, prev, zero, 96);

            return curr;
        }
        private static void Rcon(BitArray curr, BitArray prev, BitArray rcon, int offset)
        {
            var prev1 = prev.SubArray(offset, 8);
            var curr1 = curr.SubArray(offset, 8);
            var xor1 = prev1.Xor(curr1);
            var xor2 = xor1.Xor(rcon);
            xor2.CopyTo(curr, offset);
        }

        internal static BitArray XorColumns(this BitArray curr, BitArray prev)
        {
            for (int i = 0; i < 4; i++)
            {
                var offset = i * 8;
                XorByte(curr, prev, 0 + offset, 8 + offset);
                XorByte(curr, prev, 32 + offset, 40 + offset);
                XorByte(curr, prev, 64 + offset, 72 + offset);
                XorByte(curr, prev, 96 + offset, 104 + offset);
            }

            return curr;
        }
        private static void XorByte(BitArray curr, BitArray prev, int currOffset, int prevOffset)
        {
            var prev1 = prev.SubArray(prevOffset, 8);
            var curr1 = curr.SubArray(currOffset, 8);
            var xor = prev1.Xor(curr1);
            xor.CopyTo(curr, currOffset + 8);
        }
    }
}
