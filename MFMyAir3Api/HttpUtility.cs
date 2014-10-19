namespace Winkler.MFMyAir3Api
{
    public class HttpUtility
    {
        public static string UrlEncode(string url)
        {
            var encoded = "";
            foreach(char charCode in url.ToCharArray())
            {
                if (
                    charCode == 0x2d                        // -
                    || charCode == 0x5f                     // _
                    || charCode == 0x2e                     // .
                    || charCode == 0x7e                     // ~
                    || (charCode > 0x2f && charCode < 0x3a) // 0-9
                    || (charCode > 0x40 && charCode < 0x5b) // A-Z
                    || (charCode > 0x60 && charCode < 0x7b) // a-z
                    )
                {
                    encoded += charCode;
                }
                else
                {
                    encoded += "%" + ((byte)charCode).ToString("X");
                }
            }

            return encoded;
        }
    }
}