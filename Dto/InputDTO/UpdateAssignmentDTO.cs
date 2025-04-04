namespace MercurialBackendDotnet.Dto.InputDTO;

public record UpdateAssignmentDTO(Guid AssignmentId, string Title, long SubjectId, long TopicId, string? NoteContent, DateTime? DueDate);