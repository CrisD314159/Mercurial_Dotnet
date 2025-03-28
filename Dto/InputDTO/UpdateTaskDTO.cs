namespace MercurialBackendDotnet.Dto.InputDTO;

public record UpdateTaskDTO(string Id, string Title, string SubjectId, string TopicId, string? NoteContent);