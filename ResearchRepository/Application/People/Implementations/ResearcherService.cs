using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Application.People;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Application.People.Implementations
{
    internal class ResearcherService: IResearcherService
    {
        public async Task<IEnumerable<Researcher>?> GetResearchersAsync(int idGroup, int currentPage, int size)
        {
            List<Researcher> people = new List<Researcher>();
            for (var p = 0; p < 18; ++p)
            {
                people.Add(new Researcher(p+1, "Juan Pérez", 1+(p%2), "Developer"));
            }
            return await Task.FromResult(people.Where(p => p.GroupId == idGroup).Skip((currentPage - 1) * size).Take(size));
        }
    }
}
