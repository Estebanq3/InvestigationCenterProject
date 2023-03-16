using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Domain.People.Entities
{
    public class PersonWorksForUnit
    {
        public PersonWorksForUnit()
        {
            Email = null;
            UnitName = null;
            _academicUnit = null;

        }
        public PersonWorksForUnit(string email, string _unitName, AcademicUnit unit )
        {
            Email = email;
            UnitName = _unitName;
            _academicUnit =unit;
        }
        public PersonWorksForUnit(string email)
        {
            Email = email;
            UnitName = "N/A";
            _academicUnit = new AcademicUnit();
        }
        public string Email { get; set; }

        public string UnitName { get; set; }

        public AcademicUnit _academicUnit { get; set; }
    }
}
