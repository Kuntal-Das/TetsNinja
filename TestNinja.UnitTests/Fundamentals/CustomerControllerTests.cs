using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals;

[TestFixture]
public class CustomerControllerTests
{
    private CustomerController _controller;

    [SetUp]
    public void SetUp()
    {
        _controller = new CustomerController();
    }

    [Test]
    public void GetCustomer_IdIsZero_ReturnNotFound()
    {
        var result = _controller.GetCustomer(0);

        //NotFound Object
        Assert.That(result, Is.TypeOf<NotFound>());
        
        //NotFound Object or one of its derivatives
        // Assert.That(result, Is.InstanceOf<NotFound>());
    }

    [Test]
    public void GetCustomer_IdIsNotZero_ReturnOk()
    {
        var result = _controller.GetCustomer(3);

        //NotFound Object
        Assert.That(result, Is.TypeOf<Ok>());
    }
}