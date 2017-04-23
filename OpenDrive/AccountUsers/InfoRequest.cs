using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.AccountUsers
{
    public class InfoRequest: PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/accountusers/info.json/{session_id}/{access_email}";


        [JsonProperty(PropertyName = "access_email")]
        public string AccessEmail { get; set; }

        public InfoResponse Call()
        {
            var request = (HttpWebRequest)WebRequest.Create(URL.Replace("{session_id}", SessionId).Replace("{access_email}", AccessEmail));
            request.Method = "GET";

            try
            {

                var wresponse = request.GetResponse();
                var reader = new StreamReader(wresponse.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<InfoResponse>(result);

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
                            case 400:
                                throw new Exception("Bad Request: invalid value specified for `access_email`. Expecting email in `name@example.com` format");
                            case 404:
                                throw new Exception("No user found");
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
