using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests;

[TestFixture]
public class HtmlFomatterTests
{
    [Test]
    public void FormatAsBold_WhenCalled_ShouldEncloseArgWithStrongElement()
    {
        var formatter = new HtmlFormatter();

        var result = formatter.FormatAsBold("abc");

        // Specific
        Assert.That(result, Is.EqualTo("<strong>abc</strong>").IgnoreCase);

        // More General
        // Assert.That(result, Does.StartWith("<strong>").IgnoreCase);
        // Assert.That(result, Does.EndWith("</strong>").IgnoreCase);
        // Assert.That(result, Does.Contain("abc"));
    }
}