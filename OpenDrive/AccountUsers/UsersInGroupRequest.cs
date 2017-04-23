using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.AccountUsers
{
    public class UsersInGroupRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/accountusers/usersingroup.json/{session_id}/{group_id}";

        [JsonProperty(PropertyName = "group_id")]
        public string GroupId { get; set; }

        [JsonProperty(PropertyName = "search_query")]
        public string SearchQuery { get; set; }

        public UsersInGroupResponse Call()
        {
            var request = (HttpWebRequest)WebRequest.Create(URL.Replace("{session_id}", SessionId).Replace("{group_id}", GroupId));
            request.Method = "GET";

            try
            {
                var wresponse = request.GetResponse();
                var reader = new StreamReader(wresponse.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<UsersInGroupResponse>(result);

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
                                throw new Exception("Invalid permissions");
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
