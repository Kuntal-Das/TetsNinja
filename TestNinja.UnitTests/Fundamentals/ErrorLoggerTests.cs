using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals;

[TestFixture]
public class ErrorLoggerTests
{
    private ErrorLogger _logger;

    [SetUp]
    public void SetUp()
    {
        _logger = new();
    }

    [Test]
    public void Log_WhenCalled_SetTheLastErrorProperty()
    {
        _logger.Log("a");
        Assert.That(_logger.LastError, Is.EqualTo("a"));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void Log_InvalidError_ThrowArgumentNullException(string error)
    {
        // _logger.Log(error);

        Assert.That(() => _logger.Log(error), Throws.ArgumentNullException);
        // Assert.That(() => _logger.Log(error), Throws.Exception.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void Log_ValidError_RaiseErrorLoggedEvent()
    {
        var id = Guid.Empty;
        //subscribe to the event
        _logger.ErrorLogged += (sender, args) => { id = args; };
        
        _logger.Log("a");
        
        Assert.That(id,Is.Not.EqualTo(Guid.Empty));
    }
    
}