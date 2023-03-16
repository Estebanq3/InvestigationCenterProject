using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Domain.People.Entities
{
    public class AcademicUnit
    {
        public string Name { get; set; }
        public IList<PersonWorksForUnit> _personWorksForUnit { get; set; }

        public AcademicUnit(string name) {
            Name = name;
            _personWorksForUnit = null!;
        }

        public AcademicUnit() {
            Name = null!;
            _personWorksForUnit = null!;
        }
    }
}
