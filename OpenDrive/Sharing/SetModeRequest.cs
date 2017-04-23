using System.IO;
using System.Net;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using OpenDrive.Constants;

namespace OpenDrive.Sharing
{
    public class SetModeRequest : PrivateApiRequest
    {

        public const string URL = Common.BaseURL + "/sharing/setmode.json";

        [JsonProperty(PropertyName = "sharing_id")]
        public string SharingId { get; set; }

        [JsonProperty(PropertyName = "sharemode")]
        public string ShareMode { get; set; } //Share mode (0 - View only, 1 - Full access mode).


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
