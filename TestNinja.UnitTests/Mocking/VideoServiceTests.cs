using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class VideoServiceTests
{
    private VideoService _videoService;
    private Mock<IFileReader> _mockFileReader;
    private Mock<IVideoRepository> _mockVideoRepo;

    [SetUp]
    public void SetUp()
    {
        _mockFileReader = new Mock<IFileReader>();
        _mockVideoRepo = new Mock<IVideoRepository>();
        _videoService = new VideoService(_mockFileReader.Object, _mockVideoRepo.Object);
    }

    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnErrorMsg()
    {
        _mockFileReader.Setup(fr => fr.Read("video.txt")).Returns("");

        var result = _videoService.ReadVideoTitle();

        Assert.That(result, Does.Contain("error").IgnoreCase);
    }

    [Test]
    public void GetUnprocessedVideosAsCsv_AllVideosProcessed_ReturnAnEmptyString()
    {
        _mockVideoRepo.Setup(repo => repo.GetUnprocessedVideos()).Returns(new List<Video>());

        var result = _videoService.GetUnprocessedVideosAsCsv();

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void GetUnprocessedVideosAsCsv_AFewUnProcessedVideos_ReturnAStringWithIdOfUnProcessedVideos()
    {
        _mockVideoRepo.Setup(repo => repo.GetUnprocessedVideos()).Returns(new List<Video>()
        {
            new Video {Id = 1},
            new Video {Id = 2},
            new Video {Id = 3},
        });

        var result = _videoService.GetUnprocessedVideosAsCsv();

        Assert.That(result, Is.EqualTo("1,2,3"));
    }
}