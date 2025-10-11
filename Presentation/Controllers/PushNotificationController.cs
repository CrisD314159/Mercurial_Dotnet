
namespace MercurialBackendDotnet.Presentation.Controllers;

using MercurialBackendDotnet.Application.ExternalServices;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class PushNotificationController(IPushNotificationService pushNotificacionService) : ControllerBase
{
  private readonly IPushNotificationService _pushNotificationService = pushNotificacionService;

  [HttpPost]
  public IActionResult ScheduleNotification(ScheduleNotificationDTO scheduleNotificationDTO)
  {
    _pushNotificationService.ScheduleNotification(scheduleNotificationDTO);
    return Ok();
  }
}