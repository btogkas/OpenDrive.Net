using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenDrive.File;

namespace OpenDrive.Folders
{
    public class TrashListResponse
    {

        public int FilesCount { get; set; }
        public int FoldersCount { get; set; }
        public List<FolderResponse> Folders { get; set; }
        public List<FileResponse> Files { get; set; }  


    }
}
