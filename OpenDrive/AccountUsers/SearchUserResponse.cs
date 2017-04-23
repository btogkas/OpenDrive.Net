using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDrive.AccountUsers
{
    public class SearchUserResponse
    {

        public string AccessUserID { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public int AdminMode { get; set; } //Account user admin mode (0 =users, 1=admin, 2=files).
        public int AccessActive { get; set; } //  Account user status (0 = inactive, 1=active, 2 - blocked).
        public int BwMax { get; set; }
        public int BwUsed { get; set; }
        public int StorageMax { get; set; }
        public int StorageUsed { get; set; }
        public string AccessSince { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }

    }
}
