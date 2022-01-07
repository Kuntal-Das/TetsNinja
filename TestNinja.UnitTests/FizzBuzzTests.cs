using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests;

[TestFixture]
public class FizzBuzzTests
{
    [Test]
    [TestCase(19,"19")]
    [TestCase(3,"Fizz")]
    [TestCase(5,"Buzz")]
    [TestCase(15,"FizzBuzz")]
    public void GetOutput_WhenCalled_ReturnsString(int number, string expectedResult)
    {
        var result = FizzBuzz.GetOutput(number);
        
        Assert.That(result, Is.EqualTo(expectedResult).IgnoreCase);
    }
}