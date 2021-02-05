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
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeStorage> _employeeStorage;
        private EmployeeController _employeeController;

        [SetUp]
        public void SetUp()
        {
            _employeeStorage = new Mock<IEmployeeStorage>();
            _employeeController = new EmployeeController(_employeeStorage.Object);
        }

        [Test]
        public void DeleteEmployee_WhenCalled_DeleteTheEmployeeFromDb()
        {
            //Arrange
            //_employeeStorage.Setup

            //Act
            _employeeController.DeleteEmployee(1);

            // En este caso no nos importa el resultado, solo queremos probar la interacción de este controlador con
            // el objeto storage
            // Setup lets you make the mock object return a specific object when a specific method is called.

            //Verify is to assert that a specific method was called.
            //Assert
            _employeeStorage.Verify(s => s.DeleteEmployee(1));
        }
        [Test]
        public void DeleteEmployee_WhenCalled_RedirectToAction()
        {
            var result = _employeeController.DeleteEmployee(1);

            Assert.That(result, Is.TypeOf<RedirectResult>());

        }
    }
}
