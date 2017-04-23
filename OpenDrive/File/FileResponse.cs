using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenDrive.File
{
    public class FileResponse
    {

        public string FileId { get; set; }
        public string Name { get; set; }
        public string GroupId { get; set; }
        public string Extension { get; set; }
        public string Size { get; set; }
        public string Views { get; set; }
        public string Version { get; set; }
        public string Downloads { get; set; }
        public string DateTrashed { get; set; }
        public string DateModified { get; set; }
        public string OwnerSuspended { get; set; }
        public string AccType { get; set; }
        public string Access { get; set; }
        public string Link { get; set; }
        public string DownloadLink { get; set; }
        public string StreamingLink { get; set; }
        public string OwnerName { get; set; }
        public string OwnerLevel { get; set; }

        public string Password { get; set; }
        public string Description { get; set; }
        public int DirUpdateTimeSrc { get; set; }
        public string ParentID { get; set; }
        public int DirUpdateTime { get; set; }
        public string ThumbLink { get; set; }
        public int EditOnline { get; set; }

        [JsonProperty(PropertyName = "update_time")]
        public int UpdateTime { get; set; }

        [JsonProperty(PropertyName = "upload_speed_limit")]
        public long UploadSpeedLimit { get; set; }

        [JsonProperty(PropertyName = "download_speed_limit")]
        public long DownloadSpeedLimit { get; set; }

        public long BWExceeded { get; set; }

        public int Encrypted { get; set; }

        public string RelativeLocation { get; set; }
        public string TempLocation { get; set; }

        public int SpeedLimit { get; set; }
        public int RequireCompression { get; set; }
        public int RequireHash { get; set; }
        public int RequireHashOnly { get; set; }
    }
}
