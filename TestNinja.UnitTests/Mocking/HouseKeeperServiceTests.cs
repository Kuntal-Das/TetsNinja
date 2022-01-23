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

    private DateTime _stmtDt = new DateTime(2022, 1, 1);
    private Housekeeper _houseKeeper;

    [SetUp]
    public void SetUp()
    {
        _houseKeeper = new Housekeeper {Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c"};
        _mockUnitOfwork = new Mock<IUnitOfWork>();
        _mockStatementGenerator = new Mock<IStatementGenerator>();
        _mockEmailSender = new Mock<IEmailSender>();
        _mockXtraMessageBox = new Mock<IXtraMessageBox>();

        _mockUnitOfwork
            .Setup(uow => uow.Query<Housekeeper>())
            .Returns(new List<Housekeeper> {_houseKeeper}.AsQueryable());

        _houseKeeperService = new HouseKeeperService(
            _mockUnitOfwork.Object,
            _mockStatementGenerator.Object,
            _mockEmailSender.Object,
            _mockXtraMessageBox.Object);
    }

    [Test]
    public void SendStatementEmails_WhenCalled_GenerateStatements()
    {
        _houseKeeperService.SendStatementEmails(_stmtDt);

        _mockStatementGenerator.Verify(sg =>
            sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _stmtDt));
    }

    [Test]
    [TestCase(null)]
    [TestCase(" ")]
    [TestCase("")]
    public void SendStatementEmails_WhenHouseKeepersEmailIs_ShouldNotGenerateStament(string email)
    {
        _houseKeeper.Email = email;

        _houseKeeperService.SendStatementEmails(_stmtDt);

        _mockStatementGenerator.Verify(sg =>
                sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _stmtDt)
            , Times.Never);
    }
}