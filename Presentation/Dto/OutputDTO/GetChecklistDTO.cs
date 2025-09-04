namespace MercurialBackendDotnet.Presentation.Dto.OutputDTO;

public record GetChecklistDTO(long Id, List<NodeDTO> Nodes);