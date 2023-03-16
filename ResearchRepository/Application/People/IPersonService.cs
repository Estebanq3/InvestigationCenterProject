using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.Authentication.ValueObjects;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.ResearchGroups.Entities;

//using ResearchRepository.Domain.People.DTOs;

namespace ResearchRepository.Application.People
{
    public interface IPersonService
    {
        /// <summary>
        /// Get the a list of all people)
        /// </summary>
        /// Author: Andrea Alvarado
        /// StoryID: ST-PA-3.6
        /// <returns>IList of all existing people in the database</returns>
        Task<IList<Person>> GetAsyncPerson();

        /// <summary>
        /// Search a specific person in the database based on their email. 
        /// </summary>
        /// Author: Greivin Sánchez 
        /// StoryID: ST-PA-3.2
        /// <returns>A entity of Person</returns>
        Task<Person>? GetPersonByEmail(string email);
        /// <summary>
        /// Get the a list of all Student
        /// </summary>
        /// Author: Andrea Alvarado
        /// StoryID: ST-PA-3.6
        /// <returns>IList of all students</returns>

        Task<IList<Student>> GetAsyncStudent();

        /// <summary>
        /// Get the a list of all students
        /// </summary>
        /// <param name="id">Identifier of the name written in the searchbar</param>
        /// <param group="id">Unique identifier of the group</param>
        /// Author: Sebastián Montero
        /// StoryID: ST-PA-3.3
        /// <returns>IList of all Academic Profiles</returns>
        Task<IList<Student>> GetAsyncStudent(string name, int groupId);

        /// <summary>
        /// Returns all collaborations from a collaborator
        /// </summary>
        /// <param name = "email" > email of the collaborator</param>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-1.6
        /// <returns>List of collaborations</returns>
        Task<IList<CollaboratorPartOfGroup>> GetCollaborationsFromEmail(string email);

        /// <summary>
        /// Get the a list of related core investigators according to a specific group
        /// </summary>
        /// <param name="id">Identifier of the name written in the searchbar</param>
        /// <param group="id">Unique identifier of the group</param>
        /// Author: Sebastián Montero
        /// StoryID: ST-PA-3.3
        /// <returns>IList of all Academic Profiles</returns>
        Task<IList<InvestigatorManagesGroup>> GetAsyncInvestigatorManagesGroup(string name, int groupId);

        /// <summary>
        /// Get the a list of all core investigators of diferent groups
        /// </summary>
        /// Author: Sebastián Montero
        /// StoryID: ST-PA-3.3
        /// <returns>IList of all Academic Profiles</returns>
        Task<IList<InvestigatorManagesGroup>> GetAsyncInvestigatorManagesGroup();


        /// <summary>
        /// Get the a list of related (no core) investigators for a specific group. 
        /// </summary>
        /// <param name="id">Identifier of the name written in the searchbar</param>
        /// <param group="id">Unique identifier of the group</param>
        /// Author: Sebastián Montero
        /// StoryID: ST-PA-3.3
        /// <returns>IList of all Academic Profiles</returns>
        Task<IList<Investigator>> GetAsyncInvestigator(string name, int groupId);

        /// <summary>
        /// Get the a list of all investigators of diferent groups
        /// </summary>
        /// Author: Sebastián Montero
        /// StoryID: ST-PA-3.3
        /// <returns>IList of all Academic Profiles</returns>
        Task<IList<Investigator>> GetAsyncInvestigator();

        /// <summary>
        /// Get the a list of all Collaborators of diferent groups
        /// </summary>
        /// <param name="id">Identifier of the name written in the searchbar</param>
        /// <param group="id">Unique identifier of the group</param>
        /// Author: Sebastián Montero
        /// StoryID: ST-PA-3.3
        /// <returns>IList of all Academic Profiles</returns>
        Task<IList<Collaborator>> GetAsyncCollaborator();

        /// <summary>
        /// Get the a list of all colaborators
        /// </summary>
        /// <param group="id">Unique identifier of the group</param>
        /// Author: Sebastián Montero
        /// StoryID: ST-PA-3.3
        /// <returns>IList of all Academic Profiles</returns>
        Task<IList<CollaboratorPartOfGroup>> GetAsyncCollaboratorPartOfGroupFromId(int groupId);

        /// <summary>
        /// Get the a list of investigators from a common group
        /// </summary>
        /// <param group="id">Unique identifier of the group</param>
        /// Author: Sebastián Montero
        /// StoryID: ST-PA-3.3
        /// <returns>IList of all Academic Profiles</returns>
        Task<IList<Investigator>> GetAsyncInvestigatorsFromGroup(int groupId);

        /// <summary>
        /// Get the list of three investigators from a common group
        /// </summary>
        /// <param group="id">Unique identifier of the group</param>
        /// Author: Sebastián Montero
        /// StoryID: ST-PA-3.3
        /// <returns>IList of all Academic Profiles</returns>
        Task<IList<Investigator>> GetAsyncMainInvestigatorsFromGroup(int groupId);

        /// <summary>
        /// Get the a list of students related to a specific group
        /// </summary>
        /// <param name="id">Identifier of the name written in the searchbar</param>
        /// <param group="id">Unique identifier of the group</param>
        /// Author: Sebastián Montero
        /// StoryID: ST-PA-3.3
        /// <returns>IList of all Academic Profiles</returns>
        Task<IList<Student>> GetAsyncStudentsFromGroup(int groupId);

        /// <summary>
        /// Get the a list of all core investigator based on the group ID
        /// </summary>
        /// <param group="id">Unique identifier of the group</param>
        /// Author: Sebastián Montero
        /// StoryID: ST-PA-3.3
        /// <returns>IList of all Academic Profiles</returns>
        Task<IList<InvestigatorManagesGroup>> GetAsyncInvestigatorManagesGroupFromId(int groupId);


        /// <summary>
        /// Get a list of collaborators related to a publication id
        /// </summary>
        /// <param publication="id">Unique identifier of the publication</param>
        /// Author: Christian Rojas
        /// StoryID: ST-PH-3.4
        /// <returns>IList of all collaborators related to a publication</returns>
        Task<IList<CollaboratorIsAuthorOfPublication>> GetAuthorsById(string id);

        /// <summary>
        /// Get a list of collaborators details.
        /// </summary>
        /// <param listAuthors="_authors">list of a collaborators authors of publication</param>
        /// Author: Christian Rojas
        /// StoryID: ST-PH-3.4
        /// <returns>IList of all collaborators details related to a publication</returns>
        Task<IList<CollaboratorIsAuthorOfPublication>> GetAuthorsDetailsByEmail(IList<CollaboratorIsAuthorOfPublication> _authors);
        /// <summary>
        /// Get the a list of all authors of a thesis
        /// </summary>
        /// <param Thesis="id">Unique identifier of the group</param>
        /// Author: Oscar Navarro
        /// StoryID: ST-HC-2.6
        /// <returns>IList of all Collaborators of a project</returns>

        Task<IList<AuthorPartOfThesis>> GetAsyncAuthorsPartOfThesisFromId(int ThesisId);
        /// <summary>
        /// Get the a list of all colaborators of a project
        /// </summary>
        /// <param listAuthors="_authors">Unique identifier of the group</param>
        /// Author: Oscar Navarro
        /// StoryID: ST-HC-2.6
        /// <returns>IList of all Academic Profiles</returns>
        Task<IList<AuthorPartOfThesis>> GetAuthorsOfThesisByEmail(IList<AuthorPartOfThesis> _authors);
        /// <summary>
        /// Get the a list of all colaborators of a project
        /// </summary>
        /// <param project="id">Unique identifier of the group</param>
        /// Author: Sebastián Gonzalez
        /// StoryID: ST-HC-2.6
        /// <returns>IList of all Collaborators of a project</returns>
        Task<IList<CollaboratorPartOfProject>> GetAsyncCollaboratorPartOfProjectFromId(int projectId);
        /// <summary>
        /// Get the a list of all colaborators of a project
        /// </summary>
        /// <param listCollaborators="_collaborators">Unique identifier of the group</param>
        /// Author: Sebastián Gonzalez
        /// StoryID: ST-HC-2.6
        /// <returns>IList of all Academic Profiles</returns>
        Task<IList<CollaboratorPartOfProject>> GetCollaboratorsofProjectByEmail(IList<CollaboratorPartOfProject> _collaborators);

        /// <summary>
        /// Store an specific thesis. 
        /// </summary>
        /// Author: Sofia Campos y Oscar Navarro. 
        /// StoryID: ST-HC-1.8, ST-HC-1.3
        /// <param name="authorPartOfThesis">An object of authorPartOfThesis</param>
        /// <returns> Task completed </returns>
        Task AddAuthorOfThesisAsync(AuthorPartOfThesis authorPartOfThesis);

        /// <summary>
        /// Store an specific thesis. 
        /// </summary>
        /// Author: Oscar Navarro. 
        /// StoryID: ST-HC-1.14
        /// <param name="email">An object of authorPartOfThesis</param>
        /// <returns> Task completed </returns>
        Task DeleteAuthorOfThesisAsync(string email);

        /// <summary>
        /// Store an specific project 
        /// </summary>
        /// Author: Steven Nuñez
        /// StoryID: ST-HC-1.12
        /// <param name="collaboratorPartOfProject">An object of authorPartOfThesis</param>
        /// <returns> Task completed </returns>
        Task AddCollaboratorPartOfProjectAsync(CollaboratorPartOfProject collaboratorPartOfProject);

        /// <summary>
        /// Update thesis authors.
        /// </summary>
        /// Author: Gabriel Revillat Zeledon.
        /// StoryID: ST-HC-1.9
        /// <param name="authorPartOfThesis">An object of authorPartOfThesis</param>
        /// <returns>Task completed.</returns>
        Task UpdateAuthorOfThesisAsync(AuthorPartOfThesis authorPartOfThesis);
        
        /// <summary>
        /// Obtain a list from the ids corresponding to a collaborator's publications
        /// </summary>
        /// Author: Andrea Alvarado Acón 
        /// StoryID: ST-PA-3.13
        /// <param name="email">Email of collaborator</param>
        /// <returns>List of collaborator's publications' ids.</returns>
        Task<IList<string>> GetPublicationsIdFromAuthor(string email);

        /// <summary>
        /// Obtain a list from the ids corresponding to a collaborator's publications
        /// </summary>
        /// Author: Carlos Mora Membreño
        /// StoryID: ST-PA-3.8
        /// <param name="email">Email of collaborator</param>
        /// <returns>List of collaborator's proyects' ids.</returns>
        Task<IList<int>> GetProjectsIdFromAuthor(string email);

        /// <summary>
        /// Obtain a list of collaborators from a group and a specified name
        /// </summary>
        /// Author: Sebastian Montero Castro
        /// StoryID: ST-PA-1.9
        /// <param name="searched">Email to be searched</param>
        /// <param name="groupId">The id of the group</param>
        /// <returns>List of collaborators that matc hthe name</returns>
        Task<IList<CollaboratorPartOfGroup>> GetCollaboratorsInGroupByName(string searched, int groupId);

        /// <summary>
        /// Deletes a collaborator part of group
        /// </summary>
        /// Author: Sebastian Montero Castro
        /// StoryID: ST-PA-1.9
        /// <param name="email">Email of the collaborator to be deleted</param>
        /// <param name="groupId">The id of the group</param>
        /// <returns> the completed task </returns>
        Task DeleteCollaboratorPartOfGroup(string email, int groupId);

        /// <summary>
        /// Deletes a collaborator of a project.
        /// </summary>
        /// Author: Gabriel Revillat Zeledón.
        /// StoryID: ST-HC-1.15
        /// <param name="email">Email of the collaborator to be deleted.</param>
        /// <returns>The completed task.</returns>
        Task DeleteCollaboratorOfProjectAsync(string email);

        /// <summary>
        /// Search for a specific CollaboratorPartOFGroup
        /// </summary>
        /// Author: Sebastián Montero
        /// StoryID: ST-PA-3.2
        /// <returns>A entity of ColllaboratorPartOfGroup</returns>
        Task<CollaboratorPartOfGroup> GetCollaboratorPartOfGroupFromEmail(string email, int groupId);
        Task<string> GetProfilePic(string email);
        /// <summary>
        /// add autor of publication  in database
        /// </summary>
        ///Elvis Badilla
        /// StoryID: ST-PH-4.1
        /// <param name="CollaboratorIsAuthorOfPublication">Collaborator Is Author Of Publication</param>
        Task AddCollaboratorIsAuthorOfPublication(CollaboratorIsAuthorOfPublication collaboratorIsAuthorOfPublication);
        /// <summary>
        /// get list of collaborator by term
        /// </summary>
        ///Elvis Badilla
        /// StoryID: ST-PH-3.6
        /// <param int id string term >id of groun ,search term</param>
        ///<returns>IList of all collaborator by term</returns>
        Task<IList<Collaborator>> GetColaboratorByTermCountAsync(int id, string term);
        /// <summary>
        ///  author of publication by email
        /// </summary>
        ///Elvis Badilla
        /// StoryID: ST-PH-3.6
        /// <param string email >id of groun ,search term</param>
        /// <returns> author of publication by email of Collobarator</returns>
        Task<CollaboratorIsAuthorOfPublication> GetAuthorByEmailEqualColaborator(string email);


        /// <summary>
        /// Obtain a list from the ids corresponding to a collaborator's thesis
        /// </summary>
        /// Author: Carlos Mora Membreño
        /// StoryID: ST-PA-3.11-Thesis
        /// <param name="email">Email of collaborator</param>
        /// <returns>List of collaborator's thesis' ids.</returns>
        Task<IList<int>> GetThesisIdFromAuthor(string email);


        /// <summary>
        ///  author of thesis by email
        /// </summary>
        ///Author: Steven Nuñez
        /// StoryID: ST-PH-2.9
        /// <param name="email">Email of collaborator</param>
        /// <returns> authors of thesis by email of Collobarator</returns>
        Task<AuthorPartOfThesis> GetAuthorThesisByEmailEqualColaborator(string email);
        /// <summary>
        /// Create a new person tuple in the database with the register data
        /// </summary>
        /// Author: Dylan Arias
        /// StoryID: ST-PA-4.15
        /// <param name="r">Object containing new person information</param>
        /// <returns></returns>
        Task registerUser(Register r);


        /// <summary>
        /// Gets all the collaborator entities that are not a part of the specified group.
        /// </summary>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-1.10
        /// <returns>List of people that are not a part of the group</returns>
        Task<IList<Collaborator>> GetCollaboratorsNotInGroup(int groupId);

        /// <summary>
        /// Adds a new InvestigatorManagesGroup to a specified group
        /// </summary>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-1.10
        Task AddInvestigatorManagesGroup(Collaborator collaborator, ResearchGroup group);

        /// <summary>
        /// Adds a new Investigator to a specified group
        /// </summary>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-1.10
        Task AddInvestigatorToGroup(Collaborator collaborator, ResearchGroup group);

        /// <summary>
        /// Adds a new Student to a specified group
        /// </summary>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-1.10
        Task AddStudentToGroup(Collaborator collaborator, ResearchGroup group);
        /// <summary>
        /// author of a publication.
        /// </summary>
        /// Author: Elvis Badilla Mena
        /// StoryID: ST-PH-4.13
        /// <param name="id">id of the publication to be deleted.</param>
        Task DeleteAuthorOfPublicationAsync(string id);
    }

}
