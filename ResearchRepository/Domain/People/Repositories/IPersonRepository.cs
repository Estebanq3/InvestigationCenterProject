using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.Authentication.ValueObjects;
//using ResearchRepository.Domain.People.DTOs;

namespace ResearchRepository.Domain.People.Repositories
{
    public interface IPersonRepository  {
        /// <summary>
        /// Gets the current state of all the people.
        /// </summary>
        /// Author: Dylan Arias
        /// StoryID: ST-PA-3.5
        /// <returns>List of people</returns>
        Task<IList<Person>> GetAsyncPerson();
        /// <summary>
        /// Gets one person via email
        /// </summary>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-4.2
        /// <returns>Person</returns>
        Task<Person>? GetPersonByEmail(string email);
        /// <summary>
        /// Gets the current state of all the students.
        /// </summary>
        /// Author: Dylan Arias
        /// StoryID: ST-PA-3.5
        /// <returns>List of students</returns>
        /// 

        Task<IList<Student>> GetAsyncStudent();
        /// <summary>
        /// Gets the current state of all students based on their name and group they belong to.
        /// </summary>
        /// <param name = "name" > name of the collaborator</param>
        /// <param name = "groupId" > group id</param>
        /// Author: Dylan Arias
        /// StoryID: ST-PA-3.5
        /// <returns>List of students</returns>
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
        /// This method return an list of the groups that manage a collaborator
        /// </summary>
        /// <param name = "name" > name of the collaborator</param>
        /// <param name = "groupId" > group id</param>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-3.5
        /// <returns>Return a list with the groups that manage the collaborator</returns>
        Task<IList<InvestigatorManagesGroup>> GetAsyncInvestigatorManagesGroup(string name, int groupId);

        /// <summary>
        /// This method return an list of all the groups
        /// </summary>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-3.5
        /// <returns>Return a list with all the groups with the respective collaborator</returns>
        Task<IList<InvestigatorManagesGroup>> GetAsyncInvestigatorManagesGroup();

        /// <summary>
        /// This method return an list of investigators of a specific name and group id
        /// </summary>
        /// <param name = "name" > name of the collaborator</param>
        /// <param name = "groupId" > group id</param>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-3.5
        /// <returns>Return an list of investigators of a specific name and group id</returns>
        Task<IList<Investigator>> GetAsyncInvestigator(string name, int groupId);

        /// <summary>
        /// This method return an list of all the investigators
        /// </summary>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-3.3
        /// <returns>Return a list with all the investigators</returns>
        Task<IList<Investigator>> GetAsyncInvestigator();

        /// <summary>
        /// This method return an list of all the collaborators
        /// </summary>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-3.3
        /// <returns>Return a list with all the collaborators</returns>
        Task<IList<Collaborator>> GetAsyncCollaborator();

        /// <summary>
        /// This method return an list of collaborators of a specific group id
        /// </summary>
        /// <param name = "groupId" > group id</param>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-3.3
        /// <returns>Return an list of collaborators of a specific group id</returns>
        Task<IList<CollaboratorPartOfGroup>> GetAsyncCollaboratorPartOfGroupFromId(int groupId);

        /// <summary>
        /// This method return an list of investigators of a specific group id
        /// </summary>
        /// <param name = "groupId" > group id</param>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-3.3
        /// <returns>Return an list of investigators of a specific group id</returns>
        Task<IList<Investigator>> GetAsyncInvestigatorsFromGroup(int groupId);

        /// <summary>
        /// This method returns a list of investigators of a specific group id
        /// </summary>
        /// <param name = "groupId" > group id</param>
        /// Author: Rodrigo Contreras (Monkey Madness)
        /// StoryID: ST-MM-2.1
        /// <returns>Return an list of investigators of a specific group id</returns>
        Task<IList<Investigator>> GetAsyncMainInvestigatorsFromGroup(int groupId);

        /// <summary>
        /// Gets the current state of all students based on the group they belong to.
        /// </summary>
        /// <param name = "groupId" > group id</param>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-3.3
        /// <returns>List of students</returns>
        Task<IList<Student>> GetAsyncStudentsFromGroup(int groupId);
        /// <summary>
        /// Gets the current state of all InvestigatorManagesGroup entities based on group id.
        /// </summary>
        /// <param name = "groupId" > group id</param>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-3.3
        /// <returns>List of InvestigatorManagesGroup</returns>
        Task<IList<InvestigatorManagesGroup>> GetAsyncInvestigatorManagesGroupFromId(int groupId);
        /// <summary>
        /// This method return an list of authors of a specific thesis id
        /// </summary>
        /// <param name = "thesisId" > thesis id</param>
        /// Author: Oscar Navarro
        /// StoryID: ST-HC-1.3
        /// <returns>Return an list of authors of a specific tesis</returns>
        Task<IList<AuthorPartOfThesis>> GetAsyncAuthorsPartOfThesisFromId(int ThesisId);
        /// <summary>
        /// This method return an list of authors of a specific thesis id
        /// </summary>
        /// <param name = "thesisId" > thesis id</param>
        /// Author: Oscar Navarro
        /// StoryID: ST-HC-1.3
        /// <returns>Return an list of collaborators of a specific tesis</returns>
        Task<IList<AuthorPartOfThesis>> GetAuthorsOfThesisByEmail(IList<AuthorPartOfThesis> _authors);


        /// <summary>
        /// Obtain a list of authors from the database related to a publication id.
        /// </summary>
        /// Author: Christian Rojas Rios
        /// StoryID: ST-PH-3.4
        /// <param name="id">The ID of the publication.</param>
        /// <returns>List of all collaborators.</returns>
        Task<IList<CollaboratorIsAuthorOfPublication>> GetAuthorsById(string publicationId);

        /// <summary>
        /// Obtain a list of collaborators from the database according to their emails.
        /// </summary>
        /// Author: Christian Rojas Rios
        /// StoryID: ST-PH-3.4
        /// <param name="_authors">List of authors</param>
        /// <returns>List of all collaborators details.</returns>
        Task<IList<CollaboratorIsAuthorOfPublication>> GetAuthorByEmail(IList<CollaboratorIsAuthorOfPublication> _authors);



        /// <summary>
        /// This method return an list of collaborators of a specific project id
        /// </summary>
        /// <param name = "projectId" > project id</param>
        /// Author: Sebastian Gonzalez
        /// StoryID: ST-HC-2.6
        /// <returns>Return an list of collaborators of a specific project</returns>
        Task<IList<CollaboratorPartOfProject>> GetAsyncCollaboratorPartOfProjectFromId(int projectId);
        /// <summary>
        /// This method return an list of collaborators of a specific project id
        /// </summary>
        /// <param name = "projectId" > project id</param>
        /// Author: Sebastian Gonzalez
        /// StoryID: ST-HC-2.6
        /// <returns>Return an list of collaborators of a specific project</returns>
        Task<IList<CollaboratorPartOfProject>> GetCollaboratorsofProjectByEmail(IList<CollaboratorPartOfProject> _collaborators);

        /// <summary>
        /// Store an specific authorPartOfThesis. 
        /// </summary>
        /// Author: Sofia Campos y Oscar Navarro. 
        /// StoryID: ST-HC-1.8, ST-HC-1.3
        /// <param AuthorPartOfThesis="authorPartOfThesis">An object of authorPartOfThesis</param>
        /// <returns> Task completed </returns>
        Task SaveAsync(AuthorPartOfThesis authorPartOfThesis);

        Task DeleteAuthorOfThesisAsync(string email);

        /// <summary>
        /// Update thesis authors.
        /// </summary>
        /// Author: Gabriel Revillat Zeledon
        /// StoryID: ST-HC-1.9
        /// <param name="authorPartOfThesis"></param>
        /// <returns></returns>
        Task UpdateAsync(AuthorPartOfThesis authorPartOfThesis);


        /// <summary>
        /// Add colaborator of a project
        /// </summary>
        /// Author: Steven
        /// StoryID: ST-HC-1.12
        /// <param name="collaboratorPartOfProject">Email of collaborator</param>
        /// <returns>List of collaborator's publications' ids.</returns>
        Task SaveCollaboratorPartOfProjectAsync(CollaboratorPartOfProject collaboratorPartOfProject);

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
        /// save autor of publication  in database
        /// </summary>
        ///Elvis Badilla
        /// StoryID: ST-PH-4.1
        /// <param name="CollaboratorIsAuthorOfPublication">Collaborator Is Author Of Publication</param>
        Task SaveCollaboratorIsAuthorOfPublicationAsync(CollaboratorIsAuthorOfPublication collaboratorIsAuthorOfPublication);
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
        /// Obtain a list from the ids corresponding to a collaborator's Thesis
        /// </summary>
        /// Author: Carlos Mora Membreño
        /// StoryID: ST-PA-3.11-Thesis
        /// <param name="email">Email of collaborator</param>
        /// <returns>List of collaborator's Thesis' ids.</returns>
        Task<IList<int>> GetThesisIdFromAuthor(string email);

        /// <summary>
        /// Create a new person tuple in the database with the register data
        /// </summary>
        /// Author: Dylan Arias
        /// StoryID: ST-PA-4.15
        /// <param name="r">Object containing new person information</param>
        /// <returns></returns>
        Task registerUser(Register r);


       

        /// <summary>
        ///  author of thesis by email
        /// </summary>
        /// Author: Steven Nuñez
        /// StoryID: ST-PH-2.9
        /// <param name="email" >id of groun ,search term</param>
        /// <returns> author of publication by email of Collobarator</returns>
        Task<AuthorPartOfThesis> GetAuthorThesisByEmailEqualColaborator(string email);

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