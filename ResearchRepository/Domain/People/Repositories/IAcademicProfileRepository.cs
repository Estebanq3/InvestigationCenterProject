using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.Authentication.ValueObjects;
using ResearchRepository.Domain.Core.Repositories;
//using ResearchRepository.Domain.People.DTOs;

namespace ResearchRepository.Domain.People.Repositories
{
    public interface IAcademicProfileRepository
    {


        /// <summary>
        /// Get the a list of all AcademicProfiles)
        /// </summary>
        /// Author: Andrea Alvarado
        /// StoryID: ST-PA-3.3
        /// <returns>IList of all Academic Profiles</returns>
        Task<IList<AcademicProfile>> GetAsync();

        /// <summary>
        /// This method return an academic profile that match with a specific email
        /// </summary>
        /// <param name = "email" > email of the profile</param>
        /// Author: Greivin Sanchez
        /// StoryID: ST-PA-3.2
        /// <returns>Return an academic profile that match with a specific email</returns>
        Task<AcademicProfile?> SearchPersonByEmail(string email);
        Task updateProfileData(string email, string firstName, string lastName1, string lastName2, string description, string degree, string title, string facebookLink, string githubLink, string linkedInLink,string tel, string profilePic);

        /// <summary>
        /// Obtain the units that a person works for
        /// </summary>
        /// Author: Andrea Alvarado Acon
        /// StoryID: ST-PA-3.15
        /// <param name="email">Person´s email</param>
        /// <returns>List of person´s units.</returns>
        Task<IList<PersonWorksForUnit>> GetPersonWorksForUnitByEmail(string email);

        /// <summary>
        /// Obtain the universities a person belongs to
        /// </summary>
        /// Author: Andrea Alvarado Acon
        /// StoryID: ST-PA-3.17
        /// <param name="email">Person´s email</param>
        /// <returns>List of person´s universities.</returns>
        Task<IList<PersonsBelongsToUniversity>> GetPersonBelongsToUniversityByEmail(string email);

        /// <summary>
        /// Deletes a relationship between a unit and a person
        /// </summary>
        /// Author: Andrea Alvarado Acon
        /// StoryID: ST-PA-3.17
        /// <param name="personWorksForUnit">The relationship to delete</param>
        void DeletePersonWorksForUnit(PersonWorksForUnit personWorksForUnit);


        /// <summary>
        /// Adds a relationship between a unit and a person
        /// </summary>
        /// Author: Andrea Alvarado Acon
        /// StoryID: ST-PA-3.17
        /// <param name="unitName">The name of the unit</param>
        /// <param name="personEmail">The email of the person</param>
        Task AddPersonBelongsToAcademicUnit(string unitName, string personEmail);

        /// <summary>
        /// Deletes a relationship between a university and a person
        /// </summary>
        /// Author: Andrea Alvarado Acon
        /// StoryID: ST-PA-3.17
        /// <param name="personsBelongsToUniversity">The relationship to delete</param>
        void DeletePersonBelongsToUniversity(PersonsBelongsToUniversity personsBelongsToUniversity);


        /// <summary>
        /// Adds a relationship between a university and a person
        /// </summary>
        /// Author: Andrea Alvarado Acon
        /// StoryID: ST-PA-3.17
        /// <param name="universityName">The name of the university</param>
        /// <param name="personEmail">The email of the person</param>
        Task AddPersonBelongsToUniversity(string universityName, string personEmail);

        /// <summary>
        /// Create a new profile tuple in the database with the register data
        /// </summary>
        /// Author: Dylan Arias
        /// StoryID: ST-PA-4.15
        /// <param name="r">Object containing new profile information</param>
        /// <returns></returns>
        Task registerUser(Register r);

    }
}