using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.SubjectCases.UseCasesInterfaces;

public interface IUpdateSubjectUseCase
{
  Task Execute(string userId, UpdateSubjectDTO updateSubjectDTO);
}
