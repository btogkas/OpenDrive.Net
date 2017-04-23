using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDrive.AccountUsers
{
    public class FolderListResponse
    {
        public string FolderId { get; set; }
        public string Name { get; set; }
        public int Access { get; set; } // integer – Account user folder permission(0-view, 1-edit)

    }
}
