using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;

namespace ResearchRepository.Domain.People.Entities
{
    public class AcademicProfile
    {
        public string? Email { get; set; }
        public string? Biography { get; set; }
        public string? ProfilePic { get; set; } 
        public string? Degree { get; set; }
        public string? FacebookLink { get; set; }
        public string? GitHubLink { get; set; }
        public string? LinkedInLink { get; set; }
        public string? Title { get; set; }
        
        [Phone]
        public string? Tel { get; set; }
        public Person? Person { get; set; }


        public AcademicProfile(string? email, string? bio, string? pic, string? degree, string? facebook, string? github, string? linkedIn, string? title, string? tel) {
            Email = email;
            Biography = bio;
            ProfilePic = pic;
            Degree = degree;
            FacebookLink = facebook;
            GitHubLink = github;
            LinkedInLink = linkedIn;
            Title = title;
            Tel = tel;
            Person = null;

        }

        public AcademicProfile() {
            Email = null;
            Biography = null;
            ProfilePic = null;
            Degree = null;
            FacebookLink = null;
            GitHubLink = null;
            LinkedInLink = null;
            Title = null;
            Tel = null;
            Person = null;
        }


    }
}
