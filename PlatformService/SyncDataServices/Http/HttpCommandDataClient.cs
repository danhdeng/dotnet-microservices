using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;

        public IConfiguration _configruation { get; }

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configruation = configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDto plat)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize<PlatformReadDto>(plat),
                Encoding.UTF8,
                "application/json"
            );
            var response = await _httpClient.PostAsync($"{_configruation["CommandService"]}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--->Sync Post to CommandServices is okay");
            }
            else
            {
                Console.WriteLine("--->Sync Post to CommandServices is failed");
            }
        }
    }

}