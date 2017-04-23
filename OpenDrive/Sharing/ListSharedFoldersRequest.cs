using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.Sharing
{
    public class ListSharedFoldersRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/sharing/listsharedfolders.json/{session_id}/{sharing_id}";

        [JsonProperty(PropertyName = "sharing_id")]
        public string SharingId { get; set; }


        public ListSharedFoldersResponse Call()
        {
            var request = (HttpWebRequest)WebRequest.Create(URL.Replace("{session_id}",SessionId).Replace("{sharing_id}", SharingId));
            request.Method = "GET";
            
            try
            {

                var wresponse = request.GetResponse();
                var reader = new StreamReader(wresponse.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<ListSharedFoldersResponse>(result);

            }
            catch (Exception)
            {
                throw new Exception("Invalid sessions please re login");
            }

        }

    }
}
