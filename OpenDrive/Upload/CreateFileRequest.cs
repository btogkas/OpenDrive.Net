using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenDrive.Constants;
using OpenDrive.File;

namespace OpenDrive.Upload
{
    public class CreateFileRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/upload/create_file.json";

        [JsonProperty(PropertyName = "folder_id")]
        public string FolderId
        {
            get; set;
        }

        [JsonProperty(PropertyName = "file_name")]
        public string FileName //FileSize in bytes
        {
            get; set;
        }

        [JsonProperty(PropertyName = "access_folder_id")]
        public string AccessFolderId
        {
            get; set;
        }

        [JsonProperty(PropertyName = "file_size")]
        public long FileSize
        {
            get; set;
        }
        
        [JsonProperty(PropertyName = "file_hash")]
        public string FileHash
        {
            get; set;
        }

        [JsonProperty(PropertyName = "sharing_id")]
        public string SharingID
        {
            get; set;
        }

        [JsonProperty(PropertyName = "open_if_exists")]
        public int OpenIfExists //(1) - file info will be returned if file already exists, (0) - error 409 will be returned if file already exists.
        {
            get; set;
        }
        // : string - MD5 File Hash (Optional).  : string - .

        public FileResponse Call()
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
                return JsonConvert.DeserializeObject<FileResponse>(result);


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
                                throw new Exception("Invalid file ID");
                            case 401:
                                throw new Exception("Unauthorized: Session has expired. Please right click on OpenDrive task bar icon, log out and then log back in.");

                        }
                    }
                    else
                    {
                        throw;
                    }
                }
                throw;
            }


            //string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            //byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            //HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            //wr.ContentType = "multipart/form-data; boundary=" + boundary;
            //wr.Method = "POST";
            //wr.KeepAlive = true;

            //Stream rs = wr.GetRequestStream();

            //string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            //foreach (string key in nvc.Keys)
            //{
            //    rs.Write(boundarybytes, 0, boundarybytes.Length);
            //    string formitem = string.Format(formdataTemplate, key, nvc[key]);
            //    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
            //    rs.Write(formitembytes, 0, formitembytes.Length);
            //}
            //rs.Write(boundarybytes, 0, boundarybytes.Length);

            //string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            //string header = string.Format(headerTemplate, paramName, file, contentType);
            //byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            //rs.Write(headerbytes, 0, headerbytes.Length);

            //FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            //byte[] buffer = new byte[4096];
            //int bytesRead = 0;
            //while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            //{
            //    rs.Write(buffer, 0, bytesRead);
            //}
            //fileStream.Close();

            //byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            //rs.Write(trailer, 0, trailer.Length);
            //rs.Close();

            //WebResponse wresp = null;
            //try
            //{
            //    wresp = wr.GetResponse();
            //    Stream stream2 = wresp.GetResponseStream();
            //    StreamReader reader2 = new StreamReader(stream2);

            //}
            //catch (Exception ex)
            //{
            //    if (wresp != null)
            //    {
            //        wresp.Close();
            //        wresp = null;
            //    }
            //}
            //finally
            //{
            //    wr = null;
            //}

        }

    }
}
