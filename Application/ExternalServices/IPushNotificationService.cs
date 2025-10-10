using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.ExternalServices;

public interface IPushNotificationService
{
  void ScheduleNotification(ScheduleNotificationDTO scheduleNotificationDto);

  Task SendNotification(ScheduleNotificationDTO scheduleNotificationDto);
}