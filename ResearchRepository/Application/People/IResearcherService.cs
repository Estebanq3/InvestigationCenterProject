using ResearchRepository.Domain.People.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Application.People
{
    public interface IResearcherService
    {
        /// <summary>
        /// TEMPORAL: Solo es para pruebas. Implementación debe ser hecha por otro equipo.
        /// </summary>
        /// Author: Dean Vargas
        /// StoryID: ST-MM-2.1
        /// <param name="idGroup"></param>
        /// <param name="currentPage"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<IEnumerable<Researcher>?> GetResearchersAsync(int idGroup, int currentPage, int size);
    }
}
