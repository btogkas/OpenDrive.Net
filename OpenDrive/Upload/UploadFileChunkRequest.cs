using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenDrive.Constants;
using OpenDrive.File;

namespace OpenDrive.Upload
{
    public class UploadFileChunkRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/upload/upload_file_chunk.json";

        [JsonProperty(PropertyName = "file_Id")]
        public string FileId
        {
            get; set;
        }

        [JsonProperty(PropertyName = "temp_location")]
        public string TempLocation
        {
            get; set;
        }

        [JsonProperty(PropertyName = "chunk_offset")]
        public int ChunkOffset
        {
            get; set;
        }


        [JsonProperty(PropertyName = "chunk_size")]
        public long ChunkSize
        {
            get; set;
        }

        [JsonIgnore]
        public string FullFilePath { get; set; }

        //If this runs once then it will be ok.
        public const int PerUploadChunkSize = 1024 * 1024; //1MB


        public List<UploadFileChunkResponse> Call()
        {

            NameValueCollection nvc = new NameValueCollection
            {
                {"session_id", SessionId},
                {"file_id", FileId},
                {"temp_location", TempLocation},
                {"chunk_size","0" },
                {"chunk_offset","0" }
            };

            List<UploadFileChunkResponse> responses = new List<UploadFileChunkResponse>();
            FileInfo info = new FileInfo(FullFilePath);
            long remainingFileBytes = info.Length;
            int offset = ChunkOffset;

            while (remainingFileBytes > 0)
            {
                nvc["chunk_offset"]= offset.ToString();

                if (remainingFileBytes > PerUploadChunkSize)
                {
                    nvc["chunk_size"]= PerUploadChunkSize.ToString();
                    var response = UploadFile(URL, FullFilePath, offset, PerUploadChunkSize, "file_data", "application/octet-stream", nvc);
                    responses.Add(JsonConvert.DeserializeObject<UploadFileChunkResponse>(response));
                    offset += PerUploadChunkSize;
                    remainingFileBytes -= PerUploadChunkSize;
                }
                else
                {
                    //TODO: Error when uploading here.
                    nvc["chunk_size"] = remainingFileBytes.ToString();
                    var response = UploadFile(URL, FullFilePath, offset, (int)remainingFileBytes, "file_data", "application/octet-stream", nvc);
                    responses.Add(JsonConvert.DeserializeObject<UploadFileChunkResponse>(response));
                    remainingFileBytes = 0;
                }

            }

            return responses;

        }

        private static string UploadFile(string url, string file, int offset, int chunksize, string paramName, string contentType,
            NameValueCollection nvc)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, Path.GetFileName(file), contentType);
            byte[] headerbytes = Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[chunksize];

            fileStream.Seek(offset, SeekOrigin.Begin);
            int bytesRead = fileStream.Read(buffer, 0, chunksize);

            rs.Write(buffer, 0, bytesRead);
            fileStream.Close();

            byte[] trailer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                return reader2.ReadToEnd();
            }
            catch (WebException ex)
            {
                var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();

                dynamic obj = JsonConvert.DeserializeObject(resp);
                var messageFromServer = obj.error.message;

                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }

            return "";
        }

        private static string HttpUploadFile(string url, string file, string paramName, string contentType,
            NameValueCollection nvc)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, Path.GetFileName(file), contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                return reader2.ReadToEnd();
            }
            catch (Exception ex)
            {
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }

            return "";
        }



    }

    public class UploadFileChunkResponse
    {

        public int TotalWritten { get; set; }

    }

}
