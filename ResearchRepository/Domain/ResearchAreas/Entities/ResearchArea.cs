using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Exceptions;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.InvestigationProjects.Entities;
using ResearchRepository.Domain.PublicationContext;
using ResearchRepository.Domain.StatisticsContext;

namespace ResearchRepository.Domain.ResearchAreas.Entities
{
    public class ResearchArea : AggregateRoot, IEquatable<ResearchArea>, IComparable<ResearchArea>
    {
        public RequiredString Name { get; private set; }
        public String? Description { get; set; }

        private readonly List<Statistic> _publications;
        public IReadOnlyCollection<Statistic> Publications => _publications.AsReadOnly();

        private readonly List<ResearchGroup> _researchGroups;
        public IReadOnlyCollection<ResearchGroup> ResearchGroups => _researchGroups.AsReadOnly();

        private readonly List<ResearchArea> _researchSubAreas;

        public IReadOnlyCollection<ResearchArea> ResearchSubAreas => _researchSubAreas.AsReadOnly();

        private readonly List<ResearchArea> _researchAreas;
        public IReadOnlyCollection<ResearchArea> ResearchAreas => _researchAreas.AsReadOnly();

        public IList<ResearchAreaProject> ResearchAreaProject { set; get; }

        private readonly List<InvestigationProject> _projects = new List<InvestigationProject>();
        public IReadOnlyCollection<InvestigationProject> Projects => _projects.AsReadOnly();

        public ResearchArea(RequiredString name, string description)
        {
            Name = name;
            Description = description;
            _researchAreas = new List<ResearchArea>();
            _researchGroups = new List<ResearchGroup>();
            _researchSubAreas = new List<ResearchArea>();
            _projects = new List<InvestigationProject>();
            _publications = new List<Statistic>();
        }

        public ResearchArea(int id, RequiredString name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            _researchAreas = new List<ResearchArea>();
            _researchGroups = new List<ResearchGroup>();
            _researchSubAreas = new List<ResearchArea>();
            _projects = new List<InvestigationProject>();
            _publications = new List<Statistic>();
        }

        private ResearchArea()
        {
            Name = null!;
            Description = null;
            _researchAreas = new List<ResearchArea>();
            _researchGroups = new List<ResearchGroup>();
            _researchSubAreas = new List<ResearchArea>();
            _projects = new List<InvestigationProject>();

        }
        public void AddResearchGroup(ResearchGroup group)
        {
            if (_researchGroups.Exists(g => g == group))
                throw new DomainException("Research group is already in the area");

            _researchGroups.Add(group);
        }

        public void RemoveResearchGroup(ResearchGroup group)
        {
            if (_researchGroups.Exists(p => p == group))
                _researchGroups.Remove(group);
            else
                throw new DomainException("Research group is not added");
        }

        public void AddSubArea(ResearchArea subArea)
        {
            if (_researchSubAreas.Exists(s => s == subArea))
                throw new DomainException("Research sub-area is already added");

            var index = _researchSubAreas.BinarySearch(subArea);
            if (index < 0)
            {
                index = ~index;
            }
            _researchSubAreas.Insert(index, subArea);
            subArea.AddParentArea(this);
        }

        public void RemoveSubArea(ResearchArea subArea)
        {
            if (subArea.ResearchAreas.Count() == 0)
                throw new DomainException("Not a research sub-area");

            _researchSubAreas.Remove(subArea);

        }
        public void AddParentArea(ResearchArea parent)
        {
            if (!_researchAreas.Contains(parent))
                _researchAreas.Add(parent);
        }
        public void RemoveParentArea(ResearchArea parent)
        {
            if (ResearchAreas.Contains(parent))
            {
                parent.RemoveSubArea(this);
                _researchAreas.Remove(parent);
            }
            else
                throw new DomainException("It's not a parent of the subarea");
        }

        public void AddParentAreas(IEnumerable<ResearchArea> areas)
        {
            _researchAreas.AddRange(areas.Where(area => !_researchAreas.Contains(area)));
        }

        public void RemoveParentAreas(IEnumerable<ResearchArea> areas)
        {
            try
            {
                _researchAreas.RemoveAll(area => areas.Contains(area));
            }
            catch
            {
                throw new DomainException("Could not remove all areas");
            }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(ResearchArea other)
        {
            var result = other != null && (Id == other.Id) && (Name == other.Name);
            return result;
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }

        public int GetSubAreasCount()
        {
            if (ResearchSubAreas != null)
                return ResearchSubAreas.Count();
            else
                return 0;
        }

        public int CompareTo(ResearchArea another)
        {
            return Name.ToString().CompareTo(another.Name.ToString());
        }

        public void UpdateName(string name)
        {
            var result = RequiredString.TryCreate(name, 100);
            if (result.IsSuccess)
            {
                Name = RequiredString.CreateRequiredString(name);
            }
            else
                throw new DomainException("Not a valid name");
        }
    }
}
