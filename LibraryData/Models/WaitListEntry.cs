using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
    public class WaitListEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
