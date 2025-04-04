namespace MercurialBackendDotnet.Dto.OutputDTO;

public record AssignmentDTO 
(Guid Id, string Title, DateOnly LastUpdatedAt, DateTime? DueDate,
long SubjectId, long TopicId, long NoteId, string NoteContent
);

