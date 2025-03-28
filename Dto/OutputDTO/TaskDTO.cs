namespace MercurialBackendDotnet.Dto.OutputDTO;

public record TaskDTO 
(string Id, string Title, DateOnly LastUpdatedAt, DateTime? DueDate,
string SubjectId, string TopicId, string NoteId, string NoteContent
);

