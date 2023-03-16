using ResearchRepository.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Domain.ResearchNews.Entities
{
    public class NewsImage : Entity
    {
        public string Path { get; }
        public News News { get; private set; }
        /// <summary>
        /// Creates a News Image object associated to the News object.
        /// </summary>
        /// User story: ST-MM-6.5
        /// Contributor: Rodrigo Contreras (Monkey Madness)
        /// <param name="path"></param>
        /// <param name="news"></param>
        public NewsImage(string path)
        {
            Path = path;
            News = null!;
        }
        //EFCore
        private NewsImage()
        {
            Path = null!;
            News = null!;
        }
        /// <summary>
        /// Associates the image to the specified News object.
        /// </summary>
        /// Author: Rodrigo Contreras (Monkey Madness)
        /// User Story ID: ST-MM-6.5
        /// <param name="news"></param>
        public void SetNews(News news)
        {
            News = news;
        }
    }
}
