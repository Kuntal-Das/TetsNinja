using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests;

[TestFixture]
public class DemeritPointsCalculatorTests
{
    private DemeritPointsCalculator _demeritCalc;

    [SetUp]
    public void Setup()
    {
        _demeritCalc = new();
    }

    [Test]
    [TestCase(-1)]
    [TestCase(301)]
    public void CalculateDemeritPoints_SpeedIsOutOfRange_ThrowsArgumentOutOfRangeException(int speed)
    {
        Assert.That(() => _demeritCalc.CalculateDemeritPoints(speed),
            Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(64, 0)]
    [TestCase(65, 0)]
    [TestCase(66, 0)]
    [TestCase(70, 1)]
    [TestCase(73, 1)]
    [TestCase(75, 2)]
    public void CalculateDemeritPoints_WhenCalled_ReturnDemeritPoints(int speed, int expectedResult)
    {
        var result = _demeritCalc.CalculateDemeritPoints(speed);

        Assert.That(result, Is.EqualTo(expectedResult));
    }
}