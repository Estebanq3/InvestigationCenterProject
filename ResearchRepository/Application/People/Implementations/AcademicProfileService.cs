using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.People.Repositories;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.Authentication.ValueObjects;
namespace ResearchRepository.Application.People.Implementations
{
    public class AcademicProfileService:IAcademicProfileService
    {
        private readonly IAcademicProfileRepository _academicProfileRepository;

        public AcademicProfileService(IAcademicProfileRepository academicProfileRepository)
        {
            _academicProfileRepository = academicProfileRepository;
        }

        public async Task<IList<AcademicProfile>> GetAsync()
        {
            return await _academicProfileRepository.GetAsync();
        }
        public async Task<AcademicProfile?> SearchPersonByEmail(string email) {
            return await _academicProfileRepository.SearchPersonByEmail(email);
        }

        public async Task updateProfileData(string email, string name, string firstLastName, string secondLastName, string description, string academicDegree, string title, string facebookLink, string githubLink, string linkedInLink, string tel, string profilePic) {
            await _academicProfileRepository.updateProfileData(email, name, firstLastName, secondLastName, description, academicDegree, title, facebookLink, githubLink, linkedInLink,tel,profilePic);
        }

        public async Task<IList<PersonWorksForUnit>> GetPersonWorksForUnitByEmail(string email)
        {
            return await _academicProfileRepository.GetPersonWorksForUnitByEmail(email);
        }

        public async Task<IList<PersonsBelongsToUniversity>> GetPersonBelongsToUniversityByEmail(string email) {
            return await _academicProfileRepository.GetPersonBelongsToUniversityByEmail(email);
        }

        public void DeletePersonWorksForUnit(PersonWorksForUnit personWorksForUnit) {
            _academicProfileRepository.DeletePersonWorksForUnit(personWorksForUnit);
        }

        public async Task AddPersonBelongsToAcademicUnit(string unitName, string personEmail) {
            await _academicProfileRepository.AddPersonBelongsToAcademicUnit(unitName, personEmail);
        }

        public void DeletePersonBelongsToUniversity(PersonsBelongsToUniversity personsBelongsToUniversity) {
            _academicProfileRepository.DeletePersonBelongsToUniversity(personsBelongsToUniversity);
        }

        public async Task AddPersonBelongsToUniversity(string universityName, string personEmail) {
            await _academicProfileRepository.AddPersonBelongsToUniversity(universityName, personEmail);
        }

        public async Task registerUser(Register r)
        {
            await _academicProfileRepository.registerUser(r);
        }

    }
}
