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

namespace OpenDrive.File
{
   public class FileVersionRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/file/fileversions.json/{session_id}/{file_group_id}";

        [JsonProperty(PropertyName = "file_group_id")]
        public string FileGroupId { get; set; }

        public void Call()
        {
            var request = (HttpWebRequest)WebRequest.Create(URL.Replace("{session_id}", SessionId).Replace("{file_group_id}", FileGroupId));
            request.Method = "GET";

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
                            case 404:
                                throw new Exception("Not Found");
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
