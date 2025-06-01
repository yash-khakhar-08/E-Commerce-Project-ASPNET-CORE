using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketMatrix_Models.ViewModels
{
    public class UserWithRolesVM
    {
        public string UserId { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }

        public string PhoneNumber { get; set; }

        public List<string> Roles { get; set; }

        public string Address { get; set; }

    }
}
