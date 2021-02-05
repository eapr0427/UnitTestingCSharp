using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class CustomerControllerTests
    {

        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            var controller = new CustomerController();

            var result = controller.GetCustomer(0);

            //NotFound
            Assert.That(result, Is.TypeOf<NotFound>());

            //NotFound object of one of its derivatives
            // Assert.That(result, Is.InstanceOf<NotFound>());

        }

        [Test]
        public void GetCustomer_IdIsNotZero_ReturnOk()
        {

            var controller = new CustomerController();

            var result = controller.GetCustomer(2);

            //NotFound
            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}
