using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.Sharing
{
    public class SharingRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/sharing.json";

        [JsonProperty(PropertyName = "folder_id")]
        public string FolderId { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "sharemode")]
        public string SharedMode { get; set; }  //) viewMode //1 fullaccess


        public SharingResponse Call()
        {
            var request = (HttpWebRequest)WebRequest.Create(URL);

            var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(this));
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {
                var wresponse = request.GetResponse();
                var reader = new StreamReader(wresponse.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<SharingResponse>(result);
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
                            case 404:
                                throw new Exception("This folder already shared with this user");
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
