using System.Collections.Generic;
using OpenDrive.Entities;

namespace OpenDrive.Sharing
{

    

    public class ListSharedUsersResponse
    {

        public int DirUpdateTime { get; set; }
        public int ResponseType { get; set; }
        public List<SharedUser> SharedUsers { get; set; }


    }
    
}
