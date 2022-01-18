using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class VideoServiceTests
{
    private VideoService _VideoService;

    // [SetUp]
    // public void SetUp()
    // {
    // }
    
    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnErrorMsg()
    {
        _VideoService = new VideoService(new FakeFileReader());
        
        var result = _VideoService.ReadVideoTitle();
        
        Assert.That(result, Does.Contain("error").IgnoreCase);
    }
}