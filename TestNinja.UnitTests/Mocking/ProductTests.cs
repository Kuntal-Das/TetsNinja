using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class ProductTests
{
    [Test]
    public void GetPrice_GoldCustomer_Apply30PercentDiscount()
    {
        var product = new Product() {ListPrice = 100};

        var result = product.GetPrice(new Customer() {IsGold = true});
        
        Assert.That(result, Is.EqualTo(70));
    } 
    
    //Abusing Mock ------------ NOT RECOMMENDED
    //use mock for removing external resources from the UnitTests 
    [Test]
    public void GetPrice_GoldCustomer_Apply30PercentDiscount2()
    {
        var mockCustomer = new Mock<ICustomer>();
        mockCustomer.Setup(c => c.IsGold).Returns(true);
        
        var product = new Product() {ListPrice = 100};

        var result = product.GetPrice(mockCustomer.Object);
        
        Assert.That(result, Is.EqualTo(70));
    } 
}