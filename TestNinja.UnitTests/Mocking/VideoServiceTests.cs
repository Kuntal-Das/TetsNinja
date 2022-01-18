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
        _VideoService.FileReader = new FakeFileReader();
        
        var result = _VideoService.ReadVideoTitle();
        
        Assert.That(result, Does.Contain("error").IgnoreCase);
    }
}