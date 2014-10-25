using System;
using System.Collections;

namespace AESSER.Validation
{
    internal static class ThrowHelper
    {
        internal static void CheckInputs(BitArray input, BitArray key, KeySize keysize)
        {
            ThrowOnNull(input, "input");
            ThrowOnNull(key, "key");
            ThrowOnBadInput(input);
            ThrowOnBadKey(key, keysize);
        }

        internal static void ThrowOnNull(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        internal static void ThrowOnBadInput(BitArray input)
        {
            if (input.Length % 128 != 0)
            {
                throw new InvalidBlockSizeException();
            }
        }

        internal static void ThrowOnBadKey(BitArray key, KeySize keysize)
        {
            if (key.Length % keysize.ConvertToInt() != 0)
            {
                throw new MismatchedKeySizeException();
            }
        }
    }
}
