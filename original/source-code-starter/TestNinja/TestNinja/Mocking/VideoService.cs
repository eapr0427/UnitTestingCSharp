using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TestNinja.Mocking
{
    public class VideoService
    {

        //public IFileReader FileReader { get; set; }

        private IFileReader _fileReader;
        private readonly IVideoRepository _videoRepository;

        public VideoService(IFileReader fileReader = null, IVideoRepository videoRepository = null)
        {

            //_fileReader = new FileReader();
            _fileReader = fileReader ?? new FileReader();
            this._videoRepository = videoRepository ?? new VideoRepository();
        }

        //public string ReadVideoTitle(IFileReader fileReader)
        public string ReadVideoTitle()
        {
            //EXTERNAL RESOURCE, EXTRAEMOS LAS LINEAS Y LAS PONEMOS EN UNA CLASE SEPARADA
            //var str = fileReader.Read("video.txt");

            var str = _fileReader.Read("video.txt");

            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        // [] => ""
        // [{},{},{}] => "1,2,3"
        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            //EXTERNAL RESOURCE, extraemos las líneas y las ponemos en una clase separada
            // Utilizamos el patrón repositorio para encapsular esta logice de BD

            var videos = _videoRepository.GetUnprocessedVideos();

            foreach (var v in videos)
                videoIds.Add(v.Id);

            return String.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}