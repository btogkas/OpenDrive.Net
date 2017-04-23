using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.Sharing
{
    public class DeleteSharingRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/sharing.json/{session_id}/{sharing_id}";

        [JsonProperty(PropertyName = "sharing_id")]
        public string SharingId { get; set; }

        public ListUsersResponse Call()
        {

            var request = (HttpWebRequest)WebRequest.Create(URL.Replace("{session_id}", SessionId).Replace("{sharing_id}", SharingId));
            request.Method = "Delete";

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
