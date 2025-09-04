

using MercurialBackendDotnet.Domain.Enums;

namespace MercurialBackendDotnet.Presentation.Dto.OutputDTO;

public record AssignmentDTO
(Guid Id, string Title, DateOnly LastUpdatedAt, DateTime? DueDate, AssignmentState State,
long SubjectId, string SubjectTitle, long TopicId, string TopicTitle, string TopicColor,
long NoteId, string NoteContent
);

