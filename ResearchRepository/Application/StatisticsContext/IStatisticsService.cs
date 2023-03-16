using ResearchRepository.Domain.StatisticsContext;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchAreas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Application.StatisticsContext
{
    public interface IStatisticsService
    {
        /// <summary>
        /// This method requests a list of all the publications from infrastructure
        /// </summary>
        /// Author: Diana Luna
        /// StoryID: ST-PH-2.1
        /// <returns>List of all publications</returns>
        Task<IList<Statistic>> GetAsync(List<int> _groupsIds);

        /// <summary>
        /// This method requests the areas of the publications and their respective quantity by ids of groups
        /// </summary>
        /// Author: Pablo Otárola
        /// StoryID: ST-PH-2.31
        /// <returns> Dictionary of the areas and each quantity</returns>
        Task<Dictionary<string, int>> GetAreasAsync(List<int> _groupsIds);

        /// <summary>
        /// This method requests the areas of the publications and their respective quantity by ids of groups and areas
        /// </summary>
        /// Author: Pablo Otárola
        /// StoryID: ST-PH-2.31
        /// <returns> Dictionary of the areas and each quantity</returns>
        Task<Dictionary<string, int>> GetSubAreasByAreasAsync(List<int> _groupsIds, List<string> researchAreas);

        /// <summary>
        /// This method requests the total of publications per ids of groups and areas
        /// </summary>
        /// Author: Pablo Otárola
        /// StoryID: ST-PH-2.31
        /// <returns> Dictionary of the areas and each quantity</returns>
        Task<int> GetCountSubAreasByAreasAsync(List<int> _groupsIds, List<string> researchAreas);

        /// <summary>
        /// This method requests a list of all the publications from infrastructureby Research Group Id
        /// </summary>
        /// Author: Frank Alvarado
        /// StoryID: ST-PH-2.11
        /// <returns>List of all publications by Research Group Id</returns>
        Task<IList<Statistic>> GetById(int IdGroup);

        /// <summary>
        /// This method requests the years of all publications and their respective quantity
        /// </summary>
        /// Author: Frank Alvarado
        /// StoryID: ST-PH-2.2
        /// <returns> Dictionary of all the years and each quantity </returns>
        Task<Dictionary<string, int>> GetYearAsync(List<int> _groupsIds);

        /// <summary>
        /// This method requests the years of publications and their respective quantity for a research group
        /// </summary>
        /// Author: Frank Alvarado
        /// StoryID: ST-PH-2.2
        /// <returns> Dictionary of all the years and each quantity </returns>
        Task<Dictionary<string, int>> GetYearByIdAsync(int IdGroup);

        /// <summary>
        /// Request the number of publications per research group
        /// </summary>
        /// Author: Pablo Otárola
        /// StoryID: ST-PH-2.26
        /// <returns> Dictionary with the number of publications per group </returns>
        Task<Dictionary<string, int>> GetPublicationsByGroups(List<int> _groupsIds);

        /// <summary>
        /// Request the research groups
        /// </summary>
        /// Author: Pablo Otárola
        /// StoryID: ST-PH-2.26
        /// <returns> Ilist with the research groups </returns>
        Task<IList<ResearchGroup>> GetGroups(List<int> _groupsIds);

        /// <summary>
        /// This method requests the type of publication of all publications and their respective quantity
        /// </summary>
        /// Author: Frank Alvarado
        /// StoryID: ST-PH-2.2
        /// <returns> Dictionary of all the type publications and each quantity </returns>
        Task<Dictionary<string, int>> GetTypePublicationAsync(List<int> _groupsIds);

        /// <summary>
        /// This method requests the type of publication of all publications and their respective quantity
        /// </summary>
        /// Author: Frank Alvarado
        /// StoryID: ST-PH-2.2
        /// <returns> Dictionary of all the type publications and each quantity </returns>
        Task<Dictionary<string, int>> GetTypePublicationByYearsAsync(List<int> _groupsIds, List<int> _listYears, string type);

        /// <summary>
        /// This method requests the type of publications and their respective quantity for a research group
        /// </summary>
        /// Author: Frank Alvarado
        /// StoryID: ST-PH-2.2
        /// <returns> Dictionary of all the type publications and each quantity </returns>
        Task<Dictionary<string, int>> GetTypePublicationByIdAsync(int IdGroup);
        Task<int> GetPublicationCountByResearchGroupAsync(int researchGroupId);
    }
}
