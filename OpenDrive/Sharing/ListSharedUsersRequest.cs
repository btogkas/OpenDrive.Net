using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.Sharing
{
    public class ListSharedUsersRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/sharing/listsharedusers.json/{session_id}";

        public ListSharedUsersResponse Call()
        {

            var request = (HttpWebRequest)WebRequest.Create(URL.Replace("{session_id}", SessionId));
            request.Method = "GET";

            try
            {

                var wresponse = request.GetResponse();
                var reader = new StreamReader(wresponse.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<ListSharedUsersResponse>(result);

            }
            catch (Exception)
            {
                throw new Exception("Invalid sessions please re login");
            }

        }

    }
}
