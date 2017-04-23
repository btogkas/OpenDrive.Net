using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.Session
{
    public class SessionExistsRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/session/exists.json";


        public bool Call()
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
                return JsonConvert.DeserializeObject<Dictionary<string,string>>(result)["result"] == "true";
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
