using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDrive.AccountUsers
{
    public class InfoResponse
    {

        public string AccessUserID { get; set; }
        public string Email { get; set; }
        public int AdminMode { get; set; }
        public int AccessActive { get; set; }
        public bool AccessNotification { get; set; }
        public string AccessPosition { get; set; }
        public bool AccessPasswordChange { get; set; }
        public int BwMax { get; set; }
        public int BwUsed { get; set; }
        public int StorageMax { get; set; }
        public int StorageUsed { get; set; }
        public string Phone { get; set; }
        public string AccessSince { get; set; }
        public dynamic AccessFolders { get; set; }

    }
}
