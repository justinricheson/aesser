using AESSER;
using AESSER.KeyExpansion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace AESSER_Tests
{
    [TestClass]
    public class KeyEnumeratorTests
    {
        // TODO update this test
        [TestMethod]
        public void ExpandKeyTest()
        {
            // Arrange
            var inputKey = new BitArray(128);
            for (int i = 0; i < inputKey.Length; i++)
            {
                inputKey[i] = true;
            }

            // Act
            var keys = KeyEnumerator.ExpandKey(inputKey, KeySize._128);

            // Assert TODO
            Assert.AreEqual(10, keys.Count);
            foreach (var key in keys)
            {
                Assert.AreEqual(128, key.Length);
                foreach (bool bit in key)
                {
                    Assert.IsTrue(bit);
                }
            }
        }
    }
}
