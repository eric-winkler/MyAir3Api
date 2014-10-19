using System.Xml.Linq;

namespace Winkler.MyAir3Api
{
    public class AirconWebResponse
    {
        public XElement InnerResponse { get; private set; }

        public bool IsAuthenticated
        {
            get
            {
                return InnerResponse != null
                    && InnerResponse.Element("authenticated") != null
                    && InnerResponse.Element("authenticated").Value == "1";
            }
        }

        private AirconWebResponse()  { }

        public static AirconWebResponse Parse(string text)
        {
            return new AirconWebResponse { InnerResponse = XElement.Parse(text) };
        }
    }
}