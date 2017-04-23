using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace System.Linq
{
    public static class WebRequestExtensions
    {

        public static void AddPostVariable(this WebRequest request,object item)
        {
            var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(item));
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
        }

    }
}
