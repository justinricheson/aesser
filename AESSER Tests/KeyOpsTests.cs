using AESSER.KeyExpansion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace AESSER_Tests
{
    [TestClass]
    public class KeyOpsTests
    {
        [TestMethod]
        public void RotWordTest()
        {
            // Arrange
            bool bit = false;
            var input = new BitArray(128);
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = bit;
                bit = (i + 1) % 32 == 0 ? !bit : bit;
            }

            // Act
            var output = new BitArray(128).RotWord(input);

            // Assert
            Assert.AreEqual(128, output.Length);

            for (int i = 24; i < 32; i++)
            {
                Assert.IsTrue(output[i]);
            }
            for (int i = 56; i < 64; i++)
            {
                Assert.IsFalse(output[i]);
            }
            for (int i = 88; i < 96; i++)
            {
                Assert.IsTrue(output[i]);
            }
            for (int i = 120; i < 128; i++)
            {
                Assert.IsFalse(output[i]);
            }
        }
    }
}
