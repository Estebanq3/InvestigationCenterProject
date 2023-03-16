using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.ResearchGroups.Entities;

namespace ResearchRepository.Domain.People.Entities
{
    public class Subscriptions
    {
        public Subscriptions(int groupID, string userEmail)
        {
            GroupID = groupID;
            UserEmail = userEmail;
        }

        public Subscriptions()
        {

        }

        public int GroupID { get; set; }

        public string UserEmail { get; set; }

        public ResearchGroup Group { get; set; }
        public Person User { get; set; }
        
    }
}
