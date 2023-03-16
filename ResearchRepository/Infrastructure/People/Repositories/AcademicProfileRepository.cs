using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.People.Repositories;
using ResearchRepository.Infrastructure.People;
using ResearchRepository.Domain.Authentication.ValueObjects;
using System.Text.RegularExpressions;

namespace ResearchRepository.Infrastructure.People.Repositories
{
    internal class AcademicProfileRepository : IAcademicProfileRepository
    {
        private readonly PeopleDbContext _dbContext;

        public IUnitOfWork UnitOfWork => _dbContext;
        

        public AcademicProfileRepository(PeopleDbContext unitOfWork)
        {
            _dbContext = unitOfWork;
        }

        public async Task<IList<AcademicProfile>> GetAsync()
        {
            return await _dbContext.AcademicProfile.ToListAsync();
        }

        public async Task<AcademicProfile?> SearchPersonByEmail(string email)
        {

            return await _dbContext.AcademicProfile.FirstOrDefaultAsync(t => t.Email == email);
        }

        public async Task updateProfileData(string email, string name, string firstLastName, string secondLastName, string description, string academicDegree, string title, string facebookLink, string githubLink, string linkedInLink, string tel, string profilePic) {
            Person person = await _dbContext.Person.FirstOrDefaultAsync(p => p.Email == email);
            person.AcademicProfile = await _dbContext.AcademicProfile.FirstOrDefaultAsync(p => p.Email == email);
            Regex r = new Regex(@"^[+]{0,1}[(]{0,1}[0-9]{0,4}[)]{0,1}[-\s\./0-9]*$");
            Regex i = new Regex(@"^[a-zA-Z ]*$");

            if (person != null && person.AcademicProfile != null && r.IsMatch(tel) && i.IsMatch(name) && i.IsMatch(firstLastName) && i.IsMatch(secondLastName)) {
                person.FirstName = name;
                person.FirstLastName =firstLastName;
                person.SecondLastName = secondLastName;
                person.AcademicProfile.Biography = description;
                person.AcademicProfile.Degree = academicDegree;
                person.AcademicProfile.Title = title;
                person.AcademicProfile.FacebookLink = facebookLink;
                person.AcademicProfile.ProfilePic = profilePic;
                person.AcademicProfile.Tel = tel;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<PersonWorksForUnit>> GetPersonWorksForUnitByEmail(string email)
        {
            return await _dbContext.PersonWorksForUnit.Where(e => e.Email == email).ToListAsync();
        }

        public async Task<IList<PersonsBelongsToUniversity>> GetPersonBelongsToUniversityByEmail(string email)
        {
            return await _dbContext.PersonsBelongsToUniversity.Where(e => e.PersonEmail == email).ToListAsync();
        }

        public void DeletePersonWorksForUnit(PersonWorksForUnit personWorksForUnit) {
            _dbContext.PersonWorksForUnit.Remove(personWorksForUnit);
            _dbContext.SaveChanges();
        }

        public void DeletePersonBelongsToUniversity(PersonsBelongsToUniversity personsBelongsToUniversity)
        {
            _dbContext.PersonsBelongsToUniversity.Remove(personsBelongsToUniversity);
            _dbContext.SaveChanges();
        }

        public async Task AddPersonBelongsToAcademicUnit(string unitName, string personEmail) {
            AcademicUnit academicUnit = await _dbContext.AcademicUnits.FirstOrDefaultAsync(e => e.Name == unitName);
            if (academicUnit == null) {
                AcademicUnit newAcademicUnit = new AcademicUnit();
                newAcademicUnit.Name = unitName;
                _dbContext.AcademicUnits.Add(newAcademicUnit);
                academicUnit = newAcademicUnit;
            }
            PersonWorksForUnit newPersonWorksForUnit = new PersonWorksForUnit();
            newPersonWorksForUnit.Email = personEmail;
            newPersonWorksForUnit.UnitName = unitName;
            newPersonWorksForUnit._academicUnit = academicUnit;
            _dbContext.PersonWorksForUnit.Add(newPersonWorksForUnit);
            _dbContext.SaveChanges();

        }

        public async Task AddPersonBelongsToUniversity(string universityName, string personEmail)
        {
            University university = await _dbContext.Universities.FirstOrDefaultAsync(e => e.Name == universityName);
            if (university == null)
            {
                University newUniversity = new University();
                newUniversity.Name = universityName;
                _dbContext.Universities.Add(newUniversity);
                university = newUniversity;
            }
            PersonsBelongsToUniversity newPersonBelongsToUniversity = new PersonsBelongsToUniversity();
            newPersonBelongsToUniversity.UniversityName = universityName;
            newPersonBelongsToUniversity.PersonEmail = personEmail;
            newPersonBelongsToUniversity.university = university;
            _dbContext.PersonsBelongsToUniversity.Add(newPersonBelongsToUniversity);
            _dbContext.SaveChanges();

        }

        public async Task registerUser(Register r)
        {
            /*AcademicProfile a = new AcademicProfile();
            a.Email = r.Email;
            a.Biography = r.Biography;
            a.Degree = r.Degree;
            a.Title = r.Title;*/
            await _dbContext.Database.ExecuteSqlInterpolatedAsync($"execute new_profile @email = {r.Email}, @degree = {r.Degree}, @biography = {r.Biography}, @title = {r.Title}");
            await AddPersonBelongsToUniversity(r.University, r.Email);
            if (r.AcademicUnit != null)
            {
                await AddPersonBelongsToAcademicUnit(r.AcademicUnit, r.Email);
            }
            //await _dbContext.AcademicProfile.AddAsync(a);
            //await _dbContext.SaveChangesAsync();
        }
    }
}