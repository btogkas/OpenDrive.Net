using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.File
{
    public class EditFileSettingRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/file/filesettings.json";

        [JsonProperty(PropertyName = "file_id")]
        public string FileId { get; set; }

        [JsonProperty(PropertyName = "file_price")]
        public string FilePrice { get; set; }

        [JsonProperty(PropertyName = "file_name")]
        public string FileName { get; set; }

        [JsonProperty(PropertyName = "file_description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "file_password")]
        public string Password { get; set; }
        
        [JsonProperty(PropertyName = "file_dest_url")]
        public string FileDestinationURL { get; set; }

        //File public or no private = 0,public = 1,hidden – 2;
        [JsonProperty(PropertyName = "file_ispublic")]
        public int FileIsPublic{ get; set; }

        [JsonProperty(PropertyName = "file_edit_online")]
        public int FileEditOnline { get; set; }

        [JsonProperty(PropertyName = "file_modification_time")]
        public int FileModificationTime { get; set; }

        [JsonProperty(PropertyName = "sharing_id")]
        public string SharingId { get; set; }

        public bool Call()
        {

            var request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "PUT";
            request.ContentType = "application/xml";

            Stream dataStream = request.GetRequestStream();
            Serialize(dataStream, this);
            request.ContentLength = dataStream.Length;
            dataStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return true;
        }

        public void Serialize(Stream output, object input)
        {
            var ser = new DataContractSerializer(input.GetType());
            ser.WriteObject(output, input);
        }

    }
}
