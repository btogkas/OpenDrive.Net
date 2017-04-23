using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenDrive.AccountUsers;
using OpenDrive.Constants;

namespace OpenDrive.Download
{
    public class DownloadFileRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/download/file.json/{file_id}?session_id={sessionId}";



        [JsonProperty(PropertyName = "file_id")]
        public string FileId { get; set; }

        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

        [JsonProperty(PropertyName = "inline")]
        public int Inline { get; set; }

        [JsonProperty(PropertyName = "sharing_id")]
        public string SharingId { get; set; }

        [JsonProperty(PropertyName = "test")]
        public int Test { get; set; }

        [JsonProperty(PropertyName = "backup")]
        public int Backup { get; set; }

        [JsonProperty(PropertyName = "app")]
        public string App { get; set; }


        [JsonIgnore]
        public string TargetFile { get; set; }

       
        public void Call()
        {
            string url = URL.Replace("{file_id}", FileId).
                Replace("{sessionId}", SessionId);
            

            try
            {

                WebClient client = new WebClient { BaseAddress = url };
                client.DownloadFile(url, TargetFile);
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
                                throw new Exception("File not exists");
                            case 404:
                                throw new Exception("Invalid value specified");
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
