using Hangfire;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Services.Interfaces;

namespace MercurialBackendDotnet.Services.Implementations;

public class PushNotificacionService(IHttpClientFactory httpClientFactory) : IPushNotificacionService
{
  private readonly IHttpClientFactory _httpClient = httpClientFactory;

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

    await client.PostAsJsonAsync("https://localhost:3000/send-notification", payload);
  }
}