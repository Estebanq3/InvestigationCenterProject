using ResearchRepository.Domain.Theses.Repositories;
using ResearchRepository.Domain.Theses.Entities;
using ResearchRepository.Domain.Theses.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Application.Theses.Implementations
{
    public class ThesisService : IThesisService
    {
        private readonly IThesesRepository _thesisRepository;

        public ThesisService(IThesesRepository thesisRepository)
        {
            _thesisRepository = thesisRepository;
        }

        public async Task<IEnumerable<ThesisDTO>> GetAllAsync()
        {
            return await _thesisRepository.GetAllAsync();
        }

        public async Task<Thesis?> GetByIdAsync(int id)
        {
            return await _thesisRepository.GetByIdAsync(id);
        }

        public async Task<Thesis?> GetByNameAsync(string name, DateTime publicationDate) 
        {
            return await _thesisRepository.GetByNameAsync(name,publicationDate);
        }

        public async Task<int> GetThesesByTermCountAsync(string term, int idGroup)
        {
            return await _thesisRepository.GetThesesByTermCount(term, idGroup);
        }

        public async Task<IEnumerable<Thesis>?> GetThesesByTermPagedAsync(int currentPage, int size, string term, int idGroup)
        {
            return await _thesisRepository.GetThesesByTermPaged(currentPage, size, term, idGroup);
        }
        public async Task<IEnumerable<ThesisDTO>?> GetThesesByGroupIDAsync(long idGroup)
        {
            return await _thesisRepository.GetThesesByGroupID(idGroup);
        }

        public async Task<int> GetThesesCountAsync(int idGroup)
        {
            return await _thesisRepository.GetThesesCount(idGroup);
        }

        public async Task<int> GetActiveThesesCountAsync(int groupId)
        {
            return await _thesisRepository.GetActiveThesesCount(groupId);
        }

        public async Task<IEnumerable<Thesis>?> GetThesesPagedAsync(int currentPage, int size, int idGroup)
        {
            return await _thesisRepository.GetThesesPaged(currentPage, size, idGroup);
        }

        public async Task<IEnumerable<Thesis>?> GetActiveThesesPagedAsync(int currentPage,
                                                                          int size,
                                                                          int groupId)
        {
            return await _thesisRepository.GetActiveThesesPaged(currentPage, size, groupId);
        }

        public async Task AddThesisAsync(Thesis thesis)
        {
            await _thesisRepository.SaveAsync(thesis);
        }

        public async Task SaveProjectPartOfThesisAsync(ThesisPartOfProject thesisPartOfProject)
        {
            await _thesisRepository.SaveProjectPartOfThesisAsync(thesisPartOfProject);
        }

        public async Task DeleteThesis(int id)
        {
            await _thesisRepository.DeleteThesis(id);
        }

        public async Task DeleteProjectPartOfThesis(int id)
        {
            await _thesisRepository.DeleteProjectPartOfThesis(id);
        }

        public async Task DeleteThesisPartOfProject(int id)
        {
            await _thesisRepository.DeleteThesisPartOfProject(id);
        }

        public async Task UpdateThesisAsync(int id, string name, DateTime publicationDate,
                                            string summary, long groupId, String image,
                                            String doi, String type, String reference,
                                            byte[]? attachment, String? attachmentName)
        {
            await _thesisRepository.UpdateAsync(id, name, publicationDate, summary,
                                                groupId, image, doi, type, reference,
                                                attachment, attachmentName);
        }
        public int GetLastThesisId()
        {
            return _thesisRepository.GetLastThesisId();
        }

        public async Task<IList<ThesisPartOfProject>> GetAsyncThesisPartOfProjectFromId(int projectId)
        {
            return await _thesisRepository.GetAsyncThesisPartOfProjectFromId(projectId);
        }

        public async Task<IList<ThesisPartOfProject>> GetAsyncProjectsPartOfThesisFromId(int thesisId)
        {
            return await _thesisRepository.GetAsyncProjectsPartOfThesisFromId(thesisId);
        }

        public async Task<IList<ThesisPartOfProject>> GetThesisOfProjectById(IList<ThesisPartOfProject> _thesis)
        {
            return await _thesisRepository.GetThesisOfProjectById(_thesis);
        }

        public async Task<IList<Thesis>> GetThesesFromIds(IList<int> ids)
        {
            return await _thesisRepository.GetThesesFromIds(ids);
        }

        public async Task<IList<int>> GetProjectFromThesisId(int thesisId)
        {
            return await _thesisRepository.GetProjectFromThesisId(thesisId);
        }

        public async Task ChangeThesisState(int thesisId, bool state)
        {
            var thesis = await GetByIdAsync(thesisId);

            if (thesis is not null)
            {
                thesis.ChangeThesisState(state);
                await _thesisRepository.SaveAsync(thesis);
            }
        }
    
        public async Task<IList<ResearchAreaThesis>> GetAssociatedAreas(int id)
        {
            return await _thesisRepository.GetAssociatedAreas(id);
        }
        public async Task<ResearchAreaThesis> GetResearchAreaThesisAssociation(int thesisId, int areaId)
        {
            return await _thesisRepository.GetResearchAreaThesisAssociation(thesisId, areaId);
        }

        public async Task AddAssociatedArea(ResearchAreaThesis association)
        {
            await _thesisRepository.AddAssociatedArea(association);
        }

        public async Task DeleteAssociatedArea(int thesisId, int areaId)
        {
            await _thesisRepository.DeleteAssociatedArea(thesisId, areaId);
        }

    }
}
