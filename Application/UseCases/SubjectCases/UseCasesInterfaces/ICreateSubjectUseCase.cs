using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.SubjectCases.UseCasesInterfaces;

public interface ICreateSubjectUseCase
{
  Task Execute(string userId, CreateSubjectDTO createSubjectDTO);
}