namespace MercurialBackendDotnet.Dto.InputDTO;


public record CreateTaskDTO(string Title, string SubjectId, string TopicId, string? NoteConted);