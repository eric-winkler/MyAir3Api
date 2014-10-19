using System;
using System.IO;
using System.Net;

namespace Winkler.MFMyAir3Api
{
    internal class AirconWebClient : IAirconWebClient
    {
        private Uri BaseAddress { get; set; }

        public AirconWebClient(Uri baseAddress)
        {
            BaseAddress = baseAddress;
        }

        public AirconWebResponse Get(string requestUri)
        {
            var uri = BaseAddress.AbsoluteUri + requestUri;

            using (var request = HttpWebRequest.Create(uri))
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                return AirconWebResponse.Parse(reader.ReadToEnd());
            }
        }
    }
}