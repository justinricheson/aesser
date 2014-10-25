using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AESSER
{
    internal static class Extensions
    {
        internal static List<BitArray> SeparateBlocks(this BitArray input)
        {
            var blocks = new List<BitArray>();

            for (int i = 0; i < input.Length; i += 128)
            {
                var block = new BitArray(128);
                for (int j = 0; j < 128; j++)
                {
                    block[j] = input[i + j];
                }

                blocks.Add(block);
            }

            return blocks;
        }

        internal static BitArray CombineBlocks(this IEnumerable<BitArray> input)
        {
            var list = input.ToList();
            var combined = new BitArray(list.Count * 128);

            for (int i = 0; i < list.Count; i++)
            {
                int blockOffset = i * 128;
                for (int j = 0; j < 128; j++)
                {
                    combined[blockOffset + j] = list[i][j];
                }
            }

            return combined;
        }

        internal static BitArray Copy(this BitArray input)
        {
            var copy = new BitArray(input.Count);

            for (int i = 0; i < input.Length; i++)
            {
                copy[i] = input[i];
            }

            return copy;
        }

        internal static void CopyTo(this BitArray input, BitArray output, int start)
        {
            for (int i = 0; i < input.Length; i++)
            {
                output[start + i] = input[i];
            }
        }

        internal static BitArray SubArray(this BitArray input, int start, int length)
        {
            var copy = new BitArray(length);

            for (int i = 0; i < length; i++)
            {
                copy[i] = input[start + i];
            }

            return copy;
        }

        internal static List<BitArray> Split(this BitArray input, int size)
        {
            var split = new List<BitArray>();

            int numSplit = input.Length / size;
            for (int i = 0; i < numSplit; i++)
            {
                var offset = i * size;
                var next = new BitArray(size);
                for (int j = 0; j < size; j++)
                {
                    next[j] = input[j + offset];
                }
                split.Add(next);
            }

            return split;
        }

        internal static int ToInt(this BitArray input)
        {
            return 0; // TODO
        }

        internal static BitArray ToBitArray(this int input)
        {
            return null; // TODO
        }
    }
}
