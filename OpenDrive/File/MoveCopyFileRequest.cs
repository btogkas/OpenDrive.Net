using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.File
{
    public class MoveCopyFileRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/file/move_copy.json";


        [JsonProperty(PropertyName = "src_file_id")]
        public string SourceFileId { get; set; }

        [JsonProperty(PropertyName = "dst_folder_id")]
        public string DestinationFolderId { get; set; }

        [JsonProperty(PropertyName = "move")]
        public string Move { get; set; } //true = move false = copy

        [JsonProperty(PropertyName = "overwrite_if_exists")]
        public string Overwrite { get; set; } //(true, false).

        [JsonProperty(PropertyName = "src_access_folder_id")]
        public string SourceAccessFolder { get; set; }

        [JsonProperty(PropertyName = "dst_access_folder_id")]
        public string DestinationAccessFolder { get; set; }

        [JsonProperty(PropertyName = "src_sharing_id")]
        public string SourceSharingId { get; set; }

        [JsonProperty(PropertyName = "dst_sharing_id")]
        public string DestinationSharingId { get; set; }

        [JsonProperty(PropertyName = "new_file_name")]
        public string NewFileName { get; set; }


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
                                throw new Exception("Values required");
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
