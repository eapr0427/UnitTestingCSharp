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
    public class VideoServiceTests
    {
        private Mock<IFileReader> _fileReader;
        private VideoService _videoService;
        private Mock<IVideoRepository> _videoRepository;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _videoRepository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object,_videoRepository.Object); // Uso de Moq para pasar un objeto fake
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            //https://github.com/Moq/moq4/wiki/Quickstart
            //Con setup programamos el Mock ya que por defecto no tiene ningún comportamiento
            // Le indicamos que cuando llamen al método Read con el argumento "video.txt" retorne un string vacío
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            //var service = new VideoService(new FakeFileReader()); // Constructor DI
            //service.FileReader = new FakeFileReader(); //DI by property
            //var result = service.ReadVideoTitle(new FakeFileReader());//DI by method parameter
            var result = _videoService.ReadVideoTitle();
            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnAnEmptyString()
        {
            //Programamos el Mock
            _videoRepository.Setup(vr => vr.GetUnprocessedVideos()).Returns(new List<Video>());


            //Act
            var result = _videoService.GetUnprocessedVideosAsCsv();

            //Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AFewUnprocessedVideos_ReturnAStringWithIdsOfUnprocessedVideos()
        {
            //Programamos el Mock
            _videoRepository.Setup(vr => vr.GetUnprocessedVideos()).Returns(new List<Video> { new Video { Id = 8}, new Video { Id = 4}, new Video { Id = 1979} });

            //Act
            var result = _videoService.GetUnprocessedVideosAsCsv();

            //Assert
            Assert.That(result, Is.EqualTo("8,4,1979"));
        }
    }
}
