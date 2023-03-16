using ResearchRepository.Domain.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.PublicationContext.Entities;

namespace ResearchRepository.Domain.PublicationContext.Repositories
{
    public interface IPublicationRepository : IRepository<Publication>
    {
        Task<IList<Publication>> GetAsync();
        Task<IList<Publication>> GetPublicationPagedAsync(int id, int currentPage, int size);

        Task<IList<Publication>> getPublicationByGroup(int id);
        /// <summary>
        /// Obtains the ammount of Publications.
        /// Author: David Sánchez López
        /// StoryID: ST-PH-1.3
        /// </summary>
        /// <param name="id">ID of a Publication.</param>
        /// <returns>Ammount of Publications.</returns>
        Task<int> GetPublicationCountAsync(int id);

        /// <summary>
        /// Obtains the ammount of Publications regarding a search query.
        /// Author: David Sánchez López
        /// StoryID: ST-PH-1.3
        /// </summary>
        /// <param name="id">ID of a Publication.</param>
        /// <param name="term">The text to be used as search query.</param>
        /// <returns>Ammount of searched Publications.</returns>
        Task<int> GetPublicationByTermCountAsync(int id, string term);

        /// <summary>
        /// Obtains the ammount of Publications regarding a
        /// search query and the current page being browsed.
        /// Author: David Sánchez López
        /// StoryID: ST-PH-1.3
        /// </summary>
        /// <param name="publicationId">ID of a Publication.</param>
        /// <param name="currentPage">The page currently being browsed.</param> 
        /// <param name="size">Number of publications per page.</param>
        /// <param name="query">The text to be used as search query.</param>
        /// <returns>Ammount of searched Publications.</returns>
        ///Task<IEnumerable<Publication>?> GetPublicationByTermPaged(int publicationId, int currentPage, int size, string query);
        Task<IList<Publication>?> GetPublicationByTermPagedAsync(int id, int currentPage, int size, string term);
        /// <summary>
        /// Obtains the ammount of Publications regarding a
        /// search query and the current page being browsed.
        /// Author: Elvis badilla y Diana Luna
        /// StoryID: ST-PH-3.5
        /// </summary>
        /// <param name="publicationId">ID of a Publication.</param>
        /// <param name="currentPage">The page currently being browsed.</param> 
        /// <param name="size">Number of publications per page.</param>
        /// <param name="query">The text to be used as search query.</param>
        /// <returns>Ammount of searched Publications.</returns>
        Task<IList<Publication>?> GetPublicationByTermPagedSummary(int id, int currentPage, int size, string term);
       
        /// <summary>
        /// Obtains the ammount of Publications regarding a search query.
        /// Author: Diana Luna & Elvis Badilla
        /// StoryID: ST-PH-3.5_ActividadSupervisada
        /// </summary>
        /// <param name="id">ID of a Publication.</param>
        /// <param name="term">The text to be used as search query.</param>
        /// <returns>Ammount of searched Publications, including the summary .</returns>
        Task<int> GetPublicationByTermCountAsyncSummary(int id, string term);

        /// <summary>
        /// Obtain a publication regarding a DOI string that identifies it.
        /// </summary>
        /// Author: David Sánchez López
        /// StoryID: ST-PH-3.1
        /// <param name="publicationId">The ID of the publication.</param>
        /// <returns>A publication object who's DOI matches the ID sent.</returns>
        Task<Publication?> GetPublicationById(string publicationId);


        /// <summary>
        /// Obtain a list from the publications corresponding to a list of ids.
        /// </summary>
        /// Author: Andrea Alvarado Acón 
        /// StoryID: ST-PA-3.13
        /// <param name="ids">List of ids</param>
        /// <returns>List of publications from ids.</returns>
        Task<IList<Publication>> GetPublicationsFromId(IList<string> ids);

  

        /// <summary>
        /// save Publication in database
        /// </summary>
        /// Elvis Badilla
        /// StoryID: ST-PH-4.1
        /// <param name="publication">publication </param>
        Task SaveAsync(Publication publication);
       

        /// <summary>
        /// save thesis asociation of Publication in database
        /// </summary>
        ///Elvis Badilla
        /// StoryID: ST-PH-4.4
        /// <param name="publicationPartOfTesis">publication Part Of Tesis </param>
        Task SavePublicationPartOfTesisAsync(PublicationPartOfTesis publicationPartOfTesis);

        ///<summary>
        /// Save an asociated investigation project to a publication in the database
        /// </summary>
        /// Author: Diana Luna 
        /// StoryID: ST-PH-4.5_v2
        /// <param name="projectAsociatedToPublication">Applies the project asociated to be saved</param>
        Task SaveProjectAsociatedAsync(ProjectAsociatedToPublication projectAsociatedToPublication);

        /// <summary>
        /// get list of tesis by asociated publication
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.4
        /// <param name="idPublication">id of publication </param>
        /// <returns>list of id of tesis by publication.</returns>
        Task<List<int>> GetPublicationPartOfTesisAsync(string idPublication);

        /// <summary>
        /// Get a list of all the projects asociated to a publication.
        /// </summary>
        /// Author: Diana Luna
        /// StoryID: ST-PH-4.5_v2
        /// <param name="publicationId"> Id of the publication </param>
        /// <return> The list of the id's from the asociated projects.</return>
        Task<List<int>> GetProjectsAsociatedAsync(string publicationId);

        /// <summary>
        ///get list of publication by list of authors
        /// </summary>
        ///Elvis Badilla
        /// StoryID: ST-PH-3.6
        /// <param name="authors">authores of publication </param>
        /// <returns>list of publication by authors.</returns>
        Task<IList<Publication>> GetPublicationByAuthor(IList<string> authors,int  groupId);

        /// <summary>
        /// Undate Publication in database
        /// </summary>
        /// Elvis Badilla
        /// StoryID: ST-PH-4.6
        /// <param name="publication">publication </param>
        Task UndatePublicationAsync(Publication publication);
        /// <summary>
        /// return true if publication is equal a other publication else if return false.
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.1
        /// <param name="publicationId"> publication </param>
        /// <return> bool by comparation .</return>
        Task<Publication> IdEqual(string id);


        /// <summary>
        /// Get a list of all the references associated to a publication.
        /// </summary>
        /// Author: Christian Rojas
        /// StoryID: ST-PH-3.8
        /// <param name="publicationId"> Id of the publication </param>
        /// <return> The list of the references asociated to a publication.</return>
        Task<IList<ReferenceListPublication>> GetReferencesById(string id);



        /// <summary>
        /// Delete an specific publication part of a thesis
        /// </summary>
        /// Author: Steven Nuñez
        /// StoryID: ST-HC-1.25
        /// <param name="id">Id of the publication</param>
        /// <returns> Task completed </returns>
        Task DeletePublicationPartOfThesis(string id);

        /// <summary>
        /// This method return an list of publications of an specific thesis
        /// </summary>
        /// <param name = "thesisId" > thesis id</param>
        /// Author: Steven Nuñez
        /// StoryID: ST-HC-1.25
        /// <returns>Return an list of publications of a specific thesis</returns>
        Task<IList<PublicationPartOfTesis>> GetAsyncPublicationsPartOfThesisFromId(int thesisId);

        /// <summary>
        /// Store an specific publication part of a thesis. 
        /// </summary>
        /// Author: Steven Nuñez 
        /// StoryID: ST-HC-1.17
        /// <param name="publicationPartOfTesis">An object PublicationPartOfThesis</param>
        /// <returns> Task completed </returns>
        Task SavePublicationPartOfThesisAsync(PublicationPartOfTesis publicationPartOfTesis);

        /// <summary>
        /// Get a list of all the publications asociated to a project.
        /// </summary>
        /// Author: Oscar Navarro
        /// StoryID: ST-HC-2.13
        /// <param name="projectId"> Id of the project </param>
        /// <return> The list of the id's from the asociated publications.</return>
        Task<List<string>> GetPublicationsAsociatedToProjectAsync(int projectId);

        /// <summary>
        /// Delete an specific publication part of a project
        /// </summary>
        /// Author: Oscar Navarro
        /// StoryID: ST-HC-2.13
        /// <param name="id">Id of the publication</param>
        /// <returns> Task completed </returns>
        Task DeletePublicationPartOfProject(string id);

        /// <summary>
        /// Get a list of all the publications asociated to a thesis.
        /// </summary>
        /// Author: Sebastian Gonzalez
        /// StoryID: ST-HC-2.3
        /// <param name="thesisId"> Id of the thesis </param>
        /// <return> The list of the id's from the asociated publications.</return>
        Task<List<string>> GetPublicationsAsociatedToThesisAsync(int thesisId);
        /// <summary>
        /// Delete a publication.
        /// </summary>
        /// Author: Christian Rojas
        /// StoryID: ST-PH-4.7
        /// <param name="publicationId"> Id of the publication </param>
        Task DeletePublication(string id);

        /// <summary>
        /// Retrieve a list with all research areas associated to a publication.
        /// </summary>
        /// Author: David Sánchez & Diana Luna
        /// StoryID: ST-PH-4.8
        /// <param name="id"> Id of the publication </param>
        /// <return> A list filled with research areas associated to the publication's ID.</return>
        Task<IList<ResearchAreaPublication>> GetAssociatedAreas(string id);

        /// <summary>
        /// Retrieve the association entity of type ResearchAreaPublication matching both IDs.
        /// </summary>
        /// Author: David Sánchez & Diana Luna
        /// StoryID: ST-PH-4.8
        /// <param name="publicationId"> Id of the publication </param>
        /// <param name="areaId"> Id of the area to associate </param>
        /// <return> ResearchAreaPublication object with requested IDs.</return>
        Task<ResearchAreaPublication> GetResearchAreaPublicationAssociation(string publicationId, int areaId);

        /// <summary>
        /// Add an association between a publication and a research area.
        /// </summary>
        /// Author: David Sánchez & Diana Luna
        /// StoryID: ST-PH-4.8
        /// <param name="association"> ResearchAreaPublication object to add to the table. </param>
        Task AddAssociatedArea(ResearchAreaPublication association);

        /// <summary>
        /// Disassociate an area from a publication using both entities's IDs.
        /// </summary>
        /// Author: David Sánchez & Diana Luna
        /// StoryID: ST-PH-4.8
        /// <param name="publicationId"> Id of the publication </param>
        /// <param name="areaId"> Id of the area to associate </param>
        Task DeleteAssociatedArea(string publicationId, int areaId);
        /// <summary>
        /// Finds if a publication already exists.
        /// </summary>
        /// Author: Diana Luna
        /// StoryID: ST-PH-4.9
        /// <param name="publication"> Publication to verifies if it exists </param>
        bool VerifyExistenceOfPublication(Publication publication);

        /// <summary>
        /// Retrieves a list of publications filtered by DOI, Name and Summary
        /// using the same search term for the tree filters, optimizing the search process.
        /// </summary>
        /// Author: David Sánchez López.
        /// StoryID: ST-PH-1.7.
        /// <param name="searchTerm">The term to filter by.</param>
        /// <returns>A resulting list of publications matching the search term.</returns>
        Task<IList<Publication>> GetPublicationByThreeFilters(string searchTerm, int groupId, int currentPage, int pageSize);

        /// <summary>
        /// Calculates the ammount of publications filtered by DOI, Name and Summary.
        /// </summary>
        /// Author: David Sánchez López.
        /// StoryID: ST-PH-1.7.
        /// <param name="searchTerm">The term to filter by.</param>
        /// <param name="groupId">The id of the group in which to search publications.</param>
        /// <returns>The ammount of publications found.</returns>
        Task<int> GetPublicationCountByThreeFilters(string searchTerm, int groupId, int currentPage, int pageSize);

        /// <summary>
        /// Add a reference.
        /// </summary>
        /// Author: Christian Rojas
        /// StoryID: ST-PH-4.20
        /// <param name="id"> Id of the publication </param>
        /// <param name="reference"> reference string </param>
        /// <param name="order"> reference order </param>
        Task addReferenceToPublication(string id, string reference, int order);
        
        /// <summary>
        /// Update a reference.
        /// </summary>
        /// Author: Christian Rojas
        /// StoryID: ST-PH-4.20
        /// <param name="id"> Id of the publication </param>
        /// <param name="reference"> new reference string </param>
        /// <param name="order"> reference order </param>
        Task updateReferenceToPublication(string id, string reference, int order);

        /// <summary>
        /// Check whether a publication exists by searching its DOI.
        /// </summary>
        /// Author: David Sánchez.
        ///<param doi="doi">DOI that identifies the publication.</param>
        /// StoryID: ST-PH-4.24
        /// <returns>Boolean value replying if publication exists.</returns>
        Task<bool> VerifyDOI(string doi);

    }

}
