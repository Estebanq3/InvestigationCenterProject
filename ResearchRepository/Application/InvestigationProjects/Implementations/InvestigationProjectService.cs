using ResearchRepository.Domain.InvestigationProjects.Repositories;
using ResearchRepository.Domain.InvestigationProjects.Entities;
using ResearchRepository.Domain.InvestigationProjects.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Application.InvestigationProjects.Implementations
{
    public class InvestigationProjectService : IInvestigationProjectService
    {
        private readonly IInvestigationProjectsRepository _investigationProjectRepository;

        public InvestigationProjectService(IInvestigationProjectsRepository investigationProjectRepository)
        {
            _investigationProjectRepository = investigationProjectRepository;
        }

        public async Task<IEnumerable<InvestigationProjectDTO>> GetAllAsync()
        {
            return await _investigationProjectRepository.GetAllAsync();
        }

        public async Task<IEnumerable<InvestigationProjectDTO>?> GetFirstProjects(int idGroup)
        {
            return await _investigationProjectRepository.GetFirstProjects(idGroup);
        }

        public async Task<InvestigationProject?> GetByIdAsync(int id)
        {
            return await _investigationProjectRepository.GetByIdAsync(id);
        }

        public async Task<int> GetIDByNameAsync(string name)
        {
            return await _investigationProjectRepository.GetIDByNameAsync(name); ;
        }


        public async Task<int> GetProjectsByTermCountAsync(string term, int idGroup)
        {
            return await _investigationProjectRepository.GetProjectsByTermCount(term, idGroup);
        }

        public async Task<IEnumerable<InvestigationProject>?> GetProjectsByTermPagedAsync(int currentPage, int size, string term, int idGroup)
        {
            return await _investigationProjectRepository.GetProjectsByTermPaged(currentPage, size, term, idGroup);
        }

        public async Task<int> GetProjectsCountAsync(int idGroup)
        {
            return await _investigationProjectRepository.GetProjectsCount(idGroup);
        }

        public async Task<int> GetActiveProjectsCountAsync(int groupId)
        {
            return await _investigationProjectRepository.GetActiveProjectsCount(groupId);
        }

        public async Task<IEnumerable<InvestigationProject>?> GetProjectsPagedAsync(int currentPage, int size, int idGroup)
        {
            return await _investigationProjectRepository.GetProjectsPaged(currentPage, size, idGroup);
        }

        public async Task<IEnumerable<InvestigationProject>?>
            GetActiveProjectsPagedAsync(int currentPage, int size, int groupId)
        {
            return await _investigationProjectRepository.GetActiveProjectsPaged(currentPage,
                                                                                size,
                                                                                groupId);
        }

        public async Task DeleteProject(int id)
        {
            await _investigationProjectRepository.DeleteProject(id);
        }

        public async Task AddProjectAsync(InvestigationProject project)
        {
            await _investigationProjectRepository.SaveAsync(project);
        }
        public async Task AddImageAsync(ProjectsImages projectImage)
        {
            await _investigationProjectRepository.SaveImageAsync(projectImage);
        }

        public async Task RemoveImageAsync(string url, int projectId)
        {
            await _investigationProjectRepository.RemoveImageAsync(url,projectId);
        }

        public async Task<IList<ProjectsImages>> GetImagesAsync(int id)
        {
            return await _investigationProjectRepository.GetImagesAsync(id);
        }

        public async Task UpdateProject(int id, string Name, DateTime StartDate, DateTime EndDate, int groupid, string description, string summary, string image)
        {
            await _investigationProjectRepository.UpdateAsync(id, Name, StartDate, EndDate,groupid, description,summary,image);
        }

        public int GetLastProjectId()
        {
            return _investigationProjectRepository.GetLastProjectId();
        }

        public async Task<IList<InvestigationProject>> GetProjectsFromId(IList<int> ids)
        {
           return await _investigationProjectRepository.GetProjectsFromId(ids);
        }

        public async Task ChangeProjectState(int projectId, bool state)
        {
            var project = await GetByIdAsync(projectId);

            if (project is not null)
            {
                project.ChangeProjectState(state);
                await _investigationProjectRepository.SaveAsync(project);
            }
        }
    }
}