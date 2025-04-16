using MercurialBackendDotnet.Model.Enums;

namespace MercurialBackendDotnet.Dto.OutputDTO;

public record AssignmentDTO 
(Guid Id, string Title, DateOnly LastUpdatedAt, DateTime? DueDate, AssignmentState State,
long SubjectId, long TopicId, long NoteId, string NoteContent
);

