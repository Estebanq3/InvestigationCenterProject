using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Domain.People.Entities
{
    public class PersonsBelongsToUniversity
    {
        public string PersonEmail { get; set; }
        public string UniversityName { get; set; }

        public University university { get; set; }
    }
}
