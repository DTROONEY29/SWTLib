using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
