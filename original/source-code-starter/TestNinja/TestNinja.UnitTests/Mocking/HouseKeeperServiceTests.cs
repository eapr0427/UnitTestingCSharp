using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HouseKeeperServiceTests
    {
        HousekeeperService _service;
        Mock<IStatementGenerator> _statementGenerator;
        Mock<IEmailSender> _emailSender;
        Mock<IXtraMessageBox> _messageBox;
        DateTime _statementDate = new DateTime(1979, 4, 8);
        Housekeeper _houseKeeper;
        string _statementFileName;

        [SetUp]
        public void Setup()
        {
            _houseKeeper = new Housekeeper { Email = "fjps", FullName = "Jovis", Oid = 80002767, StatementEmailBody = "Love you bro" };

            var unitOfWork = new Mock<IUnitOfWork>();

            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(
                new List<Housekeeper>
                {
                    _houseKeeper
                }.AsQueryable());

            _statementFileName = "fileName";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator.Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                       .Returns(() => _statementFileName);

            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _messageBox.Object);
        }

        //This is an interaction test
        [Test]
        public void SendStatementEmails_WhenCalled_SaveStatements()
        {
            //ACT
            _service.SendStatementEmails(_statementDate);

            //ASSERT
            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate));
        }

        //[Test]
        //public void SendStatementEmails_HouseKeepersEmailIsNull_ShouldNotGenerateStatements()
        //{
        //    //ARRANGE
        //    _houseKeeper.Email = null;

        //    //ACT
        //    _service.SendStatementEmails(_statementDate);

        //    //ASSERT
        //    _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate),Times.Never);
        //}

        //[Test]
        //public void SendStatementEmails_HouseKeepersEmailIsWhiteSpace_ShouldNotGenerateStatements()
        //{
        //    //ARRANGE
        //    _houseKeeper.Email = " ";

        //    //ACT
        //    _service.SendStatementEmails(_statementDate);

        //    //ASSERT
        //    _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);
        //}

        //[Test]
        //public void SendStatementEmails_HouseKeepersEmailIsEmpty_ShouldNotGenerateStatements()
        //{
        //    //ARRANGE
        //    _houseKeeper.Email = "";

        //    //ACT
        //    _service.SendStatementEmails(_statementDate);

        //    //ASSERT
        //    _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);
        //}

        //Reemplazamos los 3 tests con el siguiente prueba parametrizada
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_WhenCalled_ShouldNotGenerateStatements(string email)
        {
            //ARRANGE
            _houseKeeper.Email = email;

            //ACT
            _service.SendStatementEmails(_statementDate);

            //ASSERT
            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            //ACT
            _service.SendStatementEmails(_statementDate);

            //ASSERT
            VerifyEmailSent();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_StatementFileNameIsNull_ShouldNotEmailTheStatement(string statementFileName)
        {
            _statementFileName = statementFileName;

            //ACT
            _service.SendStatementEmails(_statementDate);

            //ASSERT
            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
        {
            //Arrange
            _emailSender.Setup(es => es.EmailFile(It.IsAny<string>(),
                                                  It.IsAny<string>(),
                                                  It.IsAny<string>(),
                                                  It.IsAny<string>()
                            )).Throws<Exception>();

            //ACT
            _service.SendStatementEmails(_statementDate);

            //ASSERT
            VerifyMessageBoxDisplayed();
        }

      

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(es => es.EmailFile(It.IsAny<string>(),
                                            It.IsAny<string>(),
                                            It.IsAny<string>(),
                                            It.IsAny<string>()),
                                            Times.Never);
        }

        private void VerifyEmailSent()
        {
            _emailSender.Verify(es => es.EmailFile(_houseKeeper.Email,
                                                   _houseKeeper.StatementEmailBody,
                                                   _statementFileName, It.IsAny<string>()));
        }

        private void VerifyMessageBoxDisplayed()
        {
            _messageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }
    }
}
