using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

public class FakeFileReader : IFileReader
{
    public string Read(string path)
    {
        return "";
    }
}