using FluentAssertions;
using NUnit.Framework;

namespace BlackjackPOS.Tests.UnitTests;

[TestFixture]
public class ExampleUnitTest
{
    [Test]
    public void SimpleTestMethod()
    {
        int TestObject = 4;
        TestObject.Should().Be(4);
    }
}