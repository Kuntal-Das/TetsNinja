using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class HouseKeeperServiceTests
{
    private Mock<IUnitOfWork> _mockUnitOfwork;
    private Mock<IStatementGenerator> _mockStatementGenerator;
    private Mock<IEmailSender> _mockEmailSender;
    private Mock<IXtraMessageBox> _mockXtraMessageBox;
    private HouseKeeperService _houseKeeperService;

    [SetUp]
    public void SetUp()
    {
        _mockUnitOfwork = new Mock<IUnitOfWork>();
        _mockStatementGenerator = new Mock<IStatementGenerator>();
        _mockEmailSender = new Mock<IEmailSender>();
        _mockXtraMessageBox = new Mock<IXtraMessageBox>();

        _houseKeeperService = new HouseKeeperService(
            _mockUnitOfwork.Object,
            _mockStatementGenerator.Object,
            _mockEmailSender.Object,
            _mockXtraMessageBox.Object);
    }

    [Test]
    public void SendStatementEmails_WhenCalled_GenerateStatements()
    {
        _mockUnitOfwork
            .Setup(uow => uow.Query<Housekeeper>())
            .Returns(new List<Housekeeper>
            {
                new Housekeeper {Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c"}
            }.AsQueryable());
        var stmtDt = new DateTime(2022, 1, 1);
        _houseKeeperService.SendStatementEmails(stmtDt);

        _mockStatementGenerator.Verify(sg =>
            sg.SaveStatement(1, "b", stmtDt));
    }
}