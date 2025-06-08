namespace MercurialBackendDotnet.Dto.InputDTO;

public record ScheduleNotificationDTO(string Token, string Title, string Message, string Link, DateTime DueDate );