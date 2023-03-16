using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.People.Repositories;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.Authentication.ValueObjects;
using ResearchRepository.Domain.ResearchGroups.Entities;

//using ResearchRepository.Domain.People.DTOs;

namespace ResearchRepository.Application.People.Implementations
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<IList<Person>> GetAsyncPerson() //GetAsyncPersona
        {
            return await _personRepository.GetAsyncPerson();
        }
        public async Task<IList<Student>> GetAsyncStudent()
        {
            return await _personRepository.GetAsyncStudent();
        }
        public async Task<IList<Student>> GetAsyncStudent(string name, int groupId)
        {
            return await _personRepository.GetAsyncStudent(name, groupId);
        }
        public async Task<IList<CollaboratorPartOfGroup>> GetCollaborationsFromEmail(string email) {
            return await _personRepository.GetCollaborationsFromEmail(email);
        }
        public async Task<IList<InvestigatorManagesGroup>> GetAsyncInvestigatorManagesGroup(string name, int groupId)
        {
            return await _personRepository.GetAsyncInvestigatorManagesGroup(name, groupId);
        }
        public async Task<IList<InvestigatorManagesGroup>> GetAsyncInvestigatorManagesGroup()
        {
            return await _personRepository.GetAsyncInvestigatorManagesGroup();
        }
        public async Task<IList<Investigator>> GetAsyncInvestigator(string name, int groupId)
        {
            return await _personRepository.GetAsyncInvestigator(name, groupId);
        }
        public async Task<IList<Investigator>> GetAsyncInvestigator()
        {
            return await _personRepository.GetAsyncInvestigator();
        }
        public async Task<IList<Collaborator>> GetAsyncCollaborator()
        {
            return await _personRepository.GetAsyncCollaborator();
        }
        public async Task<IList<CollaboratorPartOfGroup>> GetAsyncCollaboratorPartOfGroupFromId(int groupId)
        {
            return await _personRepository.GetAsyncCollaboratorPartOfGroupFromId(groupId);
        }
        public async Task<IList<Investigator>> GetAsyncInvestigatorsFromGroup(int groupId)
        {
            return await _personRepository.GetAsyncInvestigatorsFromGroup(groupId);
        }
        public async Task<IList<Investigator>> GetAsyncMainInvestigatorsFromGroup(int groupId)
        {
            return await _personRepository.GetAsyncMainInvestigatorsFromGroup(groupId);
        }
        public async Task<IList<Student>> GetAsyncStudentsFromGroup(int groupId)
        {
            return await _personRepository.GetAsyncStudentsFromGroup(groupId);
        }
        public async Task<IList<InvestigatorManagesGroup>> GetAsyncInvestigatorManagesGroupFromId(int groupId)
        {
            return await _personRepository.GetAsyncInvestigatorManagesGroupFromId(groupId);
        }

        public async Task<IList<CollaboratorIsAuthorOfPublication>> GetAuthorsById(string id)
        {
            return await _personRepository.GetAuthorsById(id);
        }

        public async Task<IList<CollaboratorIsAuthorOfPublication>> GetAuthorsDetailsByEmail(IList<CollaboratorIsAuthorOfPublication> _authors)
        {
            return await _personRepository.GetAuthorByEmail(_authors);
        }
        public async Task<IList<string>> GetPublicationsIdFromAuthor(string email)
        {
            return await _personRepository.GetPublicationsIdFromAuthor(email);
        }
        public async Task<Person>? GetPersonByEmail(string email)
        {
            return await _personRepository.GetPersonByEmail(email);
        }

        public async Task<IList<AuthorPartOfThesis>> GetAsyncAuthorsPartOfThesisFromId(int ThesisId)
        {
            return await _personRepository.GetAsyncAuthorsPartOfThesisFromId(ThesisId);
        }

        public async Task<IList<AuthorPartOfThesis>> GetAuthorsOfThesisByEmail(IList<AuthorPartOfThesis> _authors)
        {
            return await _personRepository.GetAuthorsOfThesisByEmail(_authors);
        }

        public async Task<IList<CollaboratorPartOfProject>> GetAsyncCollaboratorPartOfProjectFromId(int projectId)
        {
            return await _personRepository.GetAsyncCollaboratorPartOfProjectFromId(projectId);
        }
        public async Task<IList<CollaboratorPartOfProject>> GetCollaboratorsofProjectByEmail(IList<CollaboratorPartOfProject> _collaborators)
        {
            return await _personRepository.GetCollaboratorsofProjectByEmail(_collaborators);
        }

        public async Task<IList<int>> GetProjectsIdFromAuthor(string email)
        {
            return await _personRepository.GetProjectsIdFromAuthor(email);
        }
        public async Task AddAuthorOfThesisAsync(AuthorPartOfThesis authorPartOfThesis)
        {
            await _personRepository.SaveAsync(authorPartOfThesis);
        }
        public async Task DeleteAuthorOfThesisAsync(string email)
        {
            await _personRepository.DeleteAuthorOfThesisAsync(email);
        }

        public async Task AddCollaboratorIsAuthorOfPublication(CollaboratorIsAuthorOfPublication collaboratorIsAuthorOfPublication)
        {
            await _personRepository.SaveCollaboratorIsAuthorOfPublicationAsync(collaboratorIsAuthorOfPublication);
        }

        public async Task<CollaboratorIsAuthorOfPublication> GetAuthorByEmailEqualColaborator(string email)
        {
            return await _personRepository.GetAuthorByEmailEqualColaborator(email);
        }

        public async Task AddCollaboratorPartOfProjectAsync(CollaboratorPartOfProject collaboratorPartOfProject)
        {
            await _personRepository.SaveCollaboratorPartOfProjectAsync(collaboratorPartOfProject);
        }

        public async Task UpdateAuthorOfThesisAsync(AuthorPartOfThesis authorPartOfThesis)
        {
            await _personRepository.UpdateAsync(authorPartOfThesis);
        }

        public async Task<string> GetProfilePic(string email)
        {
            return await _personRepository.GetProfilePic(email);
        }
        public async Task<IList<int>> GetThesisIdFromAuthor(string email)
        {
            return await _personRepository.GetThesisIdFromAuthor(email);
        }

        public async Task<IList<CollaboratorPartOfGroup>> GetCollaboratorsInGroupByName(string searched, int groupId)
        {
            return await _personRepository.GetCollaboratorsInGroupByName(searched, groupId);
        }

        public async Task<IList<Collaborator>> GetColaboratorByTermCountAsync(int id, string term)
        {
            return await _personRepository.GetColaboratorByTermCountAsync(id, term);
        }


        public async Task DeleteCollaboratorPartOfGroup(string email, int groupId)
        {
            await _personRepository.DeleteCollaboratorPartOfGroup(email, groupId);
        }

        public async Task DeleteCollaboratorOfProjectAsync(string email)
        {
            await _personRepository.DeleteCollaboratorOfProjectAsync(email);
        }

        public async Task<CollaboratorPartOfGroup> GetCollaboratorPartOfGroupFromEmail(string email, int groupId)
        {
            return await _personRepository.GetCollaboratorPartOfGroupFromEmail(email, groupId);
        }


        public async Task<IList<Collaborator>> GetCollaboratorsNotInGroup(int groupId) {
            return await _personRepository.GetCollaboratorsNotInGroup(groupId);
        }
        public async Task AddInvestigatorManagesGroup(Collaborator collaborator, ResearchGroup group) {
            await _personRepository.AddInvestigatorManagesGroup(collaborator, group);
        }
        public async Task AddInvestigatorToGroup(Collaborator collaborator, ResearchGroup group) {
            await _personRepository.AddInvestigatorToGroup(collaborator, group);
        }
        public async Task AddStudentToGroup(Collaborator collaborator, ResearchGroup group) {
            await _personRepository.AddStudentToGroup(collaborator, group);
        }
        public async Task<AuthorPartOfThesis> GetAuthorThesisByEmailEqualColaborator(string email)
        {
            return await _personRepository.GetAuthorThesisByEmailEqualColaborator(email);
        }



        public async Task registerUser(Register r)
        {
            await _personRepository.registerUser(r);
        }

        public async Task DeleteAuthorOfPublicationAsync(string id)
        {
            await _personRepository.DeleteAuthorOfPublicationAsync(id);
        }
        
    }
}
