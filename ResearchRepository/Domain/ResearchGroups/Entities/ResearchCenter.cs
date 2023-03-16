using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;

namespace ResearchRepository.Domain.ResearchGroups.Entities
{
    public class ResearchCenter : AggregateRoot
    {
        public RequiredString Name { get; }
        public String? Abbreviation { get; }

        public String? Description { get; }

        public String? ImageName { get; }

        public readonly List<ResearchGroup> _researchGroups;
        public IReadOnlyCollection<ResearchGroup> ResearchGroups => _researchGroups.AsReadOnly();

        public ResearchCenter(RequiredString name, String? description, String? imageName, String? abbreviation)
        {
            Name = name;
            Description = description;
            Abbreviation = abbreviation;
            ImageName = imageName;
            _researchGroups = new List<ResearchGroup>();
        }

        // for EFCore empty private constructor
        private ResearchCenter()
        {
            Name = null!;
            _researchGroups = null!;
        }
        public void AddGroupToCenter(ResearchGroup group)
        {
            if (!_researchGroups.Exists(n => n == group))
            {
                _researchGroups.Add(group);
                group.AssignCenter(this);
            }

        }

        public void RemoveGroupFromCenter(ResearchGroup group)
        {
            if (_researchGroups.Exists(n => n == group))
            {
                _researchGroups.Remove(group);
                group.AssignCenter(null);
            }
        }

    }
}
