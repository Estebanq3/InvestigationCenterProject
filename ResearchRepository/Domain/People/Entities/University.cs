﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Domain.People.Entities
{
    public class University
    {
        public string Name{get; set;}

        public IList<PersonsBelongsToUniversity> personsBelongsToUniversityList { get; set; }
    }
}
