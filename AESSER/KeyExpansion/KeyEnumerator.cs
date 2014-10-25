using System.Collections;
using System.Collections.Generic;

namespace AESSER.KeyExpansion
{
    internal static class KeyEnumerator
    {
        internal static List<BitArray> ExpandKey(BitArray key, KeySize keySize)
        {
            int rounds = keySize.NumRounds();
            int length = keySize.ConvertToInt();
            var keys = new BitArray(rounds * length);

            var prev = key;
            for (int i = 0; i < rounds; i++)
            {
                var next = prev.Next(i + 1);
                next.CopyTo(keys, length * i);
            }

            return keys.SeparateBlocks();
        }
    }
}
