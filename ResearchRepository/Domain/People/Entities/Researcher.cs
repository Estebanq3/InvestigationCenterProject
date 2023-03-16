using ResearchRepository.Domain.ResearchGroups.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchRepository.Domain.People.Entities
{
    public class Researcher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId{ get; set; }
        public string Description { get; set; }

        public Researcher(int id, string name, int group, string description)
        {
            Id = id;
            Name = name;
            GroupId= group;
            Description = description;
        }
    }
}
