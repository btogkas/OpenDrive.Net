using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.Session
{
    public class LogOut : PrivateApiRequest
    {
        public const string URL = Common.BaseURL + "/session/logout.json";

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
                return true; //JsonConvert.DeserializeObject<LogOutResponse>(result);
            }
            catch (Exception)
            {
                return false;
                //throw new Exception("Failed to login");
            }

        }

    }
}
