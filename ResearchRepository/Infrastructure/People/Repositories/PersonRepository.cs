using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.People.Entities;
//using ResearchRepository.Domain.People.DTOs;
using ResearchRepository.Domain.People.Repositories;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Infrastructure.People;
using ResearchRepository.Domain.Authentication.ValueObjects;


namespace ResearchRepository.Infrastructure.People.Repositories
{
    internal class PersonRepository : IPersonRepository
    {
        private readonly PeopleDbContext _dbContext;
        public IUnitOfWork UnitOfWork => _dbContext;

        public PersonRepository(PeopleDbContext unitOfWork)
        {
            _dbContext = unitOfWork;
        }

        public async Task<IList<Person>> GetAsyncPerson() //GetAsyncPersona
        {
            return await _dbContext.Person.ToListAsync();
        }

        public async Task<IList<Student>> GetAsyncStudent()
        {
            return await _dbContext.Student.ToListAsync();
        }

        public async Task<IList<CollaboratorPartOfGroup>> GetCollaborationsFromEmail(string email) {
            var collaborations = new List<CollaboratorPartOfGroup>();
            var partOfGroups = await _dbContext.CollaboratorPartOfGroup.ToListAsync();
            var collab = await _dbContext.Collaborator.FirstOrDefaultAsync(e => e.Email == email);
            if (collab != null) {
                foreach (var c in collab.CollaboratorPartOfGroups) {
                    collaborations.Add(c);
                }
            }
            return collaborations;
        }
        public async Task<IList<Student>> GetAsyncStudent(string name, int groupId)
        {
            IList<Student> studentsFromGroup = await GetAsyncStudentsFromGroup(groupId);
            return studentsFromGroup.Where(e => (e.FirstName.ToLower() + " " + e.FirstLastName.ToLower() + " " + e.SecondLastName.ToLower()).Contains(name.ToLower())).ToList();
        }

        public async Task<IList<InvestigatorManagesGroup>> GetAsyncInvestigatorManagesGroup(string name, int groupId)
        {
            return await _dbContext.InvestigatorManagesGroup.Where(e => (e.Investigator.FirstName.ToLower() + " " + e.Investigator.FirstLastName.ToLower() + " " + e.Investigator.SecondLastName.ToLower()).Contains(name.ToLower()) && e.GroupId == groupId).ToListAsync();
        }

        public async Task<IList<InvestigatorManagesGroup>> GetAsyncInvestigatorManagesGroup()
        {
            return await _dbContext.InvestigatorManagesGroup.ToListAsync();
        }

        public async Task<IList<Investigator>> GetAsyncInvestigator(string name, int groupId)
        {
            IList<Investigator> investigatorsFromGroup = await GetAsyncInvestigatorsFromGroup(groupId);
            return investigatorsFromGroup.Where(e => (e.FirstName.ToLower() + " " + e.FirstLastName.ToLower() + " " + e.SecondLastName.ToLower()).Contains(name.ToLower())).ToList();
        }

        public async Task<IList<Investigator>> GetAsyncInvestigator()
        {
            return await _dbContext.Investigator.ToListAsync();
        }

        public async Task<IList<Collaborator>> GetAsyncCollaborator()
        {
            return await _dbContext.Collaborator.ToListAsync();
        }

        public async Task<IList<CollaboratorPartOfGroup>> GetAsyncCollaboratorPartOfGroupFromId(int groupId)
        {
            return await _dbContext.CollaboratorPartOfGroup.Where(e => e.InvestigationGroupId == groupId).ToListAsync();
        }

        public async Task<IList<Investigator>> GetAsyncInvestigatorsFromGroup(int groupId)
        {
            IList<CollaboratorPartOfGroup> collaboratorsFromGroup = await GetAsyncCollaboratorPartOfGroupFromId(groupId);
            IList<Investigator> investigators = await GetAsyncInvestigator();
            var investigatorsFromGroup = from colab in collaboratorsFromGroup
                                         join inv in investigators on colab.CollaboratorEmail equals inv.Email
                                         select inv;

            return investigatorsFromGroup.ToList();
        }

        public async Task<IList<Investigator>> GetAsyncMainInvestigatorsFromGroup(int groupId)
        {
            IList<Investigator> investigators = await GetAsyncInvestigatorsFromGroup(groupId);
            return investigators.Take(3).ToList();

        }

        public async Task<IList<Student>> GetAsyncStudentsFromGroup(int groupId)
        {
            IList<CollaboratorPartOfGroup> collaboratorsFromGroup = await GetAsyncCollaboratorPartOfGroupFromId(groupId);
            IList<Student> students = await GetAsyncStudent();
            var studentsFromGroup = from colab in collaboratorsFromGroup
                                    join student in students on colab.CollaboratorEmail equals student.Email
                                    select student;

            return studentsFromGroup.ToList();
        }
        public async Task<IList<InvestigatorManagesGroup>> GetAsyncInvestigatorManagesGroupFromId(int groupId)
        {
            return await _dbContext.InvestigatorManagesGroup.Where(e => e.GroupId == groupId).ToListAsync();
        }

        public async Task<IList<AuthorPartOfThesis>> GetAsyncAuthorsPartOfThesisFromId(int ThesisId)
        {
            return await _dbContext.AuthorPartOfThesis.Where(e => e.ThesisId == ThesisId).ToListAsync();
        }

        public async Task<IList<AuthorPartOfThesis>> GetAuthorsOfThesisByEmail(IList<AuthorPartOfThesis> _authors)
        {
            Collaborator currentCollaborator;
            foreach (var a in _authors)
            {
                currentCollaborator = (await _dbContext.Collaborator
                    .Where(p => p.Email == a.CollaboratorEmail)
                    .ToListAsync()).First();

                a.Collaborator = currentCollaborator;
            }
            return _authors;

        }

        /// <summary>
        /// Obtain a list of authors from the database related to a publication id.
        /// </summary>
        /// Author: Christian Rojas Rios
        /// StoryID: ST-PH-3.4
        /// <param name="id">The ID of the publication.</param>
        /// <returns>List of all collaborators.</returns>
        public async Task<IList<CollaboratorIsAuthorOfPublication>> GetAuthorsById(string id)
        {
            return await _dbContext.AuthorOfPublications.Where(p => p.IdPublication == id).ToListAsync();
        }


        /// <summary>
        /// Obtain a list of collaborators from the database according to their emails.
        /// </summary>
        /// Author: Christian Rojas Rios
        /// StoryID: ST-PH-3.4
        /// <param name="_authors">List of authors</param>
        /// <returns>List of all collaborators details.</returns>
        public async Task<IList<CollaboratorIsAuthorOfPublication>> GetAuthorByEmail(IList<CollaboratorIsAuthorOfPublication> _authors)
        {

            Collaborator currentCollaborator;
            foreach (var a in _authors)
            {
                currentCollaborator = (await _dbContext.Collaborator
                    .Where(p => p.Email == a.EmailCollaborator)
                    .ToListAsync()).First();

                a.Collaborator = currentCollaborator;
            }
            return _authors;

        }
        public async Task<IList<CollaboratorPartOfProject>> GetAsyncCollaboratorPartOfProjectFromId(int projectId)
        {
            return await _dbContext.CollaboratorPartOfProject.Where(e => e.InvestigationProjectId == projectId).ToListAsync();
        }
        public async Task<IList<CollaboratorPartOfProject>> GetCollaboratorsofProjectByEmail(IList<CollaboratorPartOfProject> _collaborators)
        {
            Collaborator currentCollaborator;
            foreach (var a in _collaborators)
            {
                currentCollaborator = (await _dbContext.Collaborator
                    .Where(p => p.Email == a.CollaboratorEmail)
                    .ToListAsync()).First();

                a.Collaborator = currentCollaborator;
            }
            return _collaborators;

        }

        public async Task SaveAsync(AuthorPartOfThesis authorPartOfThesis)
        {
            _dbContext.AuthorPartOfThesis.Add(authorPartOfThesis);
            await _dbContext.SaveEntitiesAsync();
        }

        public async Task DeleteAuthorOfThesisAsync(string email)
        {
            var _author = (from a in _dbContext.AuthorPartOfThesis
                              where a.CollaboratorEmail == email
                              select a);
            _dbContext.AuthorPartOfThesis.RemoveRange(_author);
            _dbContext.SaveChanges();
            await _dbContext.SaveEntitiesAsync();
        }

        public async Task SaveCollaboratorPartOfProjectAsync(CollaboratorPartOfProject collaboratorPartOfProject)
        {
            _dbContext.CollaboratorPartOfProject.Add(collaboratorPartOfProject);
            await _dbContext.SaveEntitiesAsync();
        }


        public async Task UpdateAsync(AuthorPartOfThesis authorPartOfThesis)
        {
            _dbContext.AuthorPartOfThesis.Update(authorPartOfThesis);
            await _dbContext.SaveEntitiesAsync();
        }

        private async Task<IList<CollaboratorIsAuthorOfPublication>> GetAsyncPublicationsIdFromAuthor(string email)
        {
            return await _dbContext.AuthorOfPublications.Where(p => p.EmailCollaborator == email).ToListAsync();
        }
        public async Task<IList<string>> GetPublicationsIdFromAuthor(string email)
        {
            IList<CollaboratorIsAuthorOfPublication> collaboratorsOfPublication = await GetAsyncPublicationsIdFromAuthor(email);
            var publications = from colabP in collaboratorsOfPublication
                               select colabP.IdPublication;
            return publications.ToList();
        }


        public async Task<Person>? GetPersonByEmail(string email)
        {
            IList<Person> personResult = await _dbContext.Person.Where(e => e.Email == email).ToListAsync();
            Person person = null;
            if (personResult.Length() > 0)
            {
                person = personResult.First();
            }
            return person;
        }
        public async Task<string> GetProfilePic(string email)
        {
            string profilePic = "default.png";
            AcademicProfile? profile = await _dbContext.AcademicProfile.FirstOrDefaultAsync(e => e.Email == email);
            if (profile != null)
            {
                profilePic = profile.ProfilePic;
            }
            return profilePic;
        }
        private async Task<IList<CollaboratorPartOfProject>> GetAsyncProjectsIdFromAuthor(string email)
        {
            return await _dbContext.CollaboratorPartOfProject.Where(p => p.CollaboratorEmail == email).ToListAsync();
        }
        public async Task<IList<int>> GetProjectsIdFromAuthor(string email)
        {
            IList<CollaboratorPartOfProject> collaboratorsOfProject = await GetAsyncProjectsIdFromAuthor(email);
            var projects = from colabP in collaboratorsOfProject
                           select colabP.InvestigationProjectId;
            return projects.ToList();
        }
        private async Task<IList<AuthorPartOfThesis>> GetAsyncThesisIdFromAuthor(string email)
        {
            return await _dbContext.AuthorPartOfThesis.Where(p => p.CollaboratorEmail == email).ToListAsync();
        }
        public async Task<IList<int>> GetThesisIdFromAuthor(string email)
        {
            IList<AuthorPartOfThesis> authorPartOfThesis = await GetAsyncThesisIdFromAuthor(email);
            var theses = from colabP in authorPartOfThesis
                         select colabP.ThesisId;
            return theses.ToList();
        }

        public async Task SaveCollaboratorIsAuthorOfPublicationAsync(CollaboratorIsAuthorOfPublication collaboratorIsAuthorOfPublication)
        {
            var CollaboratorIsAuthorOfPublicationInset = await _dbContext.AuthorOfPublications.FirstOrDefaultAsync(n => n.IdPublication == collaboratorIsAuthorOfPublication.IdPublication && n.EmailCollaborator == collaboratorIsAuthorOfPublication.EmailCollaborator);
            if (CollaboratorIsAuthorOfPublicationInset != null)
            {
                _dbContext.Entry(CollaboratorIsAuthorOfPublicationInset).CurrentValues.SetValues(collaboratorIsAuthorOfPublication);
            }
            else
            {
                _dbContext.Attach(collaboratorIsAuthorOfPublication).State = EntityState.Added;
                _dbContext.AuthorOfPublications.Add(collaboratorIsAuthorOfPublication);
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IList<Collaborator>> GetColaboratorByTermCountAsync(int id, string term)
        {

            return await _dbContext.Collaborator.Where(e => (e.FirstName + " " + e.FirstLastName + " " + e.SecondLastName).Contains(term)).ToListAsync();
        }
        public async Task<CollaboratorIsAuthorOfPublication> GetAuthorByEmailEqualColaborator(string email)
        {
            return (await _dbContext.AuthorOfPublications
            .Where(p => p.EmailCollaborator == email)
            .ToListAsync()).First();
        }

        public async Task<IList<CollaboratorPartOfGroup>> GetCollaboratorsInGroupByName(string searched, int groupId)
        {
            IList<CollaboratorPartOfGroup> allGroupMembers = await GetAsyncCollaboratorPartOfGroupFromId(groupId);
            IList<CollaboratorPartOfGroup> foundResults = new List<CollaboratorPartOfGroup>();
            foreach (var r in allGroupMembers)
            {
                if ((r.Collaborator.FirstName.ToLower() + " " + r.Collaborator.FirstLastName.ToLower() + " " + r.Collaborator.SecondLastName.ToLower()).Contains(searched.ToLower()))
                {
                    foundResults.Add(r);
                }
            }
            return foundResults;
        }

        public async Task DeleteCollaboratorPartOfGroup(string email, int groupId)
        {
            var collaborator = await GetCollaboratorPartOfGroupFromEmail(email, groupId);
            var leadInvestigator = await GetInvestigatorManagesFromEmail(email, groupId);
            if (collaborator != null)
            {
                var collab = collaborator.Collaborator;
                collab.CollaboratorPartOfGroups.Remove(collaborator);
                _dbContext.CollaboratorPartOfGroup.Remove(collaborator);
            }
            if (leadInvestigator != null)
            {
                var investig = leadInvestigator.Investigator;
                investig.InvestigatorManagesGroups.Remove(leadInvestigator);
                _dbContext.InvestigatorManagesGroup.Remove(leadInvestigator);
            }
            _dbContext.SaveChanges();
        }

        public async Task DeleteCollaboratorOfProjectAsync(string email)
        {
            var _collaborator = (from collaborator in _dbContext.CollaboratorPartOfProject
                                 where collaborator.CollaboratorEmail == email
                                 select collaborator);

            _dbContext.CollaboratorPartOfProject.RemoveRange(_collaborator);
            _dbContext.SaveChanges();
            await _dbContext.SaveEntitiesAsync();
        }

        public async Task<CollaboratorPartOfGroup> GetCollaboratorPartOfGroupFromEmail(string email, int groupId)
        {
            return await _dbContext.CollaboratorPartOfGroup.FirstOrDefaultAsync(e => e.CollaboratorEmail == email && e.InvestigationGroupId == groupId);
        }
        public async Task<AuthorPartOfThesis> GetAuthorThesisByEmailEqualColaborator(string email)
        {
            return (await _dbContext.AuthorPartOfThesis
            .Where(p => p.CollaboratorEmail == email)
               .ToListAsync()).First();
        }


        public async Task registerUser(Register r)
        {
            /*Person p = new Person();
            p.Email = r.Email;
            p.FirstName = r.FirstName;
            p.FirstLastName = r.FirstLastName;
            p.SecondLastName = r.SecondLastName;
            p.Country = r.Country;
            await _dbContext.Person.AddAsync(p);
            await _dbContext.SaveChangesAsync();*/
            await _dbContext.Database.ExecuteSqlInterpolatedAsync($"execute new_person @firstName = {r.FirstName}, @firstLastName = {r.FirstLastName}, @secondLastName = {r.SecondLastName}, @email = {r.Email}, @country = {r.Country}");
        }

        public async Task<InvestigatorManagesGroup> GetInvestigatorManagesFromEmail(string email, int groupId)
        {
            return await _dbContext.InvestigatorManagesGroup.FirstOrDefaultAsync(e => e.Email == email && e.GroupId == groupId);
        }

        public async Task<IList<Collaborator>> GetCollaboratorsNotInGroup(int groupId)
        {
            IList<CollaboratorPartOfGroup> partOfGroup = await GetAsyncCollaboratorPartOfGroupFromId(groupId);
            IList<Collaborator> collaboratorsInGroup = new List<Collaborator>();
            foreach (var u in partOfGroup)
            {
                collaboratorsInGroup.Add(u.Collaborator);
            }
            IList<Collaborator> collaboratorsNotInGroup = (await GetAsyncCollaborator()).Except(collaboratorsInGroup).ToList();
            return collaboratorsNotInGroup;
        }
        public async Task AddInvestigatorManagesGroup(Collaborator collaborator, ResearchGroup group){
            IList<InvestigatorManagesGroup> managers = await GetAsyncInvestigatorManagesGroup();
            Investigator investigator = await _dbContext.Investigator.FirstOrDefaultAsync(e => e.Email == collaborator.Email);
            if (investigator == null) {
                investigator = (Investigator)collaborator;
                _dbContext.Investigator.Add(investigator);
            }
            if (investigator.InvestigatorManagesGroups == null) {
                investigator.InvestigatorManagesGroups = new List<InvestigatorManagesGroup>();
            }

            if (investigator.CollaboratorPartOfGroups == null)
            {
                investigator.CollaboratorPartOfGroups = new List<CollaboratorPartOfGroup>();
            }

            CollaboratorPartOfGroup newCollab = new CollaboratorPartOfGroup()
            {
                Collaborator = investigator,
                CollaboratorEmail = investigator.Email,
                InvestigationGroupId = group.Id,
                ResearchGroup = group
            };

            InvestigatorManagesGroup newCoreMember = new InvestigatorManagesGroup(){
                Email = investigator.Email,
                Investigator = investigator,
                GroupId = group.Id,
                ResearchGroup = group
            };

            investigator.InvestigatorManagesGroups.Add(newCoreMember);
            investigator.CollaboratorPartOfGroups.Add(newCollab);


            _dbContext.Entry(newCollab);
            _dbContext.Entry(newCollab.ResearchGroup);
            _dbContext.Entry(newCollab.Collaborator);
            _dbContext.CollaboratorPartOfGroup.Add(newCollab);
            _dbContext.Entry(newCoreMember);
            _dbContext.Entry(newCoreMember.ResearchGroup);
            _dbContext.Entry(newCoreMember.Investigator);
            _dbContext.InvestigatorManagesGroup.Add(newCoreMember);
            _dbContext.SaveChanges();
            await _dbContext.SaveEntitiesAsync();
        }
        public async Task AddInvestigatorToGroup(Collaborator collaborator, ResearchGroup group){
            IList<CollaboratorPartOfGroup> partOfGroups = await _dbContext.CollaboratorPartOfGroup.ToListAsync();
            Investigator investigator = await _dbContext.Investigator.FirstOrDefaultAsync(e => e.Email == collaborator.Email);
            if (investigator == null) {
                investigator = (Investigator)collaborator;
                _dbContext.Investigator.Add(investigator);
            }

            if (investigator.CollaboratorPartOfGroups == null) {
                investigator.CollaboratorPartOfGroups = new List<CollaboratorPartOfGroup>();
            }

            CollaboratorPartOfGroup newCollab = new CollaboratorPartOfGroup()
            {
                Collaborator = investigator,
                CollaboratorEmail = investigator.Email,
                InvestigationGroupId = group.Id,
                ResearchGroup = group
            };

            investigator.CollaboratorPartOfGroups.Add(newCollab);


            _dbContext.Entry(newCollab);
            _dbContext.Entry(newCollab.ResearchGroup);
            _dbContext.Entry(newCollab.Collaborator);
            _dbContext.CollaboratorPartOfGroup.Add(newCollab);
            _dbContext.SaveChanges();
            await _dbContext.SaveEntitiesAsync();
        }

        public async Task AddStudentToGroup(Collaborator collaborator, ResearchGroup group){
            IList<CollaboratorPartOfGroup> partOfGroups = await _dbContext.CollaboratorPartOfGroup.ToListAsync();
            Student student = await _dbContext.Student.FirstOrDefaultAsync(e => e.Email == collaborator.Email);
            if (student == null) {
                student = (Student)collaborator;
                _dbContext.Student.Add(student);
            }

            if (student.CollaboratorPartOfGroups == null){
                student.CollaboratorPartOfGroups = new List<CollaboratorPartOfGroup>();
            }

            CollaboratorPartOfGroup newCollab = new CollaboratorPartOfGroup(){
                Collaborator = student,
                CollaboratorEmail = student.Email,
                InvestigationGroupId = group.Id,
                ResearchGroup = group
            };
            
            student.CollaboratorPartOfGroups.Add(newCollab);

            
            _dbContext.Entry(newCollab);
            _dbContext.Entry(newCollab.ResearchGroup);
            _dbContext.Entry(newCollab.Collaborator);
            _dbContext.CollaboratorPartOfGroup.Add(newCollab);
            _dbContext.SaveChanges();
            await _dbContext.SaveEntitiesAsync();
        }
        public async Task DeleteAuthorOfPublicationAsync(string id)
        {
            var _author = (from a in _dbContext.AuthorOfPublications
                           where a.IdPublication == id
                           select a);
            _dbContext.AuthorOfPublications.RemoveRange(_author);
            _dbContext.SaveChanges();
            await _dbContext.SaveEntitiesAsync();
        }

    }
}

