using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenDrive.File;

namespace OpenDrive.Folders
{
    public  class SharedFolderDetailsResponse
    {
        public FolderInfoDetails  FolderInfo { get; set; }
    }

    public class FolderInfoDetails
    {
        public string FolderID { get; set; }
        public string Name{get;set;}
        public string DateCreated{get;set;}
        public string DateTrashed {get;set;}
        public string DirUpdateTime {get;set;}
        public string Access {get;set;}
        public string PublicUpload {get;set;}
        public string PublicContent {get;set;}
        public string DateModified {get;set;}
        public string Owner {get;set;}
        public string Shared {get;set;}
        public string OwnerSuspended {get;set;}
        public string Description {get;set;}
        public string Permission {get;set;}
        public string PublicDownload {get;set;}
        public string Link {get;set;}
        public string Lang {get;set;}
        public string OwnerLogo {get;set;}
        public string CompanyName {get;set;}
        public string Encrypted {get;set;}
        public List<FileResponse> Files { get; set; } 

    }
}
