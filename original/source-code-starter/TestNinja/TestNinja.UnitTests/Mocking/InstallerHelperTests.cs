using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnsFalse()
        {
            //Arrange
            //Con setup programamos el Mock. Algo importante a tener en cuenta es que cuando programamos el  mock
            // usando el método SetUp, el comportamiento que definimos, en este caso  que lance la excepción WebException. 
            // Solo sucede cuando llamamos el método DownloadFile  con los mismos argumentos exactos
            // que los que ejecuta el método DownloadInstaller
            // Si pasamos otros argumentos, ese comportamiento no sucederá. Por lo que el método no hará nada


            //Otra forma mas elegante de enviar los parámetros y programar el Mock de una forma más genérica
            // es usando It.IsAny<string> como parámetros.

            //_fileDownloader.Setup(fd => fd.DownloadFile("http://example.com/jov/pin", null)).Throws<WebException>();
            _fileDownloader.Setup(fd => 
                                  fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                                  .Throws<WebException>();

            //ACT
            var result = _installerHelper.DownloadInstaller("jov", "pin");
            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void DownloadInstaller_DownloadCompletes_ReturnsTrue()
        {
            //Arrange: No es necesario programar el Mock  porque por default DownloadFile no hace nada ni arroja una excepción

            //ACT
            var result = _installerHelper.DownloadInstaller("jov", "pin");
            //Assert
            Assert.That(result, Is.True);
        }
    }
}
