using System.Xml;

namespace Winkler.MFMyAir3Api
{
    public static class XmlReaderEx
    {
        public static string ReadOuterXml(this XmlReader reader)
        {
            var startDepth = reader.Depth;
            var accumulated = "";

            do
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        accumulated += "<" + reader.Name + ">";
                        break;
                    case XmlNodeType.Text:
                        accumulated += reader.Value;
                        break;
                    case XmlNodeType.EndElement:
                        accumulated += "</" + reader.Name + ">";
                        break;
                }
            } while ((reader.NodeType != XmlNodeType.EndElement || reader.Depth != startDepth) && reader.Read());
            return accumulated;
        }
    }
}