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

namespace OpenDrive.AccountUsers
{
    public class FolderListRequest: PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/accountusers/folderslist.json/{session_id}/{folder_id}";


        [JsonProperty(PropertyName = "folder_id")]
        public string FolderId { get; set; }

        [JsonProperty(PropertyName = "access_email")]
        public string AccessEmail { get; set; }

        public FolderListResponse Call()
        {
            var request = (HttpWebRequest)WebRequest.Create(URL.Replace("{session_id}", SessionId).Replace("{folder_id}", FolderId));
            request.Method = "GET";

            try
            {

                var wresponse = request.GetResponse();
                var reader = new StreamReader(wresponse.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<FolderListResponse>(result);

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
                            case 401:
                                throw new Exception("Unauthorized: Invalid sessions please re login");
                            case 400:
                                throw new Exception("Bad Request: invalid value specified for `access_email`. Expecting email in `name@example.com` format");
                            case 404:
                                throw new Exception("Not Found: Directory doesn't exist");
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
