namespace MercurialBackendDotnet.Dto.InputDTO;


public record CreateAssignmentDTO(string Title, string SubjectId, string TopicId, string? NoteContent, DateTime DueDate);