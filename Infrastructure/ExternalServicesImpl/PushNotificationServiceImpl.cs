using Hangfire;
using MercurialBackendDotnet.Application.ExternalServices;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Infrastructure.ExternalServicesImpl;

public class PushNotificationServiceImpl(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IPushNotificationService
{
    private readonly IHttpClientFactory _httpClient = httpClientFactory;
    private readonly IConfiguration _configuration = configuration;

    public void ScheduleNotification(ScheduleNotificationDTO scheduleNotificationDto)
    {
      BackgroundJob.Schedule(() =>
        SendNotification(scheduleNotificationDto),
        scheduleNotificationDto.DueDate - DateTime.UtcNow
      );
    }

    public async Task SendNotification(ScheduleNotificationDTO scheduleNotificationDto)
    {
      var client = _httpClient.CreateClient();

      var payload = new
      {
        scheduleNotificationDto.Token,
        scheduleNotificationDto.Title,
        scheduleNotificationDto.Message,
        scheduleNotificationDto.Link,
      };

      var apiUrl = $"{_configuration["Api:Url"]}/send-notification";

      await client.PostAsJsonAsync(apiUrl, payload);
    }
}

