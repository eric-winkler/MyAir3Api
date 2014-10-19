﻿using System.IO;
using System.Text;
using System.Xml;

namespace Winkler.MFMyAir3Api
{
    public class XElement
    {
        private string Xml { get; set; }

        public string Value
        {
            get
            {
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(Xml)))
                using (var xmlReader = XmlReader.Create(stream))
                {
                    xmlReader.MoveToContent();
                    if (!xmlReader.Read())
                        return Xml;

                    if (xmlReader.NodeType != XmlNodeType.Text)
                        return Xml;

                    return xmlReader.Value;
                }
            }
        }

        public static XElement Parse(string xml)
        {
            return new XElement { Xml = xml };
        }

        public XElement Element(string elementName)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(Xml)))
            using (var xmlReader = XmlReader.Create(stream))
            {
                xmlReader.ReadToFollowing(elementName);
                var elementString = xmlReader.ReadOuterXml();
                return elementString == null ? null : XElement.Parse(elementString);
            } 
        }
    }
}