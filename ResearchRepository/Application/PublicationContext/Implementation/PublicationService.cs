using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Application.PublicationContext;
using ResearchRepository.Domain.Repositories;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.PublicationContext;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.PublicationContext.Repositories;
using ResearchRepository.Domain.PublicationContext.Entities;

namespace ResearchRepository.Application.PublicationContext.Implementation
{
    public class PublicationService : IPublicationService
    {
        private readonly IPublicationRepository _publicationRepository;

        public PublicationService(IPublicationRepository publicationRepository)
        {

            _publicationRepository = publicationRepository;

        }

        /// <summary>
        /// This method requests a list of all the publications from infrastructure
        /// </summary>
        /// Author: Christian Rojas
        /// StoryID: ST-PH-1.1
        /// <returns>List of all publications</returns>
        public async Task<IList<Publication>> GetAsync()
        {
            return await _publicationRepository.GetAsync();
        }

        /// <summary>
        /// This method provides the connection with the publication method checkPublicationSummary
        /// </summary>
        /// Author: Christian Rojas
        /// StoryID: ST-PH-1.1
        /// <param name="publication"></param>
        public void summaryCheck(Publication publication)
        {

            publication.checkPublicationSummary();
        }

        /// <summary>
        /// Get the total number to Publication in id groud
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-1.2
        /// <returns>Total number of Publication</returns> 
        public async Task<int> GetPublicationCountAsync(int id)
        {
            return await _publicationRepository.GetPublicationCountAsync(id);
        }

        /// <summary>
        /// Get the total number to Publication  in id groud
        /// </summary>
        /// Author::Elvis Badilla
        /// StoryID: ST-PH-1.2
        /// <param name="term">Search term to use in the query</param>
        /// <returns>Total number of Publication </returns>
        public async Task<int> GetPublicationByTermCountAsync(int id, string term)
        {

            return await _publicationRepository.GetPublicationByTermCountAsync(id, term);
        }

        /// <summary>
        /// Get the Publication by current page  in id groud
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-1.2
        /// <param name="currentPage">Current page</param>
        /// <param name="size">Number of Publication to retreive per page</param>
        /// <returns>List of Publication given the page and the size</returns>
        public async Task<IList<Publication>> GetPublicationPagedAsync(int id, int currentPage, int size)
        {
            return await _publicationRepository.GetPublicationPagedAsync(id, currentPage, size);
        }

        /// <summary>
        /// This method provides the connection with the publication method getPublicationByGroup  
        /// </summary>
        /// Author: Christian Rojas
        /// StoryID: ST-PH-1.1
        /// <param name="id"></param>
        /// <returns>List of publications</returns>
        public async Task<IList<Publication>> getPublicationByGroup(int id)
        {

            return await _publicationRepository.getPublicationByGroup(id);
        }
        /// <summary>
        /// Get the Publication by current page  in id groud and term 
        /// <param name="publicationId">ID of a Publication.</param>
        /// <param name="currentPage">The page currently being browsed.</param>
        /// <param name="size">Number of publications per page.</param>
        /// <param name="query">The text to be used as search query.</param>
        /// <returns>Ammount of searched Publications.</returns>
        public async Task<IList<Publication>?> GetPublicationByTermPagedAsync(int id, int currentPage, int size, string term)
        {
            return await _publicationRepository.GetPublicationByTermPagedAsync(id, currentPage, size, term);
        }
        /// <summary>
        /// Get the Publication by current page  in id groud and term
        /// </summary>
        /// Author: Elvis Badilla and Diana Luna
        /// StoryID: ST-PH-3.5
        /// <param name="currentPage">Current page</param>
        /// <param name="size">Number of Publication to retreive per page</param>
        /// <param name="query">The text to be used as search query.</param>
        /// <returns>List of Publication given the page and the size</returns>
       public async Task<IList<Publication>?> GetPublicationByTermPagedSummary(int id, int currentPage, int size, string term)
        {
            return await _publicationRepository.GetPublicationByTermPagedSummary(id, currentPage, size, term);
        }

        /// <summary>
        /// Get the total number to Publication in id group
        /// </summary>
        /// Author: Diana Luna & Elvis Badilla
        /// StoryID: ST-PH-3.5_ActividadSupervisada
        /// <param name="id">Search id of the publication</param>
        /// <param name="term">Search term to use in the query</param>
        /// <returns>Total number of publications by summary </returns>
        public async Task<int> GetPublicationByTermCountAsyncSummary(int id, string term)
        {

            return await _publicationRepository.GetPublicationByTermCountAsyncSummary(id, term);
        }

        /// <summary>
        /// Obtain a publication regarding a DOI string that identifies it.
        /// </summary>
        /// Author: David Sánchez López
        /// StoryID: ST-PH-3.1
        /// <param name="publicationId">The ID of the publication.</param>
        /// <returns>A publication object who's DOI matches the ID sent.</returns>
        public async Task<Publication?> GetPublicationById(string publicationId)
        {
            return await _publicationRepository.GetPublicationById(publicationId);
        }

        /// <summary>
        /// add Publication in database
        /// </summary>
        /// Elvis Badilla
        /// StoryID: ST-PH-4.1
        /// <param name="publication">publication </param>
        public async Task AddPublicationAsync(Publication publication)
        {
            await _publicationRepository.SaveAsync(publication);
        }
        /// <summary>
        /// add thesis asociation of Publication in database
        /// </summary>
        ///Elvis Badilla
        /// StoryID: ST-PH-4.4
        /// <param name="publicationPartOfTesis">publication Part Of Tesis </param>
        public async Task AddPublicationPartOfTesisAsync(PublicationPartOfTesis publicationPartOfTesis)
        {
            await _publicationRepository.SavePublicationPartOfTesisAsync(publicationPartOfTesis);
        }

        ///<summary>
        /// Add an investigation project to a publication
        /// </summary>
        /// Author: Diana Luna 
        /// StoryID: ST-PH-4.5_v2
        /// <param name="projectAsociatedToPublication">Applies the project asociated to be saved</param>
        public async Task AddProjectAsociatedAsync(ProjectAsociatedToPublication projectAsociatedToPublication)
        {
            await _publicationRepository.SaveProjectAsociatedAsync(projectAsociatedToPublication);
        }

        public async Task<IList<Publication>> GetPublicationsFromId(IList<string> ids)
        {
            return await _publicationRepository.GetPublicationsFromId(ids);
        }

        public async Task<List<int>> GetPublicationPartOfTesisAsync(string idPublication)
        {
            return await _publicationRepository.GetPublicationPartOfTesisAsync(idPublication);

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
            return await _publicationRepository.GetProjectsAsociatedAsync(publicationId);
        }
        public async Task<IList<Publication>> GetPublicationByAuthor(IList<string> authors, int groupId)
        {
            return await _publicationRepository.GetPublicationByAuthor(authors, groupId);
        }
        public async Task UndatePublicationAsync(Publication publication)
        {
            await _publicationRepository.UndatePublicationAsync(publication);
        }
        public async Task<Publication> IdEqual(string id)
        {
            return await _publicationRepository.IdEqual(id);
        }

        public async Task<IList<ReferenceListPublication>> GetReferencesById(string id) {


            return await _publicationRepository.GetReferencesById(id);

        
        }

        public async Task DeletePublication(string id)
        {
            await _publicationRepository.DeletePublication(id);
        }
        



        public async Task DeletePublicationPartOfThesis(string id)
        {
            await _publicationRepository.DeletePublicationPartOfThesis(id);
        }

        public async Task<IList<PublicationPartOfTesis>> GetAsyncPublicationsPartOfThesisFromId(int thesisId)
        {
            return await _publicationRepository.GetAsyncPublicationsPartOfThesisFromId(thesisId);
        }
        public async Task SavePublicationPartOfThesisAsync(PublicationPartOfTesis publicationPartOfTesis)
        {
            await _publicationRepository.SavePublicationPartOfThesisAsync(publicationPartOfTesis);
        }

        
        public async Task<List<string>> GetPublicationsAsociatedToProjectAsync(int projectId)
        {
            return await _publicationRepository.GetPublicationsAsociatedToProjectAsync(projectId);
        }

        public async Task DeletePublicationPartOfProject(string id)
        {
            await _publicationRepository.DeletePublicationPartOfProject(id);
        }

        public async Task<List<string>> GetPublicationsAsociatedToThesisAsync(int thesisId)
        {
            return await _publicationRepository.GetPublicationsAsociatedToThesisAsync(thesisId);
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
            return await _publicationRepository.GetAssociatedAreas(id);
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
            return await _publicationRepository.GetResearchAreaPublicationAssociation(publicationId, areaId);
        }

        /// <summary>
        /// Add an association between a publication and a research area.
        /// </summary>
        /// Author: David Sánchez & Diana Luna
        /// StoryID: ST-PH-4.8
        /// <param name="association"> ResearchAreaPublication object to add to the table. </param>
        public async Task AddAssociatedArea(ResearchAreaPublication association)
        {
            await _publicationRepository.AddAssociatedArea(association);
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
            await _publicationRepository.DeleteAssociatedArea(publicationId, areaId);
        }
        public bool VerifyExistenceOfPublication(Publication publication) 
        {
            return _publicationRepository.VerifyExistenceOfPublication(publication);
        }

        /// <summary>
        /// Retrieves a list of publications filtered by DOI, Name and Summary
        /// using the same search term for the tree filters, optimizing the search process.
        /// </summary>
        /// Author: David Sánchez López.
        /// StoryID: ST-PH-1.7.
        /// <param name="searchTerm">The term to filter by.</param>
        /// <returns>A resulting list of publications matching the search term.</returns>
        public async Task<IList<Publication>> GetPublicationByThreeFilters(string searchTerm, int groupId, int currentPage, int pageSize)
        {
            return await _publicationRepository.GetPublicationByThreeFilters(searchTerm, groupId, currentPage, pageSize);
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
            return await _publicationRepository.GetPublicationCountByThreeFilters(searchTerm, groupId, currentPage, pageSize);
        }


        public async Task addReference(string id, string reference, int order)
        {
            await _publicationRepository.addReferenceToPublication(id,reference,order);
        }
        
        public async Task UpdateReference(string id, string reference, int order)
        {
            await _publicationRepository.updateReferenceToPublication(id,reference,order);
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
            return await _publicationRepository.VerifyDOI(doi);
        }

    }
}
