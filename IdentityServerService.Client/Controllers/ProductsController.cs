using IdentityModel.Client;
using IdentityServerService.Client.Models;
using IdentityServerService.Client.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IdentityServerService.Client.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IApiResourceHttpClient _apiResourceHttpClient;

        public ProductsController(IConfiguration configuration, IApiResourceHttpClient apiResourceHttpClient)
        {
            _configuration = configuration;
            _apiResourceHttpClient = apiResourceHttpClient;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient client = await _apiResourceHttpClient.GetHttpClient();
            List<Product> _products = new List<Product>();

            /****ClientCredentialsToken***/
            //var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5445");

            //if (disco.IsError) { /*log*/ }

            //ClientCredentialsTokenRequest clientCredentialsTokenRequest = new ClientCredentialsTokenRequest();

            //clientCredentialsTokenRequest.ClientId = _configuration["Client:ClientId"];
            //clientCredentialsTokenRequest.ClientSecret = _configuration["Client:ClientSecret"];
            //clientCredentialsTokenRequest.Address = disco.TokenEndpoint;

            //var token = await client.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

            //var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            //client.SetBearerToken(accessToken);

            var response = await client.GetAsync("https://localhost:5001/api/products/getproducts");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                _products = JsonConvert.DeserializeObject<List<Product>>(content);
            }

            return View(_products);
        }
    }
}
