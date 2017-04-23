using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.Folders
{
    public class EditFolderSettingsRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/folder/foldersettings.json";

        [JsonProperty(PropertyName = "access_folder_id")]
        public string AccessFolderId { get; set; }

        [JsonProperty(PropertyName = "folder_id")]
        public string FolderId { get; set; }

        [JsonProperty(PropertyName = "folder_name")]
        public string FolderName { get; set; }

        [JsonProperty(PropertyName = "folder_description")]
        public string FolderDescription { get; set; }

        [JsonProperty(PropertyName = "folder_access")]
        public int FolderAccess { get; set; }

        [JsonProperty(PropertyName = "folder_public_upl")]
        public int FolderPublicUpload { get; set; }

        [JsonProperty(PropertyName = "folder_public_display")]
        public int FolderPublicDisplay { get; set; }

        [JsonProperty(PropertyName = "folder_public_dnl")]
        public int FolderPublicDownload { get; set; }

        [JsonProperty(PropertyName = "sharing_id")]
        public int SharingId { get; set; }

        public void Call()
        {
            var request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.AddPostVariable(this);
            try
            {

                var wresponse = request.GetResponse();
                var reader = new StreamReader(wresponse.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
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
