namespace MercurialBackendDotnet.Presentation.Dto.InputDTO;


public record CreateAssignmentDTO(string Title, long SubjectId, long TopicId, string? NoteContent, DateTime DueDate);