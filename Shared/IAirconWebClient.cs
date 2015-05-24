using System.Threading.Tasks;

namespace Winkler.MyAir3Api
{
    public interface IAirconWebClient
    {
        Task<AirconWebResponse> GetAsync(string requestUri);
    }
}