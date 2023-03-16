using ResearchRepository.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.ResearchAreas.Entities;

namespace ResearchRepository.Domain.StatisticsContext
{
    public partial class Statistic : AggregateRoot
    {

        public string Id { get; set; }
        public DateTime Year { get; set; }
        public string TypePublication { get; set; }
        public int ResearchGroupId { get; set; }
        public bool Deleted { get; set; }

        private readonly List<ResearchArea> _researchAreas;
        public IReadOnlyCollection<ResearchArea> ResearchAreas => _researchAreas.AsReadOnly();

        public Statistic(string id, DateTime year, string typePublication, int researchGroupId)
        {
            Id = id;
            Year = year;
            TypePublication = typePublication;
            ResearchGroupId = researchGroupId;
            _researchAreas = new List<ResearchArea>();
        }
        public Statistic()
        {
            Id = null!;
            Year = DateTime.Now;
            TypePublication = null!;
            ResearchGroupId = -1;
            _researchAreas = new List<ResearchArea>();
        }
    }
}
