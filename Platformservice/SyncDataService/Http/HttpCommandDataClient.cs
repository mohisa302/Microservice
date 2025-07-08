using System.Net.Http;
using System.Text;
using System.Text.Json;
using PlatformService.Dtos; 
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace PlatformService.SyncDataServices.Http
{
  public class HttpCommandDataClient : ICommandDataClient
  {
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpCommandDataClient(
      HttpClient httpClient,
      IConfiguration configuration
      )
    {
      _httpClient = httpClient;
      _configuration = configuration;
    }
    public async Task SendPlatformToCommand(PlatformReadDto plat)
    {
      var httpContent = new StringContent(
        JsonSerializer.Serialize(plat), //convert json object to string to send
        Encoding.UTF8,
        "application/json");
      var response = await _httpClient.PostAsync(_configuration["CommandService"], httpContent);
      if (response.IsSuccessStatusCode)
      {
        Console.WriteLine("--> sync post to command service");
      }
      else
      {
        Console.WriteLine("--> not sync post to command service");

      }
    }
  }
}
