using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.SubjectCases.UseCasesInterfaces;

public interface IDeleteSubjectUseCase
{
  Task Execute(string userid, long subjectId);
}
