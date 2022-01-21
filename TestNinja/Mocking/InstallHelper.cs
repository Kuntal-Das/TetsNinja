using System.Net;

namespace TestNinja.Mocking;

public class InstallHelper
{
    private string _setupDestinationFile;
    private readonly IFileDownloader _fileDownloader;

    public InstallHelper(IFileDownloader fileDownloader)
    {
        _fileDownloader = fileDownloader;
    }

    public bool DownloadInstaller(string customerName, string installerName)
    {
        try
        {
            // await _fileDownloader.DownloadFile($"$http://example.com/{customerName}/{installerName}", _setupDestinationFile);
            _fileDownloader.DownloadFile($"http://example.com/{customerName}/{installerName}", _setupDestinationFile);
            
            return true;
        }
        catch (WebException)
        {
            return false;
        }
    }
}