using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDrive.File
{
    public class UpdateFileAccessResponse
    {

        public string FileId { get; set; }
        public string Name { get; set; }
        public string GroupID { get; set; }
        public string Extension { get; set; }
        public string Size { get; set; }
        public string Views { get; set; }
        public string Downloads { get; set; }
        public string DateModified { get; set; }
        public string Access { get; set; }
        public string Link { get; set; }
        public string DownloadLink { get; set; }
        public string StreamingLink { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public int DirUpdateTime {get;set;}


    }
}
