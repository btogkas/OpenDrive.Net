using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.File
{
    public class FullLinkRequest
    {

        public const string URL = Common.BaseURL + "/file/fulllink.json/{short_link}";

        [JsonProperty(PropertyName = "short_link")]
        public string ShortLink { get; set; }

       
        public string Call()
        {
            var request = (HttpWebRequest)WebRequest.Create(URL.Replace("{short_link}", ShortLink));
            request.Method = "GET";

            try
            {

                var wresponse = request.GetResponse();
                var reader = new StreamReader(wresponse.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
                return result;
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
                                throw new Exception("Not Found");
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

