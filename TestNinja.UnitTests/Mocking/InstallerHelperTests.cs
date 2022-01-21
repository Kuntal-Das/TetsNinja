using System.Net;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class InstallerHelperTests
{
    private Mock<IFileDownloader> _fileDownloader;
    private InstallHelper _installerHelper;

    [SetUp]
    public void SetUp()
    {
        _fileDownloader = new Mock<IFileDownloader>();
        _installerHelper = new InstallHelper(_fileDownloader.Object);
    }

    [Test]
    public void DownloadInstaller_DownloadFails_ReturnFalse()
    {
        _fileDownloader
            .Setup(fd =>
                fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>())
            ).Throws<WebException>();

        var result = _installerHelper.DownloadInstaller("customer", "installer");

        Assert.That(result, Is.False);
    }

    [Test]
    public void DownloadInstaller_DownloadCompletes_ReturnTrue()
    {
        var result = _installerHelper.DownloadInstaller("customer", "installer");

        Assert.That(result, Is.True);
    }
}