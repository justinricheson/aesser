using AESSER;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;

namespace AESSER_Tests
{
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void SeparateBlocksTest()
        {
            // Arrange
            var input = new BitArray(256);
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = true;
            }

            // Act
            var blocks = input.SeparateBlocks();

            // Assert
            Assert.AreEqual(2, blocks.Count);

            foreach (var block in blocks)
            {
                Assert.AreEqual(128, block.Length);
                foreach (bool bit in block)
                {
                    Assert.IsTrue(bit);
                }
            }
        }

        [TestMethod]
        public void CombineBlocksTest()
        {
            // Arrange
            var input = new List<BitArray>
            {
                new BitArray(128),
                new BitArray(128)
            };

            foreach (var block in input)
            {
                for (int i = 0; i < block.Length; i++)
                {
                    block[i] = true;
                }
            }

            // Act
            var combined = input.CombineBlocks();

            // Assert
            Assert.AreEqual(256, combined.Length);
            foreach (bool bit in combined)
            {
                Assert.IsTrue(bit);
            }
        }

        [TestMethod]
        public void CopyTest()
        {
            // Arrange
            var input = new BitArray(128);
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = true;
            }

            // Act
            var copy = input.Copy();

            // Assert
            Assert.AreEqual(128, copy.Length);
            foreach (bool bit in copy)
            {
                Assert.IsTrue(bit);
            }
        }

        [TestMethod]
        public void CopyToTest()
        {
            // Arrange
            var input = new BitArray(128);
            var output = new BitArray(256);

            for (int i = 0; i < input.Length; i++)
            {
                input[i] = true;
            }

            // Act
            input.CopyTo(output, 128);

            // Assert
            for (int i = 0; i < 128; i++)
            {
                Assert.IsFalse(output[i]);
            }
            for (int i = 128; i < 256; i++)
            {
                Assert.IsTrue(output[i]);
            }
        }

        [TestMethod]
        public void SubArrayTest()
        {
            // Arrange
            var input = new BitArray(256);
            for (int i = 128; i < input.Length; i++)
            {
                input[i] = true;
            }

            // Act
            var output = input.SubArray(64, 128);

            // Assert
            Assert.AreEqual(128, output.Length);
            for (int i = 0; i < 64; i++)
            {
                Assert.IsFalse(output[i]);
            }
            for (int i = 64; i < 128; i++)
            {
                Assert.IsTrue(output[i]);
            }
        }
    }
}
