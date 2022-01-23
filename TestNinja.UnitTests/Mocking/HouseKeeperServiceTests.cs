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
    private readonly string _statementFilename = "fileName";

    [SetUp]
    public void SetUp()
    {
        _houseKeeper = new Housekeeper {Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c"};
        _mockUnitOfwork = new Mock<IUnitOfWork>();
        _mockStatementGenerator = new Mock<IStatementGenerator>();
        _mockEmailSender = new Mock<IEmailSender>();
        _mockXtraMessageBox = new Mock<IXtraMessageBox>();

        _mockUnitOfwork
            .Setup(work => work.Query<Housekeeper>())
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

    [Test]
    public void SendStatementEmails_WhenCalled_EmailTheStatement()
    {
        _mockStatementGenerator
            .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _stmtDt))
            .Returns(_statementFilename);

        _houseKeeperService.SendStatementEmails(_stmtDt);

        _mockEmailSender.Verify(
            es => es.EmailFile(
                _houseKeeper.Email,
                _houseKeeper.StatementEmailBody,
                _statementFilename,
                It.IsAny<string>())
        );
    }

    [Test]
    [TestCase(null)]
    [TestCase(" ")]
    [TestCase("")]
    public void SendStatementEmails_StatementFileNameIs_ShouldNotEmailTheStatement(string statementFileName)
    {
        _mockStatementGenerator
            .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _stmtDt))
            .Returns(statementFileName);

        _houseKeeperService.SendStatementEmails(_stmtDt);

        _mockEmailSender.Verify(
            es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Never()
        );
    }

    [Test]
    public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
    {
        _mockStatementGenerator
            .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _stmtDt))
            .Returns(_statementFilename);

        _mockEmailSender
            .Setup(sender => sender.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
            .Throws<Exception>();

        _houseKeeperService.SendStatementEmails(_stmtDt);

        _mockXtraMessageBox.Verify(mb => mb.Show(
            It.IsAny<string>(),
            It.IsAny<string>(),
            MessageBoxButtons.OK));
    }
}