using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.Core.Exceptions;
using ResearchRepository.Domain.Core.ValueObjects;
using System;
using System.Collections.Generic;
using ResearchRepository.Domain.ResearchAreas.Entities;
using ResearchRepository.Domain.ResearchNews.Entities;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.Contacts.Entities;

namespace ResearchRepository.Domain.ResearchGroups.Entities
{
    public class ResearchGroup : AggregateRoot, ISoftDeletable
    {
        public RequiredString Name { get; }

        public String? Description { get; }

        public string? ImageName { get; }

        public DateTime? CreationDate { get; }

        //public IList<Subscriptions> Subscriptions { get; set; }

        public ResearchCenter Center { get; private set; }

        public bool Active { get; private set; }

        public string AdminEmail { get; private set; }

        public bool Deleted { get; set; }

        public readonly List<News> _news;
        public readonly List<Contact> _contacts;
        public readonly List<ResearchArea> _researchAreas;
        public IReadOnlyCollection<ResearchArea> ResearchAreas => _researchAreas.AsReadOnly();
        public IReadOnlyCollection<News> News => _news.AsReadOnly();
        public IReadOnlyCollection<Contact> Contacts => _contacts.AsReadOnly();

        public ResearchGroup(RequiredString name, String? description, string? imageName, DateTime? date, ResearchCenter center)
        {
            Name = name;
            Description = description;
            ImageName = imageName;
            CreationDate = date;
            Center = center;
            _news = new List<News>();
            _contacts = new List<Contact>();
            _researchAreas = new List<ResearchArea>();
        }
        public ResearchGroup(int id, RequiredString name, String? description, string? imageName, DateTime? date, ResearchCenter center)
        {
            Id = id;
            Name = name;
            Description = description;
            ImageName = imageName;
            CreationDate = date;
            Center = center;
            _news = new List<News>();
            _researchAreas = new List<ResearchArea>();
            _contacts = new List<Contact>();
        }

        //EFCore
        private ResearchGroup()
        {
            Name = null!;
            Center = null!;
            _news = null!;
            _researchAreas = null!;
            _contacts = null!;
        }

        public void AddNewsToGroup(News news)
        {
            if (news != null)
            {
                if (!_news.Exists(n => n == news))
                {
                    _news.Add(news);
                    news.AssignGroup(this);
                }
                else
                    throw new DomainException("Research news is already added to the group");
            }
            else
                throw new DomainException("Research news is null");
        }

        public void RemoveNewsFromGroup(News news)
        {
            if(_news.Exists(n => n == news))
            {
                _news.Remove(news);
                news.AssignGroup(null);
            }
        }

        public void AddContactToGroup(Contact contact)
        {
            if (contact != null)
            {
                if (!_contacts.Exists(n => n == contact))
                {
                    _contacts.Add(contact);
                    contact.AssignGroup(this);
                }
                //else
                    //throw new DomainException("Contact is already added to the group");
               
            }
            else
                throw new DomainException("Contact is null");
        }

        public void RemoveContactFromGroup(Contact contact)
        {
            if (_contacts.Exists(n => n == contact))
            {
                _contacts.Remove(contact);
            }
        }

        public void AddAreaToGroup(ResearchArea area)
        {
            if(_researchAreas.Exists(r => r == area))
            {
                throw new DomainException("Research area is already in the group");
            }

            _researchAreas.Add(area);
            area.AddResearchGroup(this);
        }

        public void RemoveAreaFromGroup(ResearchArea area)
        {
            if (_researchAreas.Contains(area))
                _researchAreas.Remove(area);
            else
                throw new DomainException("Research area is not in the group");
            area.RemoveResearchGroup(this);
        }

        /// <summary>
        /// Change the state of the group (active/inactive)
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-1.6
        /// <param name="state">Active = true, Inactive = False</param>
        public void ChangeStateGroup(bool state)
        {
            Active = state;
        }

        public void AssignCenter(ResearchCenter? center)
        {
            Center = center;
        }

        public void AssingAdmin(string adminEmail)
        {
            AdminEmail = adminEmail;
        }
    }
}
