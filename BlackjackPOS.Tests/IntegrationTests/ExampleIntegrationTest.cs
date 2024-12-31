using FluentAssertions;
using NUnit.Framework;

namespace BlackjackPOS.Tests.IntegrationTests;

[TestFixture]
public class ExampleIntegrationTest
{
    [Test]
    public void SimpleTestMethod()
    {
        
        int testNumber = 1;
        testNumber.Should().Be(1);
        
    }
}