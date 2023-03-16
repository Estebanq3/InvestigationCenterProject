using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchNews.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ResearchRepository.Presentation.ResearchNews.Models
{
    public class NewsModel
    {

        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public NewsImage? MainImage { get; set; }
        public List<NewsImage>? AssociatedImages { get; set; }
        public List<Person> AssociatedPeople { get; set; }

        [MaxLength(43, ErrorMessage = "Enlace muy largo. Verifique que sea un video y no una lista de reproducción")]
        [RegularExpression(@"http(?:s?):\/\/(?:www\.)?youtu(?:be\.com\/watch\?v=|\.be\/)([\w\-\\_]*)(&(amp;)?‌​[\w\?‌​=]*)?", ErrorMessage = "Verifique que sea un enlace válido de Youtube")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string? VideoURL { get; set; }
        public string? DocumentURN{ get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? PublicationDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? EndTime { get; set; }
        public TimeSpan? PublicationTime { get; set; }
        public ResearchGroup Group { get; set; }

        public NewsModel(News news)
        {
            Id = news.Id;
            Title = news.Title.ToString();
            Description = news.Description.ToString();
            MainImage = news.MainImage;
            AssociatedImages = news.AssociatedImages.ToList();
            AssociatedPeople = news.AssociatedPeople.ToList();
            VideoURL = news.VideoURL;
            DocumentURN = news.DocumentURN;
            CreationDate = news.CreationDate;
            PublicationDate = news.PublicationDate;
            EndDate = news.EndDate;
            EndTime = news.EndDate!.Value.TimeOfDay;
            PublicationTime = news.PublicationDate!.Value.TimeOfDay;
            Group = news.Group;
        }

        public NewsModel(ResearchGroup group, DateTime? defaultEndDate = null)
        {
            Title = null!;
            Description = null!;
            Group = group;
            CreationDate = DateTime.Now;
            PublicationDate = DateTime.Now;
            EndDate = defaultEndDate;
            PublicationTime = PublicationDate.Value.TimeOfDay;
            EndTime = (EndDate != null)? EndDate.Value.TimeOfDay: null;
            AssociatedImages = new List<NewsImage>();
            AssociatedPeople = new List<Person>();
        }

        /// <summary>
        /// Removes the specified image from the NewsModel's associated images.
        /// </summary>
        /// User story: ST-MM-6.5
        /// Contributor: Rodrigo Contreras (Monkey Madness)
        /// <param name="image">The image to be removed</param>
        public void DeleteAssociatedImage(NewsImage image)
        {
            if (AssociatedImages.Exists(i => i == image))
                AssociatedImages.Remove(image);

            if (MainImage == image && AssociatedImages.Count != 0)
                MainImage = AssociatedImages.Head();
            else if(AssociatedImages.Count == 0)
                MainImage = null!;
        }
    }
}
