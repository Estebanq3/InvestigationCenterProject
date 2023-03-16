using Microsoft.EntityFrameworkCore;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.StatisticsContext;
using ResearchRepository.Domain.Repositories;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchAreas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ResearchRepository.Infrastructure.StatisticsContext.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly StatisticsDbContext _dbContext;
        public IUnitOfWork UnitOfWork => _dbContext;


        public StatisticsRepository(StatisticsDbContext unitOfWork)
        {
            _dbContext = unitOfWork;
        }
        
        public async Task<IList<Statistic>> GetAsync(List<int> _groupsIds)
        {
            List<Statistic> statistics = new List<Statistic>();
            if (_groupsIds.Count() != 0)
            {
                foreach (var id in _groupsIds)
                {
                    statistics.AddRange(await _dbContext.Publication
                      .Where(g => g.Deleted == false && g.ResearchGroupId == id)
                      .Include(e => e.ResearchAreas).ThenInclude(subarea => subarea.ResearchAreas)
                      .ToListAsync());
                }
            }
            return statistics;
        }

        public async Task<Dictionary<string, int>> GetAreasAsync(List<int> _groupsIds)
        {
            List<Statistic> statistics = new List<Statistic>();
            Dictionary<string, int> _researchArea = new Dictionary<string, int>();
            if (_groupsIds.Count() != 0)
            {
                foreach (var id in _groupsIds)
                {
                    statistics.AddRange(await _dbContext.Publication
                      .Where(g => g.Deleted == false && g.ResearchGroupId == id)
                      .Include(e => e.ResearchAreas).ThenInclude(subarea => subarea.ResearchAreas)
                      .ToListAsync());
                }
                foreach (var publication in statistics)
                {
                    foreach (var subarea in publication.ResearchAreas)
                    {
                        foreach (var area in subarea.ResearchAreas)
                        {
                            if (_researchArea.ContainsKey(area.Name.ToString()))
                            {
                                _researchArea[area.Name.ToString()]++;
                            }
                            else
                            {
                                _researchArea.Add(area.Name.ToString(), 1);
                            }
                        }
                    }
                }
            }
            return _researchArea;
        }

        public async Task<Dictionary<string, int>> GetSubAreasByAreasAsync(List<int> _groupsIds, List<string> researchAreas)
        {
            List<Statistic> statistics = new List<Statistic>();
            Dictionary<string, int> researchSubareas = new Dictionary<string, int>();
            if (_groupsIds.Count() != 0)
            {
                foreach (var id in _groupsIds)
                {
                    statistics.AddRange(await _dbContext.Publication
                      .Where(g => g.Deleted == false && g.ResearchGroupId == id)
                      .Include(e => e.ResearchAreas).ThenInclude(subarea => subarea.ResearchAreas)
                      .ToListAsync());
                }
                foreach (var publication in statistics)
                {
                    foreach (var subarea in publication.ResearchAreas)
                    {
                        foreach (var area in subarea.ResearchAreas)
                        {
                            foreach (var areaFiltered in researchAreas)
                            {
                                if (area.Name.ToString() == areaFiltered)
                                {
                                    if (researchSubareas.ContainsKey(subarea.Name.ToString()))
                                    {
                                        researchSubareas[subarea.Name.ToString()]++;
                                    }
                                    else
                                    {
                                        researchSubareas.Add(subarea.Name.ToString(), 1);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return researchSubareas;
        }

        public async Task<int> GetCountSubAreasByAreasAsync(List<int> _groupsIds, List<string> researchAreas)
        {
            List<Statistic> statistics = new List<Statistic>();
            int count = 0;
            bool added = false;
            if (_groupsIds.Count() != 0)
            {
                foreach (var id in _groupsIds)
                {
                    statistics.AddRange(await _dbContext.Publication
                      .Where(g => g.Deleted == false && g.ResearchGroupId == id)
                      .Include(e => e.ResearchAreas).ThenInclude(subarea => subarea.ResearchAreas)
                      .ToListAsync());
                }
                foreach (var publication in statistics)
                {
                    foreach (var subarea in publication.ResearchAreas)
                    {
                        foreach (var area in subarea.ResearchAreas)
                        {
                            foreach (var areaFiltered in researchAreas)
                            {
                                if (area.Name.ToString() == areaFiltered && added == false)
                                {
                                    added = true;
                                    count += 1;
                                }
                            }
                        }
                    }
                    added = false;
                }
            }
            return count;
        }

            public async Task<Dictionary<string, int>> GetYearAsync(List<int> _groupsIds)
        {
            List<Statistic> statistics = new List<Statistic>();
            Dictionary<string, int> years = new Dictionary<string, int>();
            if (_groupsIds.Count() != 0)
            {
                foreach (var id in _groupsIds)
                {
                    statistics.AddRange(await _dbContext.Publication.Where(p => p.Deleted == false && p.ResearchGroupId == id).ToListAsync());
                }
                if (statistics.Count() != 0)
                {
                    var totalYears = from d in statistics
                                     orderby d.Year
                                     group d by d.Year.Year into totals
                                     select new
                                     {
                                         Year = totals.Key,
                                         counterYears = totals.Count()
                                     };
                    int year = totalYears.First().Year;
                    // every year of publication
                    foreach (var item in totalYears)
                    {
                        if (year != item.Year)
                        {
                            // if there are years without publications
                            for (int i = year; i < item.Year; ++i)
                            {
                                years.Add(i.ToString(), 0);
                                ++year;
                            }
                        }
                        years.Add(item.Year.ToString(), item.counterYears);
                        ++year;
                    }

                    // from last year publication to the current year
                    for (int j = year; j <= DateTime.Today.Year; ++j)
                    {
                        years.Add(j.ToString(), 0);
                    }
                }
            }
            return years;
        }
        
        public async Task<Dictionary<string, int>> GetYearByIdAsync(int IdGroup)
        {
            List<Statistic> statistics = await _dbContext.Publication.Where(p => p.Deleted == false && p.ResearchGroupId == IdGroup).ToListAsync();
            Dictionary<string, int> years = new Dictionary<string, int>();
            var totalYears = from d in statistics
                             orderby d.Year
                             group d by d.Year.Year into totals
                             select new
                             {
                                 Year = totals.Key,
                                 counterYears = totals.Count()
                             };
            int year = totalYears.First().Year;
            // every year of publication
            foreach (var item in totalYears)
            {
                if (year != item.Year)
                {
                    // if there are years without publications
                    for (int i = year; i < item.Year; ++i)
                    {
                        years.Add(i.ToString(), 0);
                        ++year;
                    }
                }
                years.Add(item.Year.ToString(), item.counterYears);
                ++year;
            }

            // from last year publication to the current year
            for (int j = year; j <= DateTime.Today.Year; ++j)
            {
                years.Add(j.ToString(), 0);
            }

            return years;
        }

        public async Task<IList<Statistic>> GetById(int IdGroup)
        {

            List<Statistic> statistics = await _dbContext.Publication.Where(p => p.ResearchGroupId == IdGroup)
                .Include(e => e.ResearchAreas).ThenInclude(subarea => subarea.ResearchAreas)
               .ToListAsync();

            return statistics;

        }
        
        public async Task<Dictionary<string, int>> GetPublicationsByGroups(List<int> _groupsIds)
        {
            List<Statistic> publications = new List<Statistic>();
            IList<ResearchGroup> researchGroups = await GetGroups(_groupsIds);
            Dictionary<string, int> _groups = new Dictionary<string, int>();
            if (_groupsIds.Count() != 0)
            {
                foreach (var id in _groupsIds)
                {
                    publications.AddRange(await _dbContext.Publication.Where(p => p.Deleted == false && p.ResearchGroupId == id).ToListAsync());
                }
                foreach (var group in researchGroups)
                {
                    if (_groups != null)
                    {
                        if (_groups.ContainsKey(group.Name.ToString()) == false)
                        {
                            _groups.Add(group.Name.ToString(), 0);
                        }
                        foreach (var publication in publications)
                        {
                            if (publication.ResearchGroupId == group.Id)
                            {
                                if (_groups.ContainsKey(group.Name.ToString()))
                                {
                                    _groups[group.Name.ToString()]++;
                                }
                                else
                                {
                                    _groups.Add(group.Name.ToString(), 1);
                                }
                            }
                        }
                    }
                }
            }
            return _groups;
        }
        
        public async Task<IList<ResearchGroup>> GetGroups(List<int> _groupsIds)
        {
            List<ResearchGroup> groups = new List<ResearchGroup>();
            if (_groupsIds != null) {
                foreach (var idGroup in _groupsIds)
                {
                    groups.AddRange(await _dbContext.ResearchGroup.Where(g => g.Id == idGroup).ToListAsync());
                }
            }
            else
            {
                groups = await _dbContext.ResearchGroup.ToListAsync();
            }
            return groups;
        }

        public async Task<Dictionary<string, int>> GetTypePublicationAsync(List<int> _groupsIds)
        {
            List<Statistic> statistics = new List<Statistic>();
            Dictionary<string, int> _typePublication = new Dictionary<string, int>();
            if (_groupsIds.Count() != 0)
            {
                foreach (var id in _groupsIds)
                {
                    statistics.AddRange(await _dbContext.Publication.Where(p => p.Deleted == false && p.ResearchGroupId == id).ToListAsync());
                }
                var totalTypes = from d in statistics
                                 group d by d.TypePublication into totals
                                 select new
                                 {
                                     Type = totals.Key,
                                     counterType = totals.Count()
                                 };
                foreach (var item in totalTypes)
                {
                    _typePublication.Add(item.Type.ToString(), item.counterType);
                }
            }
            return _typePublication;
        }

        public async Task<Dictionary<string, int>> GetTypePublicationByYearsAsync(List<int> _groupsIds, List<int> _listYears, string type)
        {
            List<Statistic> statistics = new List<Statistic>();
            Dictionary<string, int> _typePublication = new Dictionary<string, int>();
            if (_groupsIds.Count() != 0 && _listYears.Count() != 0)
            {

                foreach (var id in _groupsIds)
                {
                    statistics.AddRange(await _dbContext.Publication.Where(p => p.Deleted == false && p.ResearchGroupId == id).ToListAsync());
                }
                var totalTypes = from d in statistics
                                 where d.TypePublication == type
                                 group d by d.Year.Year into totals
                                 select new
                                 {
                                     Year = totals.Key,
                                     counterType = totals.Count()
                                 };


                bool added;
                foreach (var year in _listYears)
                {
                    added = false;
                    foreach (var item in totalTypes)
                    {
                        if (item.Year == year)
                        {
                            _typePublication.Add(item.Year.ToString(), item.counterType);
                            added = true;
                        }
                    }
                    if (!added)
                    {
                        _typePublication.Add(year.ToString(), 0);
                    }
                }
            }
            return _typePublication;
        }

        public async Task<Dictionary<string, int>> GetTypePublicationByIdAsync(int IdGroup)
        {
            List<Statistic> statistics = await _dbContext.Publication.Where(p => p.Deleted == false && p.ResearchGroupId == IdGroup).ToListAsync();
            Dictionary<string, int> _typePublication = new Dictionary<string, int>();
            var totalTypes = from d in statistics
                             group d by d.TypePublication into totals
                             select new
                             {
                                 Type = totals.Key,
                                 counterType = totals.Count()
                             };
            foreach (var item in totalTypes)
            {
                _typePublication.Add(item.Type.ToString(), item.counterType);
            }

            return _typePublication;
        }

        public async Task<int> GetPublicationCountByResearchGroup(int researchGroupId)
        {
            var result = _dbContext.ScalarIntValue.FromSqlRaw(
                    "EXEC SP_GetPublicationCountByResearchGroup @researchGroupId",
                    new SqlParameter("@researchGroupId", researchGroupId)
                    ).AsEnumerable().FirstOrDefault();

            if (result == null)
            {
                return 0;
            }

            return result.Value;
        }
    }
}
