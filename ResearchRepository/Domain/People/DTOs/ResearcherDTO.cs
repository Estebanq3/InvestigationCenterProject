using ResearchRepository.Domain.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Domain.Projects.DTOs
{
    class Researcher
    {
        public long Id { get; }
        public RequiredString Name { get; }

        public Researcher(long id, RequiredString name)
        {
            Id = id;
            Name = name;
        }
    }
}
