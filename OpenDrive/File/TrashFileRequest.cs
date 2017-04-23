﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.File
{
    public class TrashFileRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/file/trash.json";

        [JsonProperty(PropertyName = "file_id")]
        public string FileId { get; set; }

        [JsonProperty(PropertyName = "access_folder_id")]
        public string AccessFolderId { get; set; }

        [JsonProperty(PropertyName = "sharing_id")]
        public string SharingId { get; set; }


        public FileResponse Call()
        {
            var request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.AddPostVariable(this);

            try
            {

                var wresponse = request.GetResponse();
                var reader = new StreamReader(wresponse.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<FileResponse>(result);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        switch ((int)response.StatusCode)
                        {
                            case 400:
                                throw new Exception("Invalid file ID");
                            case 401:
                                throw new Exception("Unauthorized: Session has expired. Please right click on OpenDrive task bar icon, log out and then log back in.");

                        }
                    }
                    else
                    {
                        throw;
                    }
                }
                throw;
            }

        }

    }
}
