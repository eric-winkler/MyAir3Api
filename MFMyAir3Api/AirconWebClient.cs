using System;
using System.Net;

namespace Winkler.MFMyAir3Api
{
    internal class AirconWebClient : IAirconWebClient
    {
        private readonly int _timeout;
        private Uri BaseAddress { get; set; }

        public AirconWebClient(Uri baseAddress, int timeout)
        {
            _timeout = timeout;
            BaseAddress = baseAddress;
        }

        public AirconWebResponse Get(string requestUri)
        {
            var uri = BaseAddress.AbsoluteUri + requestUri;

            using (var request = HttpWebRequest.Create(uri))
            {
                request.Timeout = _timeout;
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {

                    var responseContent = stream.ReadToEnd();
                    return AirconWebResponse.Parse(responseContent);
                }
            }
        }
    }
}