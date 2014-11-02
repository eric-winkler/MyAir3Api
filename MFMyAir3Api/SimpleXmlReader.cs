using System;

namespace Winkler.MFMyAir3Api
{
    class SimpleXmlReader
    {
        private readonly string _xml;
        private int _index;

        public SimpleXmlReader(string xml)
        {
            _xml = xml;
        }

        public string Name
        {
            get
            {
                var nameStart = _index + 1;
                var nameEnd = NextIndexOf(' ', '>');
                return _xml.Substring(nameStart, nameEnd - nameStart);
            }
        }

        public string Value
        {
            get
            {
                var valueStart = NextIndexOf('>') + 1;
                var valueEnd = _xml.IndexOf('<', valueStart);
                return _xml.Substring(valueStart, valueEnd - valueStart);
            }
        }

        public string ReadOuterXml()
        {
            // will not work for recursively nested structure
            var endElementName = "</" + Name + ">";
            var start = _index;
            var end = _xml.IndexOf(endElementName, _index) + endElementName.Length;
            return _xml.Substring(start, end - start);
        }

        public bool MoveToNextElement()
        {
            return MoveToElement(_index);
        }

        public void MoveToElement()
        {
            MoveToElement(_index - 1);
        }

        public void MoveToElement(string elementName)
        {
            while (Name != elementName)
            {
                _index++;
                MoveToElement();
            }
        }

        private bool MoveToElement(int startIndex)
        {
            var nextIndex = startIndex;

            do
            {
                nextIndex++;
                nextIndex = _xml.IndexOf('<', nextIndex);
            }
            while (nextIndex + 1 < _xml.Length && !_xml[nextIndex + 1].IsLetter());

            _index = nextIndex;
            return _index > startIndex;
        }

        private int NextIndexOf(params char[] endChars)
        {
            var closest = _xml.Length - 1;

            foreach (var c in endChars)
            {
                var charLocation = _xml.IndexOf(c, _index);
                if (charLocation >= 0)
                    closest = Math.Min(closest, charLocation);
            }

            return closest;
        }
    }
}