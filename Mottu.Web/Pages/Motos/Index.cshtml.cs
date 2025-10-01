using Microsoft.AspNetCore.Mvc.RazorPages;
using Mottu.Web.Models;
using System.Net.Http.Json;

namespace Mottu.Web.Pages.Motos
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public List<Moto> Motos { get; set; } = new();

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("MottuApi");
            var response = await client.GetFromJsonAsync<List<Moto>>("moto");
            if (response != null)
                Motos = response;
        }
    }
}
