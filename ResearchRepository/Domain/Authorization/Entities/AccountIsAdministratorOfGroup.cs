using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Domain.Authorization.Entities
{
    public class AccountIsAdministratorOfGroup
    {
        public string Email { get; set; }
        public int GroupId { get; set; }

        public AccountIsAdministratorOfGroup() {
            Email = null!;
            GroupId = -1;
        }

        public AccountIsAdministratorOfGroup(string email, int groupId)
        {
            Email = email;
            GroupId = groupId;
        }

    }
}
