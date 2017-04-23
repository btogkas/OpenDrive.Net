using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDrive.File
{
    public class FileInfoResponse
    {

        public string FileId { get; set; }
        public string Name { get; set; }
        public string GroupID { get; set; }
        public string Size { get; set; }
        public string Views { get; set; }
        public string Version { get; set; }
        public string Downloads { get; set; }
        public string DateTrashed { get; set; }
        public string DateModified { get; set; }
        public string Access { get; set; }
        public string FileHash { get; set; }
        public string Link { get; set; }
        public string DownloadLink { get; set; }
        public string StreamingLink { get; set; }
        public string OwnerName { get; set; }
        public long BWExceeded { get; set; }
        public string Password { get; set ; }
        public int EditOnline { get; set; }
        public string Description { get; set; }
        public string IsArchive { get; set; }
        public string Date { get; set; }
        public int DateUploaded { get; set; }
        public string DateAccessed { get; set; } //The date the file was accessed. 0 if it never was accessed.
        public int AccessDisabled { get; set; }
        public string DestURL{get;set;}
        public string Owner { get; set; }
        public string AccessUser { get; set; }
        public int DirUpdateTime { get; set; }
        /*
         * FileId: string - A file’s ID. 
         * Name: string - The file’s name. 
         * GroupID: string - Size: string - The file size in bytes. 
         * Views: string - 
         * Version: string - 
         * Downloads: string - 
         * DateTrashed: string - The date the file was trashed. 0 if it never was. 
         * DateModified: string - The date last modified. 
         * Access: string - 
         * FileHash: string - 
         * Link: strinh - DownloadLink: string - The files download link. StreamingLink: string - The files streaming link. OwnerName: string - BWExceeded: integer - Password: string - EditOnline: integer - Description: string - IsArchive: string - Date: string - DateUploaded: integer - The date the file was uploaded. DateAccessed: string - The date the file was accessed. 0 if it never was accessed. AccessDisabled: integer - DestURL: string - Owner: string - AccessUser: string - DirUpdateTime: integer - The directory update time.*/
    }
}
