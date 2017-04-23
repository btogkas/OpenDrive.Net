using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenDrive.Constants;
using OpenDrive.Sharing;

namespace OpenDrive.File
{
    public class DeleteFileRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/file.json/{session_id}/{file_id}";

        [JsonProperty(PropertyName = "file_id")]
        public string FileId { get; set; }

        [JsonProperty(PropertyName = "access_folder_id")]
        public string AccessFolderId { get; set; }

        [JsonProperty(PropertyName = "sharing_id")]
        public string SharingId { get; set; }

        public FileResponse Call()
        {

            var request = (HttpWebRequest)WebRequest.Create(URL.Replace("{session_id}", SessionId).Replace("{file_id}", FileId));
            request.Method = "Delete";

            try
            {

                var wresponse = request.GetResponse();
                var reader = new StreamReader(wresponse.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<FileResponse>(result);

            }
            catch (Exception)
            {
                throw new Exception("Invalid sessions please re login");
            }

        }

    }

}

