using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class OrderServiceTests
{
    private Mock<IStorage> _mockStoage;
    private OrderService _orderService;

    [SetUp]
    public void SetUp()
    {
        _mockStoage = new Mock<IStorage>();
        _orderService = new OrderService(_mockStoage.Object);
    }

    [Test]
    public void PlaceOrder_WhenCalled_StoreTheOrder()
    {
        var order = new Order();
        _orderService.PlaceOrder(order);
        
        _mockStoage.Verify(s => s.Store(order));
    }
}