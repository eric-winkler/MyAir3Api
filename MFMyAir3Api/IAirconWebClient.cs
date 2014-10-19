namespace Winkler.MFMyAir3Api
{
    public interface IAirconWebClient
    {
        AirconWebResponse Get(string requestUri);
    }
}