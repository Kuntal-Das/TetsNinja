namespace TestNinja.Mocking;

public class Programme
{
    public static void Main()
    {
        var service = new VideoService();
        var title = service.ReadVideoTitle(new FileReader());
    }
}