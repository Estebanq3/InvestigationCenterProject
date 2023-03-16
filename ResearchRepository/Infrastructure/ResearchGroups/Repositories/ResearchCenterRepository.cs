using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchGroups.DTOs;
using ResearchRepository.Domain.ResearchGroups.Repositories;
using ResearchRepository.Infrastructure.ResearchGroups;
using ResearchRepository.Domain.ResearchAreas.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ResearchRepository.Infrastructure.ResearchGroups.Repositories
{
    internal class ResearchCenterRepository : IResearchCenterRepository
    {
        private readonly ResearchGroupsDbContext _dbContext;
        public IUnitOfWork UnitOfWork => _dbContext;

        public ResearchCenterRepository(ResearchGroupsDbContext unitOfWork)
        {
            _dbContext = unitOfWork;
        }
        public async Task DeleteGroupAsync(ResearchGroup group)
        {
            _dbContext.Remove(group);
            await _dbContext.SaveEntitiesAsync();
        }

        public async Task SaveGroupAsync(ResearchGroup group)
        {
            var groupToInsert = await _dbContext.Groups.FirstOrDefaultAsync(g => g.Id == group.Id);
            if (groupToInsert != null)
            {
                _dbContext.Entry(groupToInsert).CurrentValues.SetValues(group);
            }
            else
            {
                _dbContext.Attach(group).State = EntityState.Added;
                _dbContext.Groups.Add(group);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ResearchCenterDTO>> GetAllAsync()
        {
            return await _dbContext.Centers.Select(c => new ResearchCenterDTO(c.Id, c.Name)).ToListAsync();
        }

        public async Task<IList<ResearchCenter>> GetAllCenters() {
            return await _dbContext.Centers.ToListAsync();
        }
        public async Task<IList<ResearchGroup>> GetAllGroupsFromCenter(int centerId) {
            return await _dbContext.Groups.Where(g => g.Center.Id == centerId).ToListAsync();
        }
        public async Task<ResearchCenter?> GetByIdAsync(int id)
        {
            return await _dbContext.Centers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<ResearchGroup>?> GetGroupsPaged(int centerId, int currentPage, int size)
        {
            return await _dbContext.Groups.Where(g => g.Center.Id == centerId )
                                          .Skip((currentPage - 1) * size).Take(size)
                                          .ToListAsync();
        }

        public async Task<IEnumerable<ResearchGroup>?> GetActiveGroupsPaged(int centerId, int currentPage, int size)
        {
            return await _dbContext.Groups.Where(g => g.Center.Id == centerId && g.Active)
                                          .Skip((currentPage - 1) * size).Take(size)
                                          .ToListAsync();
        }

        public async Task<int> GetGroupsCount(int centerId)
        {
            var count = await _dbContext.Groups.Where(g => g.Center.Id == centerId).CountAsync();
            return count;
        }

        public async Task<int> GetActiveGroupsCount(int idCenter) {
            var count = await _dbContext.Groups.Where(g => g.Center.Id == idCenter && g.Active).CountAsync();
            return count;
        }

        public async Task<IEnumerable<ResearchGroup>?> GetGroupsByTermPaged(int centerId, int currentPage, int size, string term)
        {
            return await _dbContext.Groups.Where(g => g.Active && ((string)(object)g.Name).Contains(term)).Take(size).Skip((currentPage - 1) * size).ToListAsync();
        }

        public async Task<int> GetGroupsByTermCount(int centerId, string term)
        {
            return await _dbContext.Groups.Where(g => g.Active && ((string)(object)g.Name).Contains(term)).CountAsync();
        }

        public async Task<IEnumerable<ResearchGroup>?> GetAllGroupsByTermPaged(int centerId, int currentPage, int size, string term)
        {
            return await _dbContext.Groups.Where(g => ((string)(object)g.Name).Contains(term)).Take(size).Skip((currentPage - 1) * size).ToListAsync();

        }

        public async Task<int> GetAllGroupsByTermCount(int centerId, string term)
        {
            return await _dbContext.Groups.Where(g => ((string)(object)g.Name).Contains(term)).CountAsync();
        }

        public async Task<IEnumerable<ResearchGroup>?> GetGroupsByAreaListAndTermPaged(int centerId, int currentPage, int size, HashSet<ResearchArea> area, string term)
        {
            var listAreas = new DataTable();
            listAreas.Columns.Add("AreaId", typeof(int));
            foreach (var rArea in area)
                listAreas.Rows.Add(rArea.Id);

            var pList = new SqlParameter("@list", SqlDbType.Structured);
            pList.TypeName = "dbo.ListArea";
            pList.Value = listAreas;

            var result = await _dbContext.Groups.FromSqlRaw(
                    "EXEC SP_GetGroupsByAreaAndTermPaged @centerId, @term, @list, @currentPage, @size",
                    new SqlParameter("@centerId", centerId),
                    new SqlParameter("@term", term),
                    pList,
                    new SqlParameter("@currentPage", currentPage),
                    new SqlParameter("@size", size)
                    ).ToListAsync();
            return result;
        }

        public async Task<int> GetGroupsByAreaListAndTermCount(int centerId, HashSet<ResearchArea> area, string term)
        {
            var listAreas = new DataTable();
            listAreas.Columns.Add("AreaId", typeof(int));
            foreach (var rArea in area)
                listAreas.Rows.Add(rArea.Id);

            var pList = new SqlParameter("@list", SqlDbType.Structured);
            pList.TypeName = "dbo.ListArea";
            pList.Value = listAreas;

            var result = _dbContext.ScalarIntValue.FromSqlRaw(
                    "EXEC SP_GetGroupsByAreaAndTermCount @centerId, @term, @list",
                    new SqlParameter("@centerId", centerId),
                    new SqlParameter("@term", term),
                    pList)
                    .AsEnumerable().FirstOrDefault();

            if (result == null)
            {
                return 0;
            }

            return result.Value;
        }

        public async Task<IEnumerable<ResearchGroup>?> GetGroupsByAreaListPaged(int centerId, int currentPage, int size, HashSet<ResearchArea> area)
        {
            var listAreas = new DataTable();
            listAreas.Columns.Add("AreaId", typeof(int));
            foreach(var rArea in area)
                listAreas.Rows.Add(rArea.Id);

            var pList = new SqlParameter("@list", SqlDbType.Structured);
                pList.TypeName = "dbo.ListArea";
                pList.Value = listAreas;

            var result = await _dbContext.Groups.FromSqlRaw(
                    "EXEC SP_GetGroupsByAreaPaged @centerId, @list, @currentPage, @size",
                    new SqlParameter("@centerId", centerId),
                    pList,
                    new SqlParameter("@currentPage", currentPage),
                    new SqlParameter("@size", size)
                    ).ToListAsync();
            return result;
        }

        public async Task<int> GetGroupsByAreaListCount(int centerId, HashSet<ResearchArea> area)
        {
            var listAreas = new DataTable();
            listAreas.Columns.Add("AreaId", typeof(int));
            foreach (var rArea in area)
                listAreas.Rows.Add(rArea.Id);

            var pList = new SqlParameter("@list", SqlDbType.Structured);
            pList.TypeName = "dbo.ListArea";
            pList.Value = listAreas;

            var result = _dbContext.ScalarIntValue.FromSqlRaw(
                    "EXEC SP_GetGroupsByAreaCount @centerId, @list",
                    new SqlParameter("@centerId", centerId),
                    pList)
                    .AsEnumerable().FirstOrDefault();

            if (result == null)
            {
                return 0;
            }

            return result.Value; 
        }

        public async Task<IList<ResearchGroup>> GetAllGroups() {
            return await _dbContext.Groups.ToListAsync();
        }
    }
}