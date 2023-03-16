using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.Authentication.ValueObjects;

namespace ResearchRepository.Application.People
{
    public interface IAcademicProfileService
    {
        /// <summary>
        /// Get the a list of all AcademicProfiles)
        /// </summary>
        /// Author: Andrea Alvarado
        /// StoryID: ST-PA-3.3
        /// <returns>IList of all Academic Profiles</returns>
        Task<IList<AcademicProfile>> GetAsync();
        /// <summary>
        /// Search a specific academic profile in the database based on their email. 
        /// </summary>
        /// Author: Greivin Sánchez 
        /// StoryID: ST-PA-3.2
        /// <returns>A entity of Academic Profile</returns>
        Task<AcademicProfile?> SearchPersonByEmail(string email);

        /// <summary>
        /// Update a person info. 
        /// </summary>
        /// Author: Sebastián Montero 
        /// StoryID: ST-PA-4.7
        /// <returns>A task</returns>
        Task updateProfileData(string email, string firstName, string lastName1, string lastName2, string description, string degree, string title, string facebookLink, string githubLink, string linkedInLink, string tel, string profilePic);

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
