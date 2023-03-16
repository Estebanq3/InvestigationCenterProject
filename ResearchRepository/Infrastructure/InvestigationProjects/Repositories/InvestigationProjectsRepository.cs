using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.InvestigationProjects.Entities;
using ResearchRepository.Domain.InvestigationProjects.DTOs;
using ResearchRepository.Domain.InvestigationProjects.Repositories;
using ResearchRepository.Infrastructure.InvestigationProjects;
using ResearchRepository.Domain.Core.ValueObjects;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Infrastructure.InvestigationProjects.Repositories
{
    internal class InvestigationProjectsRepository : IInvestigationProjectsRepository
    {
        private readonly InvestigationProjectsDbContext _dbContext;
        public IUnitOfWork UnitOfWork => _dbContext;

        public InvestigationProjectsRepository(InvestigationProjectsDbContext unitOfWork)
        {
            _dbContext = unitOfWork;
        }

        public async Task<IEnumerable<InvestigationProjectDTO>> GetAllAsync()
        {
            return await _dbContext.Projects.Select(p => new InvestigationProjectDTO(p.Id, p.Name, p.StartDate, p.EndDate, p.InvestigationGroupID, p.Description, p.Summary, p.Image)).ToListAsync();
        }

        public async Task<IEnumerable<InvestigationProjectDTO>?> GetFirstProjects(int idGroup)
        {

            return await _dbContext.Projects.Where(p => p.Id != null && p.InvestigationGroupID == idGroup).OrderByDescending(p =>p.StartDate)
            .Select(p => new InvestigationProjectDTO(p.Id, p.Name, p.StartDate, p.EndDate, p.InvestigationGroupID, p.Description, p.Summary, p.Image)).Take(3).ToListAsync();
        }

        public async Task<InvestigationProject?> GetByIdAsync(int id)
        {
            var result = _dbContext.Projects.FromSqlRaw("EXEC GetByIdAsync @Id",
            new SqlParameter("@Id", id)).AsEnumerable().FirstOrDefault();
            return result;
        }

        public async Task<int> GetProjectsCount(int idGroup)
        {
            var count = _dbContext.ScalarIntValue.FromSqlRaw("EXEC GetProjectsCountById @IdGroup",
            new SqlParameter("@IdGroup", idGroup)).AsEnumerable().FirstOrDefault();

            if (count == null)
            {
                return 0;
            }
            return count.Value;
        }

        public async Task<int> GetActiveProjectsCount(int groupId)
        {
            var count = await _dbContext.Projects.Where(p => p.Id != null
                                                        && p.InvestigationGroupID == groupId
                                                        && p.Active)
                                                 .CountAsync();

            return count;
        }

        public async Task<IEnumerable<InvestigationProject>?> GetProjectsByTermPaged(int currentPage, int size, string term, int idGroup)
        {
            //Gets all groups by center ID
            var data = await _dbContext.Projects
             .Where(p => p.Id != null && p.InvestigationGroupID == idGroup && p.Active)
             .ToListAsync();

            //Local filter
            //Slower than doing all in SQL
            return await Task.FromResult(data.Where(p => p.Name.ToString().ToLower().Contains(term.ToLower()))
             .Skip((currentPage - 1) * size).Take(size));
        }

        public async Task<int> GetProjectsByTermCount(string term, int idGroup)
        {
            //Gets all groups by center ID
            var data = await _dbContext.Projects
             .Where(p => p.Id != null && p.InvestigationGroupID == idGroup && p.Active)
             .ToListAsync();

            //Local filter
            //Slower than doing all in SQL
            return await Task.FromResult(data
                .Where(p => p.Name.ToString().ToLower().Contains(term.ToLower()))
                .Count());
        }

        public async Task<int> GetIDByNameAsync(string name)
        {
            var project =  await _dbContext.Projects.FirstOrDefaultAsync(p => p.Name == name);
            return project.Id;
        }


        public async Task<IEnumerable<InvestigationProject>?> GetProjectsPaged(int currentPage, int size, int idGroup)
        {
            return await _dbContext.Projects.Where(p => p.Id != null && p.InvestigationGroupID == idGroup)
                                          .Skip((currentPage - 1) * size).Take(size)
                                          .ToListAsync();
        }

        public async Task<IEnumerable<InvestigationProject>?>
            GetActiveProjectsPaged(int currentPage, int size, int groupId)
        {
            return await _dbContext.Projects.Where(p => p.Id != null
                                                   && p.InvestigationGroupID == groupId
                                                   && p.Active)
                                            .Skip((currentPage - 1) * size).Take(size)
                                            .ToListAsync();
        }

        public Task DeleteProject(int id)
        {
            var _projectid = (from p in _dbContext.Projects
                              where p.Id == id
                              select p);
            _dbContext.Projects.RemoveRange(_projectid);
            _dbContext.SaveChanges();
            return Task.CompletedTask;
        }
        public async Task SaveAsync(InvestigationProject project)
        {
            _dbContext.Update(project);
            await _dbContext.SaveEntitiesAsync();
        }

        public async Task SaveImageAsync(ProjectsImages projectImage)
        {
            _dbContext.Add(projectImage);
            await _dbContext.SaveEntitiesAsync();
        }

         public async Task RemoveImageAsync(string url, int projectId)
        {
            var _image= (from i in _dbContext.ProjectsImage
                              where i.Image == url && i.ProjectId == projectId
                              select i);
            _dbContext.ProjectsImage.RemoveRange(_image);
            _dbContext.SaveChanges();
        }

        public async Task<IList<ProjectsImages>> GetImagesAsync(int id)
        { 
        
            return await _dbContext.ProjectsImage.Where(i => i.ProjectId == id).ToListAsync();
        }


        public async Task UpdateAsync(int id, string Name, DateTime  StartDate, DateTime EndDate, int groupid, string description, string summary, string image) 
        {
            var _projectid = (from p in _dbContext.Projects
                              where p.Id == id
                              select p).SingleOrDefault();
            if (_projectid is not null)
            {
                Regex injectionPattern = new Regex(@"(?i)\b(ALTER|CREATE|DELETE|DROP|EXEC(UTE){0,1}|INSERT( +INTO){0,1}|MERGE|SELECT|UPDATE|UNION( +ALL){0,1})\b");

                Match validateName = injectionPattern.Match(Name);
                Match validateDescription = injectionPattern.Match(description);
                Match validateSummary = injectionPattern.Match(summary);

                if (validateName.Success && validateDescription.Success && validateSummary.Success)
                {
                    _projectid.Name = Name;
                    _projectid.StartDate = StartDate;
                    _projectid.EndDate = EndDate;
                    _projectid.InvestigationGroupID = groupid;
                    _projectid.Description = description;
                    _projectid.Summary = summary;
                    _projectid.Image = image;

                    _dbContext.SaveChanges();
                    await _dbContext.SaveEntitiesAsync();
                }
            }
        }

        public int GetLastProjectId()
        {
            int id;
            if (_dbContext.Projects.Count() != 0)
            {
                id = _dbContext.Projects.Max(p => p.Id);
            }
            else {
                id = 1;
            }
            
            return id;
        }

        public async Task<IList<InvestigationProject>> GetProjectsFromId(IList<int> ids)
        {
            IList<InvestigationProject> projects = new List<InvestigationProject>();
            foreach (int id in ids)
            {
                projects.Add(await GetByIdAsync(id));
            }
            IList<InvestigationProject> ordered = projects.OrderByDescending(e => e.StartDate).ToList();
            return ordered;
        }



    }
}