using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class EmployeeStorageTests
{
    private EmployeeController _empController;
    private Mock<IEmployeeStorage> _mockEmpStorage;

    [SetUp]
    public void SetUp()
    {
        _mockEmpStorage = new Mock<IEmployeeStorage>();
        _empController = new EmployeeController(_mockEmpStorage.Object);
    }
    
    [Test]
    public void DeleteEmployee_WhenCalled_DeleteTheEmployeeFromDb()
    {
        _empController.DeleteEmployee(1);

        _mockEmpStorage.Verify(s => s.DeleteEmployee(1));
    }

    [Test]
    public void DeleteEmployee_WhenCalled_ReturnsRedirectResult()
    {
        var result = _empController.DeleteEmployee(1);
        
        Assert.That(result, Is.InstanceOf<RedirectResult>());
    }
}