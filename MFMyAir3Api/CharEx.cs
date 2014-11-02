namespace Winkler.MFMyAir3Api
{
    static class CharEx
    {
        public static bool IsLetter(this char c)
        {
            return (c >= 'A' && c <= 'Z')
                   || (c >= 'a' && c <= 'z');
        }
    }
}