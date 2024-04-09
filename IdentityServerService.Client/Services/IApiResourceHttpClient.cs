using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityServerService.Client.Services
{
    public interface IApiResourceHttpClient
    {
        Task<HttpClient> GetHttpClient();
    }
}
