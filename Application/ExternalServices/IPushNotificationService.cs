using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.ExternalServices;

public interface IPushNotification
{
  void ScheduleNotification(ScheduleNotificationDTO scheduleNotificationDTO);

  Task SendNotification(ScheduleNotificationDTO scheduleNotificationDTO);
}