using System.Threading.Tasks;

namespace Winkler.MyAir3Api
{
    internal interface IUdpIdentifier
    {
        Task<string> IdentifyAirconAsync();
    }
}