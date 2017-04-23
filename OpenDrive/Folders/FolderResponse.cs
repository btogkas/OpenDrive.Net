using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenDrive.File;

namespace OpenDrive.Folders
{
    public class FolderResponse
    {

        public string ID { get; set; }
        public string FolderID { get; set; }
        public string Name { get; set; }
        public int DateCreated { get; set; }
        public int DateTrashed { get; set; }
        public int DirUpdateTime { get; set; }
        public int Access { get; set; }
        public bool PublicUpload { get; set; }
        public bool PublicContent { get; set; }
        public int DateModified { get; set; }

        public string Shared { get; set; }
        public int Owner { get; set; }
        public int OwnerLevel { get; set; }
        public bool OwnerSuspended { get; set; }
        public string Description { get; set; }
        public int Permission { get; set; }
        public bool PublicDownload { get; set; }
        public string Link { get; set; }
        public string Lang { get; set; }
        public string Encrypted { get; set; }
        public string ParentFolderID { get; set; }
        public string DirectFolderLink { get; set; }
        public int PeResponseType { get; set; }


        public List<FolderResponse> Folders { get; set; }
        public List<FileResponse> Files{ get; set; }


    }
}
