using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Application.StatisticsContext;
using ResearchRepository.Domain.Repositories;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.StatisticsContext;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchAreas.Entities;

namespace ResearchRepository.Application.StatisticsContext.Implementation
{
    public class StatisticsService:IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsService(IStatisticsRepository statisticsRepository)
        {

            _statisticsRepository = statisticsRepository;

        }

        public async Task<IList<Statistic>> GetAsync(List<int> _groupsIds)
        {
            return await _statisticsRepository.GetAsync(_groupsIds);
        }

        public async Task<Dictionary<string, int>> GetAreasAsync(List<int> _groupsIds)
        {
            return await _statisticsRepository.GetAreasAsync(_groupsIds);
        }
        public async Task<Dictionary<string, int>> GetSubAreasByAreasAsync(List<int> _groupsIds, List<string> researchAreas)
        {
            return await _statisticsRepository.GetSubAreasByAreasAsync(_groupsIds,researchAreas);
        }

        public async Task<int> GetCountSubAreasByAreasAsync(List<int> _groupsIds, List<string> researchAreas)
        {
            return await _statisticsRepository.GetCountSubAreasByAreasAsync(_groupsIds, researchAreas);
        }

        public async Task<Dictionary<string, int>> GetYearAsync(List<int> _groupsIds)
        {
            return await _statisticsRepository.GetYearAsync(_groupsIds);
        }

        public async Task<Dictionary<string, int>> GetYearByIdAsync(int IdGroup)
        {
            return await _statisticsRepository.GetYearByIdAsync(IdGroup);
        }


        public async Task<IList<Statistic>> GetById(int IdGroup)
        {
            return await _statisticsRepository.GetById(IdGroup);
        }

        public async Task<Dictionary<string, int>> GetPublicationsByGroups(List<int> _groupsIds)
        {
            return await _statisticsRepository.GetPublicationsByGroups(_groupsIds);
        }

        public async Task<IList<ResearchGroup>> GetGroups(List<int> _groupsIds)
        {
            return await _statisticsRepository.GetGroups(_groupsIds);
        }

        public async Task<Dictionary<string, int>> GetTypePublicationAsync(List<int> _groupsIds)
        {
            return await _statisticsRepository.GetTypePublicationAsync(_groupsIds);
        }

        public async Task<Dictionary<string, int>> GetTypePublicationByYearsAsync(List<int> _groupsIds, List<int> _listYears, string type)
        {
            return await _statisticsRepository.GetTypePublicationByYearsAsync(_groupsIds, _listYears, type);
        }

        public async Task<Dictionary<string, int>> GetTypePublicationByIdAsync(int IdGroup)
        {
            return await _statisticsRepository.GetTypePublicationByIdAsync(IdGroup);
        }

        public async Task<int> GetPublicationCountByResearchGroupAsync(int researchGroupId)
        {
            return await _statisticsRepository.GetPublicationCountByResearchGroup(researchGroupId);
        }


    }
}
