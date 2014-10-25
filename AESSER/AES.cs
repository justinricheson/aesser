using AESSER.KeyExpansion;
using AESSER.Validation;
using System.Collections;
using System.Collections.Generic;

namespace AESSER
{
    public class AES
    {
        public CipherMode Mode { get; private set; }
        public KeySize KeySize { get; private set; }
        public AES(CipherMode mode, KeySize keysize)
        {
            Mode = mode;
            KeySize = keysize;
        }

        public BitArray Encrypt(BitArray plainText, BitArray inputKey)
        {
            ThrowHelper.CheckInputs(plainText, inputKey, KeySize);

            var cipherTextBlocks = new List<BitArray>();
            var plainTextBlocks = plainText.SeparateBlocks();
            var keys = KeyEnumerator.ExpandKey(inputKey, KeySize);

            foreach (var plainTextBlock in plainTextBlocks)
            {
                var cipherTextBlock = plainTextBlock
                    .Copy().AddRoundKey(inputKey);

                for (int i = 0; i < KeySize.NumRounds(); i++)
                {
                    var key = keys[i];

                    if (i == plainTextBlocks.Count - 1)
                    {
                        cipherTextBlock = cipherTextBlock
                            .SubBytes()
                            .ShiftRows()
                            .AddRoundKey(key);
                    }
                    else
                    {
                        cipherTextBlock = cipherTextBlock
                            .SubBytes()
                            .ShiftRows()
                            .MixColumns()
                            .AddRoundKey(key);
                    }
                }

                cipherTextBlocks.Add(cipherTextBlock);
            }

            return cipherTextBlocks.CombineBlocks();
        }

        public BitArray Decrypt(BitArray cipherText, BitArray inputKey)
        {
            ThrowHelper.CheckInputs(cipherText, inputKey, KeySize);

            var plainTextBlocks = new List<BitArray>();
            var cipherTextBlocks = cipherText.SeparateBlocks();
            var keys = KeyEnumerator.ExpandKey(inputKey, KeySize);
            keys.Reverse();

            foreach (var cipherTextBlock in cipherTextBlocks)
            {
                var plainTextBlock = cipherTextBlock.Copy();

                for (int i = 0; i < KeySize.NumRounds(); i++)
                {
                    var key = keys[i];

                    if (i == 0)
                    {
                        plainTextBlock = plainTextBlock
                            .AddRoundKey(key)
                            .InvShiftRows()
                            .InvSubBytes();
                    }
                    else
                    {
                        plainTextBlock = plainTextBlock
                            .AddRoundKey(key)
                            .InvMixColumns()
                            .InvShiftRows()
                            .InvSubBytes();
                    }
                }

                plainTextBlocks.Add(plainTextBlock
                    .AddRoundKey(inputKey));
            }

            return plainTextBlocks.CombineBlocks();
        }
    }
}
