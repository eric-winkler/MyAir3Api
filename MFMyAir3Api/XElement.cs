using System.Collections;

namespace Winkler.MFMyAir3Api
{
    public class XElement
    {
        private string Xml { get; set; }

        public string Value
        {
            get
            {
                var xmlReader = new SimpleXmlReader(Xml);
                xmlReader.MoveToElement();
                return xmlReader.Value;
            }
        }

        public string Name
        {
            get
            {
                var xmlReader = new SimpleXmlReader(Xml);
                xmlReader.MoveToElement();
                return xmlReader.Name;
            }
        }

        public static XElement Parse(string xml)
        {
            return new XElement { Xml = xml };
        }

        public XElement Element(string elementName)
        {
            var xmlReader = new SimpleXmlReader(Xml);
            xmlReader.MoveToElement(elementName);
            return XElement.Parse(xmlReader.ReadOuterXml());
        }

        public XElement[] Elements()
        {
            var elements = new ArrayList();

            var xmlReader = new SimpleXmlReader(Xml);
            xmlReader.MoveToElement();

            while (xmlReader.MoveToNextElement())
            {
                elements.Add(XElement.Parse(xmlReader.ReadOuterXml()));
            }

            return (XElement[]) elements.ToArray(typeof (XElement));
        }
    }
}