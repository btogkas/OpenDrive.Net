using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.Session
{
    public class PartnerLogin
    {
        public const string URL = Common.BaseURL + "/session/partnerlogin.json";

        [JsonProperty(PropertyName = "partner_name")]
        public string PartnerName
        {
            get; set;
        }

        [JsonProperty(PropertyName = "passwd")]
        public string Password
        {
            get; set;
        }

        public bool Call()
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
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
