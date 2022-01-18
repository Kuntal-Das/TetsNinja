using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class VideoServiceTests
{
    private VideoService _VideoService;

    [SetUp]
    public void SetUp()
    {
        _VideoService = new VideoService();
    }
    
    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnErrorMsg()
    {
        var result = _VideoService.ReadVideoTitle(new FakeFileReader());
        
        Assert.That(result, Does.Contain("error").IgnoreCase);
    }
}