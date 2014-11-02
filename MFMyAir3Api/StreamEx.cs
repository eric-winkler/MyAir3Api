using System.IO;
using System.Text;

namespace Winkler.MFMyAir3Api
{
    internal static class StreamEx
    {
        public static string ReadToEnd(this Stream stream)
        {
            // stream reader works fine in the emulator, but not on a netduino plus 2
            var buffer = new byte[512];
            var sb = new StringBuilder(512);

            int bytesRead;
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                sb.Append(Encoding.UTF8.GetChars(buffer, 0, bytesRead));
            }

            return sb.ToString();
        }
    }
}