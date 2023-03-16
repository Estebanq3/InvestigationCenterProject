using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.Core.Exceptions;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchRepository.Domain.ResearchNews.Entities
{
    public class News : AggregateRoot, ISoftDeletable
    {
        public RequiredString Title { get; }

        public RequiredString Description { get; }
        public int? MainImageId { get; private set; }
        [NotMapped]
        public NewsImage? MainImage { get; private set; }

        public string? VideoURL { get; }

        public string? DocumentURN { get; }

        public DateTime? CreationDate { get; }

        public DateTime? PublicationDate { get; }

        public DateTime? EndDate { get; }

        public ResearchGroup Group { get; private set; }

        public bool Deleted { get; set; }
        //Associated Images
        private readonly List<NewsImage> _associatedImages;
        public IReadOnlyCollection<NewsImage> AssociatedImages => _associatedImages.AsReadOnly();
        //Associated people
        private readonly List<Person> _associatedPeople;
        public IReadOnlyCollection<Person> AssociatedPeople => _associatedPeople.AsReadOnly();


        public News(RequiredString title, RequiredString description, string? videoUrl, string? documenteUrn, DateTime? creationDate, DateTime? publinDate, DateTime? endDate, ResearchGroup group)
        {
            Title = title;
            Description = description;
            CreationDate = creationDate;
            Group = group;
            PublicationDate = publinDate;
            EndDate = endDate;
            VideoURL = videoUrl;
            DocumentURN = documenteUrn;
            _associatedImages = new List<NewsImage>();
            _associatedPeople = new List<Person>();
        }


        public News(int id, RequiredString title, RequiredString description, string? videoUrl, string? documenteUrn, DateTime? creationDate, DateTime? publinDate, DateTime? endDate, ResearchGroup group)
        {
            Id = id;
            Title = title;
            Description = description;
            CreationDate = creationDate;
            Group = group;
            PublicationDate = publinDate;
            EndDate = endDate;
            VideoURL = videoUrl;
            DocumentURN = documenteUrn;
            _associatedImages = new List<NewsImage>();
            _associatedPeople = new List<Person>();
        }

        //EFCore
        private News()
        {
            Title = null!;
            Description = null!;
            Group = null!;
            _associatedImages = null!;
            _associatedPeople = null!;
        }

        public void AssignGroup(ResearchGroup? group)
        {
            Group = group!;
        }
        /// <summary>
        /// Associates the specified image with the News object.
        /// </summary>
        /// User story: ST-MM-6.5
        /// Contributor: Rodrigo Contreras (Monkey Madness)
        /// <param name="image"></param>
        /// <exception cref="DomainException"></exception>
        public void AddAssociatedImage(NewsImage image)
        {
            image.SetNews(this);
            if (!_associatedImages.Exists(i => i == image))
                _associatedImages.Add(image);
            else
                throw new DomainException("Image is already associated to this news.");
        }

        public void RemoveAssociatedImage(NewsImage image)
        {
            if (_associatedImages.Exists(i => i == image))
            {
                _associatedImages.Remove(image);
                if (MainImage == image)
                {
                    if (_associatedImages.Count > 0)
                        MainImage = _associatedImages.Head();
                    else
                        MainImage = null;
                }
            }
            else
                throw new DomainException("Image isn't associated to this news.");
        }
        
        /// <summary>
        /// Sets the main image of the news.
        /// </summary>
        /// User story: ST-MM-6.5
        /// Contributor: Rodrigo Contreras (Monkey Madness)
        /// <param name="image"></param>
        /// <exception cref="DomainException"></exception>
        public void SetMainImage(NewsImage image)
        {
            if (_associatedImages.Exists(i => i == image))
            {
                MainImage = image;
                MainImageId = image.Id;
            }
            else
                throw new DomainException("Image isn't associated to this news.");
        }

        public NewsImage ClearMainImage()
        {
            var image = MainImage;
            MainImage = null;
            MainImageId = null;
            return image!;
        }

        /// <summary>
        /// Associates the specified person with the News object.
        /// </summary>
        /// User story: ST-MM-6.4
        /// Contributor: Rodrigo Contreras (Monkey Madness)
        /// <param name="person"> The person object</param>
        /// <exception cref="DomainException"> Thrown when person is already associated to News</exception>
        public void AddAssociatedPerson(Person person)
        {
            if (_associatedPeople.Exists(p => p == person))
                throw new DomainException("Person is already associated to this news.");
            else
                _associatedPeople.Add(person);
        }

        /// <summary>
        /// Removes the association of the specified person from the News object.
        /// </summary>
        /// User story: ST-MM-6.4
        /// Contributor: Rodrigo Contreras (Monkey Madness)
        /// <param name="person"> The person object</param>
        /// <exception cref="DomainException"> Thrown when person isn't associated to News</exception>
        public void RemoveAssociatedPerson(Person person)
        {
            if (!_associatedPeople.Exists(p => p == person))
                throw new DomainException("Person isn't associated to this news.");
            else
                _associatedPeople.Remove(person);
        }
    }
}
