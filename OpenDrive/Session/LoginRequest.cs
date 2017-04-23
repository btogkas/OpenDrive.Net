using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.Session
{
    public class LoginRequest
    {

        public const string URL = Common.BaseURL + "/session/login.json";

        [JsonProperty(PropertyName = "username")]
        public string UserName
        {
            get; set; 
        }

        [JsonProperty(PropertyName = "passwd")]
        public string Password
        {
            get; set;
        }

        [JsonProperty(PropertyName = "version")] //string - Application version number (max 10).
        public string Version
        {
            get; set;
        }

        [JsonProperty(PropertyName = "partner_id")] //string - Partner username  (Empty for OpenDrive)
        public string PartnerId
        {
            get; set;
        }

        public LoginResponse Call()
        {
            var request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(this));
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
                return JsonConvert.DeserializeObject<LoginResponse>(result);
            }
            catch (WebException wex)
            {
                throw new Exception("Could not connect to Open Drive");
                
                
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to login");
            }
            
        }

    }
}
