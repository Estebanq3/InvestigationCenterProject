using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Theses.Entities;
using ResearchRepository.Domain.Theses.DTOs;
using ResearchRepository.Domain.Theses.Repositories;
using ResearchRepository.Infrastructure.Theses;
using Microsoft.Data.SqlClient;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Infrastructure.Theses.Repositories
{
    internal class ThesesRepository : IThesesRepository
    {
        private readonly ThesesDbContext _dbContext;
        public IUnitOfWork UnitOfWork => _dbContext;

        public ThesesRepository(ThesesDbContext unitOfWork)
        {
            _dbContext = unitOfWork;
        }

        public async Task<IEnumerable<ThesisDTO>> GetAllAsync()
        {
            return await _dbContext.Theses.Select(t => new ThesisDTO(t.Id, t.Name, t.PublicationDate, t.Summary,t.InvestigationGroupId, t.Image, t.DOI, t.Type,t.Reference)).ToListAsync();
        }

        public async Task<Thesis?> GetByIdAsync(int id)
        {
            return await _dbContext.Theses.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Thesis?> GetByNameAsync(string name, DateTime publicationDate)
        {
            var thesis = _dbContext.Theses.FromSqlRaw("EXEC GetIdByNameTheses @name, @publicationDate",
            new SqlParameter("@name", name), new SqlParameter("@publicationDate",publicationDate)).AsEnumerable().FirstOrDefault();
            return thesis;
        }

        public async Task<int> GetThesesCount(int idGroup)
        {
            var count = await _dbContext.Theses.Where(t => t.Id != null && t.InvestigationGroupId == idGroup).CountAsync();
            return count;
        }

        public async Task<int> GetActiveThesesCount(int groupId)
        {
            var count = await _dbContext.Theses.Where(t => t.Id != null
                                                      && t.InvestigationGroupId == groupId
                                                      && t.Active)
                                               .CountAsync();

            return count;
        }

        public async Task<IEnumerable<Thesis>?> GetThesesByTermPaged(int currentPage, int size, string term, int idGroup)
        {
            //Gets all groups by center ID
            var data = await _dbContext.Theses
             .Where(t => t.Id != null && t.InvestigationGroupId == idGroup && t.Active)
             .ToListAsync();

            //Local filter
            //Slower than doing all in SQL
            return await Task.FromResult(data.Where(t => t.Name.ToString().ToLower().Contains(term.ToLower()))
             .Skip((currentPage - 1) * size).Take(size));
        }

        public async Task<int> GetThesesByTermCount(string term, int idGroup)
        {
            //Gets all groups by center ID
            var data = await _dbContext.Theses
             .Where(t => t.Id != null && t.InvestigationGroupId == idGroup && t.Active)
             .ToListAsync();

            //Local filter
            //Slower than doing all in SQL
            return await Task.FromResult(data
                .Where(t => t.Name.ToString().ToLower().Contains(term.ToLower()))
                .Count());
        }

        public async Task<IEnumerable<ThesisDTO>?> GetThesesByGroupID(long idGroup) {
            return await _dbContext.Theses
             .Where(t => t.InvestigationGroupId == idGroup).OrderByDescending(t => t.PublicationDate)
             .Select(t => new ThesisDTO(t.Id, t.Name, t.PublicationDate, t.Summary, t.InvestigationGroupId,t.DOI, t.Image,t.Type,t.Reference))
             .ToListAsync();

        }

        public async Task<IEnumerable<Thesis>?> GetThesesPaged(int currentPage, int size, int idGroup)
        {
            return await _dbContext.Theses.Where(t => t.Id != null && t.InvestigationGroupId == idGroup)
                                          .Skip((currentPage - 1) * size).Take(size)
                                          .ToListAsync();
        }

        public async Task<IEnumerable<Thesis>?> GetActiveThesesPaged(int currentPage,
                                                                     int size,
                                                                     int groupId)
        {
            return await _dbContext.Theses.Where(t => t.Id != null
                                                 && t.InvestigationGroupId == groupId
                                                 && t.Active)
                                          .Skip((currentPage - 1) * size).Take(size)
                                          .ToListAsync();
        }

        public async Task SaveAsync(Thesis thesis)
        {
            _dbContext.Update(thesis);
            await _dbContext.SaveEntitiesAsync();
        }

        public async Task SaveProjectPartOfThesisAsync(ThesisPartOfProject thesisPartOfProject)
        {
            _dbContext.ThesisPartOfProject.Add(thesisPartOfProject);
            await _dbContext.SaveEntitiesAsync();
        }

        public Task DeleteThesis(int id)
        {
            var _thesisid = (from t in _dbContext.Theses
                              where t.Id == id
                              select t);
            _dbContext.Theses.RemoveRange(_thesisid);
            _dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteProjectPartOfThesis(int id)
        {
            var _projectid = (from t in _dbContext.ThesisPartOfProject
                             where t.InvestigationProjectId == id
                             select t);
            _dbContext.ThesisPartOfProject.RemoveRange(_projectid);
            _dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteThesisPartOfProject(int id)
        {
            var _thesisid = (from t in _dbContext.ThesisPartOfProject
                              where t.ThesisId == id
                              select t);
            _dbContext.ThesisPartOfProject.RemoveRange(_thesisid);
            _dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<IList<ThesisPartOfProject>> GetAsyncThesisPartOfProjectFromId(int projectId)
        {
            return await _dbContext.ThesisPartOfProject.Where(p => p.InvestigationProjectId == projectId).ToListAsync();
        }

        public async Task<IList<ThesisPartOfProject>> GetAsyncProjectsPartOfThesisFromId(int thesisId)
        {
            return await _dbContext.ThesisPartOfProject.Where(p => p.ThesisId == thesisId).ToListAsync();
        }
        public async Task<IList<int>> GetProjectFromThesisId(int thesisId)
        {
            IList<ThesisPartOfProject> projectPartOfThesis = await GetAsyncProjectsPartOfThesisFromId(thesisId);

            var projects = from thesisP in projectPartOfThesis
                         select thesisP.InvestigationProjectId;
            return projects.ToList();
        }

    public async Task<IList<ThesisPartOfProject>> GetThesisOfProjectById(IList<ThesisPartOfProject> _thesis)
        {
            Thesis currentThesis;
            foreach (var a in _thesis)
            {
                currentThesis = (await _dbContext.Theses
                    .Where(p => p.Id == a.ThesisId)
                    .ToListAsync()).First();

                a.Thesis = currentThesis;
            }
            return _thesis;
        }
        public async Task UpdateAsync(int id, string name, DateTime publicationDate,
                                      string summary, long groupId, String image,
                                      String doi, String type, String reference,
                                      byte[]? attachment, String? attachmentName)
        {
            var _thesisId = (from thesis in _dbContext.Theses
                             where thesis.Id == id
                             select thesis).SingleOrDefault();

            if (_thesisId is not null)
            {
                _thesisId.Name = name;
                _thesisId.PublicationDate = publicationDate;
                _thesisId.Summary = summary;
                _thesisId.InvestigationGroupId = groupId;
                _thesisId.Image = image;
                _thesisId.DOI = doi;
                _thesisId.Type = type;
                _thesisId.Reference = reference;
                _thesisId.Attachment = attachment;
                _thesisId.AttachmentName = attachmentName;
            }

            _dbContext.SaveChanges();
            await _dbContext.SaveEntitiesAsync();
        }
        public int GetLastThesisId()
        {
            int id;
            Task.WaitAll();
            if (_dbContext.Theses.Count() != 0)
            {
                id = _dbContext.Theses.Max(t => t.Id);
            }
            else
            {
                id = 1;
            }
            return id;
        }

        public async Task<IList<Thesis>> GetThesesFromIds(IList<int> ids)
        {
            IList<Thesis> theses = new List<Thesis>();
            foreach (int id in ids)
            {
                theses.Add(await GetByIdAsync(id));
            }
            IList<Thesis> ordered = theses.OrderByDescending(e => e.PublicationDate).ToList();
            return ordered;
        }
        public async Task<IList<ResearchAreaThesis>> GetAssociatedAreas(int id)
        {
            IList<ResearchAreaThesis> areas = await _dbContext.ResearchAreaThesis.Where(r => r.ThesisId == id).ToListAsync();

            return areas;
        }
        public async Task<ResearchAreaThesis> GetResearchAreaThesisAssociation(int thesisId, int areaId)
        {
            return (await _dbContext.ResearchAreaThesis
                .Where(r => r.ResearchAreasId == areaId && r.ThesisId == thesisId)
                .ToListAsync()).First();
        }
        public async Task AddAssociatedArea(ResearchAreaThesis association)
        {
            _dbContext.ResearchAreaThesis.Add(association);
            await _dbContext.SaveEntitiesAsync();
        }
        public async Task DeleteAssociatedArea(int thesisId, int areaId)
        {
            var association = await GetResearchAreaThesisAssociation(thesisId, areaId);
            if (association != null)
            {
                _dbContext.ResearchAreaThesis.Remove(association);
                _dbContext.SaveChanges();
            }
         }

    }
}