using System;

namespace AESSER
{
    public enum KeySize
    {
        _128,
        _192,
        _256
    }

    internal static class KeySizeExtensions
    {
        internal static int ConvertToInt(this KeySize keysize)
        {
            switch (keysize)
            {
                case KeySize._128: return 128;
                case KeySize._192: return 192;
                case KeySize._256: return 256;
            }

            throw new ArgumentException("Unknown KeySize");
        }

        internal static int NumRounds(this KeySize keysize)
        {
            switch (keysize)
            {
                case KeySize._128: return 10;
                case KeySize._192: return 12;
                case KeySize._256: return 14;
            }

            throw new ArgumentException("Unknown KeySize");
        }
    }
}
