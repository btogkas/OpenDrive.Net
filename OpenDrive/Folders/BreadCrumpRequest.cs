using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenDrive.Constants;
using OpenDrive.File;

namespace OpenDrive.Folders
{
    public class BreadCrumpRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/folder/breadcrumb.json/{session_id}/{folder_id}";

        [JsonProperty(PropertyName = "folder_id")]
        public string FolderId { get; set; }


        public FolderResponse Call()
        {
            var request = (HttpWebRequest)WebRequest.Create(URL.Replace("{session_id}", SessionId).Replace("{folder_id}", FolderId));
            request.Method = "GET";

            try
            {
                var wresponse = request.GetResponse();
                var reader = new StreamReader(wresponse.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<FolderResponse>(result);
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
                            case 404:
                                throw new Exception("Wrong folder ID");
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
