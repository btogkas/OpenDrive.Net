using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.Sharing
{
    public class ListUsersRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/sharing/listusers.json/{session_id}/{folder_id}";

        [JsonProperty(PropertyName = "folder_id")]
        public string FolderId { get; set; }


        public ListUsersResponse Call()
        {

            var request = (HttpWebRequest)WebRequest.Create(URL.Replace("{session_id}", SessionId).Replace("{folder_id}",FolderId));
            request.Method = "GET";

            try
            {

                var wresponse = request.GetResponse();
                var reader = new StreamReader(wresponse.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<ListUsersResponse>(result);

            }
            catch (Exception)
            {
                throw new Exception("Invalid sessions please re login");
            }

        }


    }

}
