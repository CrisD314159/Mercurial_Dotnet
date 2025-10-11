using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.SubjectCases.UseCasesInterfaces;

public interface IGetUserSubjectsUseCase
{
  Task<GetUserSubjectsDTO> Execute(string userId, int offset, int limit);
}
