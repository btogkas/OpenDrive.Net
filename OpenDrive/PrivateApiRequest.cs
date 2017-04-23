using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenDrive
{
    public class PrivateApiRequest
    {

        [JsonProperty(PropertyName = "session_id")]
        public string SessionId { get; set; }

    }
}
