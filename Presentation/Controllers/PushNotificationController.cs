
namespace MercurialBackendDotnet.Presentation.Controllers;

using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class PushNotificationController(IPushNotificacionService pushNotificacionService) : ControllerBase
{
  private readonly IPushNotificacionService _pushNotificationService = pushNotificacionService;

  [HttpPost]
  public IActionResult ScheduleNotification(ScheduleNotificationDTO scheduleNotificationDTO)
  {
    _pushNotificationService.ScheduleNotification(scheduleNotificationDTO);
    return Ok();
  }
}