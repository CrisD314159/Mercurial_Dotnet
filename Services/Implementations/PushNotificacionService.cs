using Hangfire;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Services.Interfaces;

namespace MercurialBackendDotnet.Services.Implementations;

public class PushNotificacionService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IPushNotificacionService
{
  private readonly IHttpClientFactory _httpClient = httpClientFactory;
  private readonly IConfiguration _configuration = configuration;

  public void ScheduleNotification(ScheduleNotificationDTO scheduleNotificationDTO)
  {
    BackgroundJob.Schedule(() =>
      SendNotification(scheduleNotificationDTO),
      scheduleNotificationDTO.DueDate - DateTime.UtcNow
    );
  }

  public async Task SendNotification(ScheduleNotificationDTO scheduleNotificationDTO)
  {
    var client = _httpClient.CreateClient();

    var payload = new
    {
      scheduleNotificationDTO.Token,
      scheduleNotificationDTO.Title,
      scheduleNotificationDTO.Message,
      scheduleNotificationDTO.Link,
    };

    var apiURL = $"{_configuration["Api:Url"]}/send-notification";

    await client.PostAsJsonAsync(apiURL, payload);
  }
}