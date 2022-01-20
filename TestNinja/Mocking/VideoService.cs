using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace TestNinja.Mocking;

public class VideoService
{
    private IFileReader _fileReader;
    private IVideoRepository _videoRepository;

    public VideoService(IFileReader fileReader = null, IVideoRepository repository = null)
    {
        _fileReader = fileReader ?? new FileReader();
        _videoRepository = repository ?? new VideoRepository();
        // _fileReader = fileReader;
        // _videoRepository = repository;
    }

    public string ReadVideoTitle()
    {
        var str = _fileReader.Read("video.txt");
        var video = JsonConvert.DeserializeObject<Video>(str);
        if (video == null)
            return "Error parsing the video.";
        return video.Title;
    }

    //[] => ""
    //[{},{},{}] => "1,2,3"
    public string GetUnprocessedVideosAsCsv()
    {
        var videoIds = new List<int>();

        var videos = _videoRepository.GetUnprocessedVideos();
        foreach (var v in videos)
            videoIds.Add(v.Id);

        return String.Join(",", videoIds);
    }
}

public class Video
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsProcessed { get; set; }
}

public class VideoContext : DbContext
{
    public DbSet<Video> Videos { get; set; }
}