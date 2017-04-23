using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.Upload
{
    public class CloseUploadRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/upload/close_file_upload.json";


        [JsonProperty(PropertyName = "file_id")]
        public string FileId
        {
            get; set;
        }

        [JsonProperty(PropertyName = "file_size")]
        public long FileSize //FileSize in bytes
        {
            get; set;
        }

        [JsonProperty(PropertyName = "temp_location")]
        public string TempLocation
        {
            get; set;
        }


        [JsonProperty(PropertyName = "file_time")]
        public int FileTime //FileSize in bytes
        {
            get; set;
        }

        [JsonProperty(PropertyName = "access_folder_id")]
        public string AccessFolderId
        {
            get; set;
        }

        [JsonProperty(PropertyName = "file_compressed")]
        public int FileCompressed 
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


        // : string - MD5 File Hash (Optional).  : string - .

        public CloseUploadResponse Call()
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
                return JsonConvert.DeserializeObject<CloseUploadResponse>(result);


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
                                throw new Exception("Invalid file Id");
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

        }

    }

    public class CloseUploadResponse
    {

        public string FileId { get; set; }// string - Name{get;set;}// string - GroupID{get;set;}// string -
        public string Extension { get; set; }// string -
        public long Size { get; set; }// integer -
        public string Views { get; set; }// string -
        public string Version { get; set; }// string -
        public string Downloads { get; set; }// string -
        public string DateTrashed { get; set; }// string -
        public string DateModified { get; set; }// string -
        public string Access { get; set; }// string -
        public string OwnerSuspended { get; set; }// string -
        public string AccType { get; set; }// string -
        public string FileHash { get; set; }// string -
        public string Link { get; set; }// string - string -
        public string DownloadLink { get; set; }// string -
        public string StreamingLink { get; set; }// string -
        public string OwnerName { get; set; }// string -

        [JsonProperty(PropertyName = "upload_speed_limit")]
        public long UploadSpeedLimit { get; set; }// integer -

        [JsonProperty(PropertyName = "download_speed_limit")]
        public long DownloadSpeedLimit { get; set; }// integer -

        public long BWExceeded { get; set; }// integer -

        public string ThumbLink { get; set; }// string -
        public int Encrypted { get; set; }// integer -
        public string Password { get; set; }// string -
        public string OwnerLevel { get; set; }// string -
        public int EditOnline { get; set; }// integer -
        public string ID { get; set; }// string -
        public string FolderID { get; set; }// string -
        public string Description { get; set; }// string -
        public string IsArchive { get; set; }// string -
        public string Category { get; set; }// string -
        public long Date { get; set; }// string -
        public long DateUploaded { get; set; }// integer -
        public string DateAccessed { get; set; }// string -
        public string DirectLinkPublic { get; set; }//string -
        public string EmbedLink { get; set; }// string - string -
        public int AccessDisabled { get; set; }
        public string Type { get; set; }// string -
        public string DestURL { get; set; }// string -
        public string Owner { get; set; }// string -
        public string AccessUser { get; set; }// string -
        public int DirUpdateTime { get; set; }// integer -
        public string FileName { get; set; }// string -
        public string FileDate { get; set; }// string -
        public string FileDescription { get; set; }// string -
        public string FileDestUrl { get; set; }// string -
        public string FileKey { get; set; }// string -
        public string FilePrice { get; set; }// string -
        public string FileVersion { get; set; }// string -
        public string FileIp { get; set; }// string -
        public string FileIsPublic { get; set; }// string -
        public string Datetime { get; set; }// string - string -

    }

}
