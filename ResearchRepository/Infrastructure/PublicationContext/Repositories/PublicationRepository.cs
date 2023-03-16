using Infrastructure.PublicationContext;
using Microsoft.EntityFrameworkCore;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.PublicationContext;
using ResearchRepository.Domain.PublicationContext.Entities;
using ResearchRepository.Domain.PublicationContext.Repositories;
using ResearchRepository.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ResearchRepository.Infrastructure.PublicationContext.Repositories
{
    public class PublicationRepository : IPublicationRepository
    {
        private readonly PublicationDbContext _dbContext;
        public IUnitOfWork UnitOfWork => _dbContext;


        public PublicationRepository(PublicationDbContext unitOfWork)
        {
            _dbContext = unitOfWork;
        }

        /// <summary>
        /// This method requests all of the publications in the database.
        /// </summary>
        /// Author: Christian Rojas
        /// StoryID: ST-PH-1.1
        /// <returns>list of publications</returns>
        public async Task<IList<Publication>> GetAsync()
        {

            List<Publication> publications = await _dbContext.Publication.Where(g => g.Deleted == false)
               .ToListAsync();

            return publications;

        }

        /// <summary>
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-1.2
        ///<param Id="id">id of groud</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="size">Number of Publication to retreive per page</param>
        /// <returns>List of Publication given the page and the size</returns>
        public async Task<IList<Publication>> GetPublicationPagedAsync(int id, int currentPage, int size)
        {

            IList<Publication> publications = await _dbContext.Publication.Where(g => g.ResearchGroupId == id && g.Deleted == false).ToListAsync();
            publications = new Publication().orderByYear(publications).Skip((currentPage - 1) * size).Take(size).ToList();

            return publications;

        }


        /// <summary>
        /// Get the total number to Publication  in id groud
        /// </summary>
        /// Author: Elvis Badilla
        ///<param Id="id">id of groud</param>
        /// StoryID: ST-PH-1.2
        /// <returns>Total number of Publication</returns>
        public async Task<int> GetPublicationCountAsync(int id)
        {
            var count = await _dbContext.Publication.Where(g => g.ResearchGroupId == id && g.Deleted == false).CountAsync();
            return count;
        }

        /// <summary>
        /// Get the total number to Publication in id groud
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-1.2
        /// <param name="term">Search term to use in the query</param>
        ///<param Id="id">id of groud</param>
        /// <returns>Total number of Publication </returns>
        public async Task<int> GetPublicationByTermCountAsync(int id, string term)
        {
            var result = _dbContext.ScalarIntValue.FromSqlRaw(
                    "EXEC SP_GetPublicationCountByName @groupId, @publicationName",
                    new SqlParameter("@groupId", id),
                    new SqlParameter("@publicationName", term)
                    ).AsEnumerable().FirstOrDefault();

            if (result == null)
            {
                return 0;
            }

            return result.Value;
        }

        /// <summary>
        /// Get a list of Publication by current page and search term in id groud
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-1.2
        /// <param name="currentPage">Current page</param>
        ///<param  Id="id">id of groud</param>
        /// <param name="size">Number of Publication to retreive per page</param>
        /// <param name="term">Search term to use in the query</param>
        /// <returns>List of Publication given the page and the size and search term</returns>
        public async Task<IList<Publication>> GetPublicationByTermPagedAsync(int id, int currentPage, int size, string term)
        {
            //Retrieves all Publications pertaining to a Research Group ID
            var data = await _dbContext.Publication
             .Where(g => g.ResearchGroupId == id && g.Deleted == false)
             .ToListAsync();

            data = new Publication().orderByYear(data).ToList();
            //Filters the retrieved Publications regarding the search terms
            return await _dbContext.Publication.Where(
                   data => data.Deleted == false && data.Name.ToLower().Contains(term.ToLower())
               ).Skip((currentPage - 1) * size).Take(size).ToListAsync();
        }


        /// <summary>
        /// This method takes all the publications related to a research group
        /// </summary>
        /// Author: Christian Rojas
        /// StoryID: ST-PH-1.1
        /// <param name="id"></param>
        /// <returns>List of publications</returns>
        public async Task<IList<Publication>> getPublicationByGroup(int id)
        {
            List<Publication> publications = await _dbContext.Publication
               .Where(publication => publication.Deleted == false && publication.ResearchGroupId == id).OrderByDescending(publication => publication.Year)
               .ToListAsync();

            return publications;
        }
        /// <summary>
        /// Get a list of Publication by current page and search term in id groud
        /// </summary>
        /// Author: Elvis Badilla y Diana Luna
        /// StoryID: ST-PH-3.5
        /// <param name="currentPage">Current page</param>
        ///<param  Id="id">id of groud</param>
        /// <param name="size">Number of Publication to retreive per page</param>
        /// <param name="term">Search term to use in the query</param>
        public async Task<IList<Publication>?> GetPublicationByTermPagedSummary(int id, int currentPage, int size, string term)
        {
            //Retrieves all Publications pertaining to a Research Group ID
            var data = await _dbContext.Publication
             .Where(g => g.ResearchGroupId == id && g.Deleted == false)
             .ToListAsync();

            var publications = await _dbContext.Publication
                .Where(data => data.Deleted == false && data.ResearchGroupId == id && data.Summary.ToLower().Contains(term.ToLower()))
                .ToListAsync();

            publications = new Publication().orderByYear(publications).Skip((currentPage - 1) * size).Take(size).ToList();

            //Filters the retrieved Publications regarding the search terms
            return publications;
        }

        /// <summary>
        /// Get the total number to Publication in id groud
        /// </summary>
        /// Author: Diana Luna & Elvis Badilla
        /// StoryID: ST-PH-3.5_ActividadSupervisada
        /// <param name="id">Search id of the group</param>
        /// <param name="term">Search term to use in the query</param>
        /// <returns>Total number of Publication </returns>
        public async Task<int> GetPublicationByTermCountAsyncSummary(int id, string term)
        {
            //Retrieves all Publications pertaining to a Research Group ID
            var data = await _dbContext.Publication
             .Where(g => g.ResearchGroupId == id && g.Deleted == false)
             .ToListAsync();

            //Filters the retrieved Publications regarding the search terms
            return await Task.FromResult(data
                .Where(g => g.Summary.ToString().ToLower().Contains(term.ToLower()))
                .Count());
        }

        public async Task SaveAsync(Publication publication)
        {
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                var PublicationToInsert = await _dbContext.Publication.FirstOrDefaultAsync(n => n.Id == publication.Id);
                if (PublicationToInsert != null)
                {

                }
                else
                {
                    _dbContext.Attach(publication).State = EntityState.Added;
                    _dbContext.Publication.Add(publication);
                }
                await _dbContext.SaveChangesAsync();
                dbContextTransaction.Commit();
            }
        }

        public async Task SavePublicationPartOfTesisAsync(PublicationPartOfTesis publicationPartOfTesis)
        {
            var publicationPartOfTesisToInsert = await _dbContext.PublicationPartOfTesis.FirstOrDefaultAsync(n => n.PublicationId == publicationPartOfTesis.PublicationId && n.ThesisId == publicationPartOfTesis.ThesisId);
            if (publicationPartOfTesisToInsert != null)
            {
                _dbContext.Entry(publicationPartOfTesisToInsert).CurrentValues.SetValues(publicationPartOfTesis);
            }
            else
            {
                _dbContext.Attach(publicationPartOfTesis).State = EntityState.Added;
                _dbContext.PublicationPartOfTesis.Add(publicationPartOfTesis);
            }

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// This method takes all the information related to a research publication
        /// </summary>
        /// Author: Christian Rojas
        /// StoryID: ST-PH-3.1
        /// <param name="id"></param>
        /// <returns>A publication</returns>
        public async Task<Publication> GetPublicationById(string id)
        {
            Publication publication = await _dbContext.Publication
            .FirstOrDefaultAsync(publication => publication.Deleted == false && publication.Id.Equals(id));

            return publication;
        }

        public async Task<IList<Publication>> GetPublicationsFromId(IList<string> ids)
        {
            IList<Publication> publications = new List<Publication>();
            Publication publication;

            foreach (string id in ids)
            {
                publication = await GetPublicationById(id);

                if (publication != null)
                {
                    publications.Add(publication);
                }
            }
            IList<Publication> ordered = publications.OrderByDescending(e => e.Year).ToList();
            return ordered;
        }

        public async Task<IList<Publication>> GetPublicationByAuthor(IList<string> authors, int groupId)
        {
            Publication currenPublication;
            IList<Publication> currenPublicationrlist = new List<Publication>();
            var data = await _dbContext.Publication
            .Where(g => g.ResearchGroupId == groupId && g.Deleted == false)
            .ToListAsync();


            foreach (var a in authors)
            {
                currenPublication = (await _dbContext.Publication.Where(
                  data => data.Deleted == false && data.Id == a && data.ResearchGroupId == groupId).ToListAsync()).First();
                currenPublicationrlist.Add(currenPublication);

            }

            currenPublicationrlist = new Publication().orderByYear(currenPublicationrlist).ToList();

            return currenPublicationrlist.Skip((1 - 1) * 10).Take(10).ToList();
        }



        ///<summary>
        /// Save an asociated investigation project to a publication in the database
        /// </summary>
        /// Author: Diana Luna 
        /// StoryID: ST-PH-4.5_v2
        /// <param name="projectAsociatedToPublication">Applies the project asociated to be saved</param>
        public async Task SaveProjectAsociatedAsync(ProjectAsociatedToPublication projectAsociatedToPublication)
        {

            var projectAsociatedToPublicationToInsert = await _dbContext.ProjectAsociatedToPublication.FirstOrDefaultAsync(n => n.PublicationId == projectAsociatedToPublication.PublicationId && n.InvestigationProjectId == projectAsociatedToPublication.InvestigationProjectId);
            if (projectAsociatedToPublicationToInsert != null)
            {
                _dbContext.Entry(projectAsociatedToPublicationToInsert).CurrentValues.SetValues(projectAsociatedToPublication);
            }
            else
            {
                _dbContext.Attach(projectAsociatedToPublication).State = EntityState.Added;
                _dbContext.ProjectAsociatedToPublication.Add(projectAsociatedToPublication);
            }

            await _dbContext.SaveChangesAsync();

        }

        public async Task<List<int>> GetPublicationPartOfTesisAsync(string idPublication)
        {
            List<int> result = new List<int>();
            var data = await _dbContext.PublicationPartOfTesis.Where(g => g.PublicationId == idPublication).ToListAsync();
            foreach (var a in data)
            {
                result.Add(a.ThesisId);
            }
            return result;
        }

        /// <summary>
        /// Get a list of all the projects asociated to a publication.
        /// </summary>
        /// Author: Diana Luna
        /// StoryID: ST-PH-4.5_v2
        /// <param name="publicationId"> Id of the publication </param>
        /// <return> The list of the id's from the asociated projects.</return>
        public async Task<List<int>> GetProjectsAsociatedAsync(string publicationId)
        {
            List<int> result = new List<int>();
            var data = await _dbContext.ProjectAsociatedToPublication.Where(g => g.PublicationId == publicationId).ToListAsync();
            foreach (var d in data)
            {
                result.Add(d.InvestigationProjectId);
            }
            return result;
        }

        public async Task UndatePublicationAsync(Publication publication)
        {
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                var PublicationToInsert = await _dbContext.Publication.FirstOrDefaultAsync(n => n.Id == publication.Id);
                if (PublicationToInsert != null)
                {
                    _dbContext.Entry(PublicationToInsert).CurrentValues.SetValues(publication);
                }
                else
                {
                    _dbContext.Attach(publication).State = EntityState.Added;
                    _dbContext.Publication.Add(publication);
                }
                await _dbContext.SaveChangesAsync();
                dbContextTransaction.Commit();
            }
        }
        public async Task<Publication> IdEqual(string id)
        {
            Publication publicationC = await _dbContext.Publication
              .FirstOrDefaultAsync(publication => publication.Deleted == false && publication.Id.Equals(id));


            return publicationC;
        }

        public async Task<IList<ReferenceListPublication>> GetReferencesById(string id)
        {

            IList<ReferenceListPublication> references = await _dbContext.ReferenceListPublication.Where(g => g.IdPublication == id).ToListAsync();

            return references;

        }

        public Task DeletePublication(string id)
        {
            _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted);

            var _publication = (from p in _dbContext.Publication
                                where p.Id == id
                                select p);

            _dbContext.Publication.RemoveRange(_publication);

            _dbContext.SaveChanges();

            _dbContext.Database.CommitTransaction();

            return Task.CompletedTask;
        }

        public async Task addReferenceToPublication(string id, string reference, int order)
        {

            ReferenceListPublication refer = new ReferenceListPublication(id,order,reference);

            _dbContext.ReferenceListPublication.Add(refer);
            await _dbContext.SaveEntitiesAsync();

        }


        public async Task updateReferenceToPublication(string id, string reference, int order)
        {
            ReferenceListPublication refer = await _dbContext.ReferenceListPublication
                .FirstOrDefaultAsync(p => p.IdPublication == id && p.Order == order);

            _dbContext.ReferenceListPublication.RemoveRange(refer);
            await _dbContext.SaveEntitiesAsync();

            refer.Reference = reference;

            _dbContext.ReferenceListPublication.Add(refer);
            await _dbContext.SaveEntitiesAsync();
        }

        public Task DeletePublicationPartOfThesis(string id)
        {
            var _publicationid = (from p in _dbContext.PublicationPartOfTesis
                                  where p.PublicationId == id
                                  select p);
            _dbContext.PublicationPartOfTesis.RemoveRange(_publicationid);
            _dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<IList<PublicationPartOfTesis>> GetAsyncPublicationsPartOfThesisFromId(int thesisId)
        {
            return await _dbContext.PublicationPartOfTesis.Where(p => p.ThesisId == thesisId).ToListAsync();
        }

        public async Task SavePublicationPartOfThesisAsync(PublicationPartOfTesis publicationPartOfTesis)
        {
            _dbContext.PublicationPartOfTesis.Add(publicationPartOfTesis);
            await _dbContext.SaveEntitiesAsync();
        }


        public async Task<List<string>> GetPublicationsAsociatedToProjectAsync(int projectId)
        {
            List<string> result = new List<string>();
            var data = await _dbContext.ProjectAsociatedToPublication.Where(g => g.InvestigationProjectId == projectId).ToListAsync();
            foreach (var d in data)
            {
                result.Add(d.PublicationId);
            }
            return result;
        }

        public Task DeletePublicationPartOfProject(string id)
        {
            var _publicationid = (from p in _dbContext.ProjectAsociatedToPublication
                                  where p.PublicationId == id
                                  select p);
            _dbContext.ProjectAsociatedToPublication.RemoveRange(_publicationid);
            _dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<List<string>> GetPublicationsAsociatedToThesisAsync(int thesisId)
        {
            List<string> result = new List<string>();
            var data = await _dbContext.PublicationPartOfTesis.Where(g => g.ThesisId == thesisId).ToListAsync();
            foreach (var d in data)
            {
                result.Add(d.PublicationId);
            }
            return result;
        }

        /// <summary>
        /// Retrieve a list with all research areas associated to a publication.
        /// </summary>
        /// Author: David Sánchez & Diana Luna
        /// StoryID: ST-PH-4.8
        /// <param name="id"> Id of the publication </param>
        /// <return> A list filled with research areas associated to the publication's ID.</return>
        public async Task<IList<ResearchAreaPublication>> GetAssociatedAreas(string id)
        {
            IList<ResearchAreaPublication> areas = await _dbContext.ResearchAreaPublication.Where(r => r.PublicationsId == id).ToListAsync();

            return areas;
        }

        /// <summary>
        /// Retrieve the association entity of type ResearchAreaPublication matching both IDs.
        /// </summary>
        /// Author: David Sánchez & Diana Luna
        /// StoryID: ST-PH-4.8
        /// <param name="publicationId"> Id of the publication </param>
        /// <param name="areaId"> Id of the area to associate </param>
        /// <return> ResearchAreaPublication object with requested IDs.</return>
        public async Task<ResearchAreaPublication> GetResearchAreaPublicationAssociation(string publicationId, int areaId)
        {
            return (await _dbContext.ResearchAreaPublication
                .Where(r => r.ResearchAreasId == areaId && r.PublicationsId == publicationId)
                .ToListAsync()).First();
        }

        /// <summary>
        /// Add an association between a publication and a research area.
        /// </summary>
        /// Author: David Sánchez & Diana Luna
        /// StoryID: ST-PH-4.8
        /// <param name="association"> ResearchAreaPublication object to add to the table. </param>
        public async Task AddAssociatedArea(ResearchAreaPublication association)
        {
            _dbContext.ResearchAreaPublication.Add(association);
            await _dbContext.SaveEntitiesAsync();
        }

        /// <summary>
        /// Disassociate an area from a publication using both entities's IDs.
        /// </summary>
        /// Author: David Sánchez & Diana Luna
        /// StoryID: ST-PH-4.8
        /// <param name="publicationId"> Id of the publication </param>
        /// <param name="areaId"> Id of the area to associate </param>
        public async Task DeleteAssociatedArea(string publicationId, int areaId)
        {
            var association = await GetResearchAreaPublicationAssociation(publicationId, areaId);

            if (association != null)
            {
                _dbContext.ResearchAreaPublication.Remove(association);
                _dbContext.SaveChanges();
            }
        }

        public bool VerifyExistenceOfPublication(Publication publication)
        {
            bool verify = false;
            var publicationToFind = _dbContext.Publication.Find(publication.Id);

            if (publicationToFind != null)
            {
                verify = true;
            }
            return verify;
        }

        /// <summary>
        /// Retrieves a list of publications filtered by DOI, Name and Summary
        /// using the same search term for the tree filters, optimizing the search process.
        /// </summary>
        /// Author: David Sánchez López.
        /// StoryID: ST-PH-1.7.
        /// <param name="searchTerm">The term to filter by.</param>
        /// <param name="groupId">The id of the group in which to search publications.</param>
        /// <returns>A resulting list of publications matching the search term.</returns>
        public async Task<IList<Publication>> GetPublicationByThreeFilters(string searchTerm, int groupId, int currentPage, int pageSize)
        {
            var result = await _dbContext.Publication.FromSqlRaw(
                    "EXEC SP_GetPublicationByThreefilters @groupId, @searchTerm",
                    new SqlParameter("@groupId", groupId),
                    new SqlParameter("@searchTerm", searchTerm)
                    ).ToListAsync();

            result = new Publication().orderByYear(result).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return result;
        }

        /// <summary>
        /// Calculates the ammount of publications filtered by DOI, Name and Summary.
        /// </summary>
        /// Author: David Sánchez López.
        /// StoryID: ST-PH-1.7.
        /// <param name="searchTerm">The term to filter by.</param>
        /// <param name="groupId">The id of the group in which to search publications.</param>
        /// <returns>The ammount of publications found.</returns>
        public async Task<int> GetPublicationCountByThreeFilters(string searchTerm, int groupId, int currentPage, int pageSize)
        {
            var resultsList = await _dbContext.Publication.FromSqlRaw(
                    "EXEC SP_GetPublicationByThreefilters @groupId, @searchTerm",
                    new SqlParameter("@groupId", groupId),
                    new SqlParameter("@searchTerm", searchTerm)
                    ).ToListAsync();

            resultsList = new Publication().orderByYear(resultsList).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            if (resultsList == null)
            {
                return 0;
            }
            else
            {
                return resultsList.Count();
            }
        }


        /// <summary>
        /// Check whether a publication exists by searching its DOI.
        /// </summary>
        /// Author: David Sánchez.
        ///<param doi="doi">DOI that identifies the publication.</param>
        /// StoryID: ST-PH-4.24
        /// <returns>Boolean value replying if publication exists.</returns>
        public async Task<bool> VerifyDOI(string doi)
        {
            var count = await _dbContext.Publication.Where(p => p.Id == doi && p.Deleted == false).CountAsync();
            
            if(count > 0)
                return true;
            else
                return false;
        }

    }
}