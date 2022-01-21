using System.Net;

namespace TestNinja.Mocking;

public interface IFileDownloader
{
    void DownloadFile(string url, string downloadPath);
}

public class FileDownloader : IFileDownloader
{
    private readonly HttpClient _httpClient;

    public FileDownloader()
    {
        _httpClient = new();
    }
    
    public void DownloadFile(string url, string downloadPath)
    {
        var client = new WebClient();
        client.DownloadFile(url,downloadPath);
    }

    public async Task DownloadFileAsync(string url, string downloadPath)
    {
        Uri uriResult;

        if (!Uri.TryCreate(url, UriKind.Absolute, out uriResult))
            throw new InvalidOperationException("URI is invalid");

        var responseByteArr = await _httpClient.GetByteArrayAsync(uriResult);
        // using (var file = new FileStream(downloadPath, FileMode.CreateNew))
        // {
        //     // await response.Content.CopyToAsync(file);
        // }

        File.WriteAllBytes(downloadPath, responseByteArr);
    }
}