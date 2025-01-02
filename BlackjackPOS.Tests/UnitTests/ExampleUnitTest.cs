using FluentAssertions;
using NUnit.Framework;

namespace BlackjackPOS.Tests.UnitTests;

[TestFixture]
public class ExampleUnitTest
{
    [Test]
    public void SimpleTestMethod()
    {
        int testObject = 4;
        testObject.Should().Be(4);
    }
}