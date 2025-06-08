using MercurialBackendDotnet.Dto.InputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface IPushNotificacionService
{
  void ScheduleNotification(ScheduleNotificationDTO scheduleNotificationDTO);

  Task SendNotification(ScheduleNotificationDTO scheduleNotificationDTO);
}