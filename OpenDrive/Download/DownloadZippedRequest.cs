using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenDrive.AccountUsers;
using OpenDrive.Constants;

namespace OpenDrive.Download
{
    public class DownloadZippedRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/download/all.json";

        [JsonProperty(PropertyName = "files")]
        public string Files { get; set; }

        [JsonProperty(PropertyName = "foldres")]
        public string Folders { get; set; }


        [JsonIgnore]
        public string TargetFile { get; set; }

        public void Call()
        {

            try
            {

                //TODO: if we want to add timeout
                //DateTime startTime = DateTime.UtcNow;
                WebRequest request = WebRequest.Create(URL);
                request.Method = "Post";
                var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(this));
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                WebResponse response = request.GetResponse();
                
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (Stream fileStream = System.IO.File.OpenWrite(TargetFile))
                    {
                        byte[] buffer = new byte[4096];
                        int bytesRead = responseStream.Read(buffer, 0, 4096);
                        while (bytesRead > 0)
                        {
                            fileStream.Write(buffer, 0, bytesRead);

                            //TODO: if we want to add timeout
                            //DateTime nowTime = DateTime.UtcNow;
                            //if ((nowTime - startTime).TotalMinutes > 5)
                            //{
                            //    throw new ApplicationException(
                            //        "Download timed out");
                            //}
                            bytesRead = responseStream.Read(buffer, 0, 4096);
                        }
                    }
                }


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
                                throw new Exception("File not exists");
                            case 404:
                                throw new Exception("Not Found: Please select file for upload$");
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
