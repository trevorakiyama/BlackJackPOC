using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace BlackjackPOS.Tests
{
    [TestFixture]
    public class SimpleTest
    {
        [Test]
        public void SimpleTestMethod()
        {
            ClassicAssert.AreEqual(1 + 1, 2);
            //Assert.Equals(1, 1);
            int testNumber = 5;
            testNumber.Should().Be(5);
        }
    }
}