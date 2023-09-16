using System.Text;
using System.Text.Json;
using PlatformService.DTOs;

namespace PlatformService.SyncDataServices.Http{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient client, IConfiguration configuration)
        {
            _httpClient = client;

            _configuration = configuration;
        }
        public async Task SendPlatFormToCommand(PlatformReadDto plat)
        {
             var httpContent = new StringContent(
                JsonSerializer.Serialize(plat),
                Encoding.UTF8,
                "application/json"
             );

             var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);

             Console.WriteLine($"{_configuration["CommandService"]}");

             if(response.IsSuccessStatusCode)
             {
                Console.WriteLine("----> sYNC pOST TO COMMAND SERVICE WAS OK");
             }
             else
             {
                 Console.WriteLine("----> sYNC pOST TO COMMAND SERVICE WAS not OK");
             }
        }
    }
}