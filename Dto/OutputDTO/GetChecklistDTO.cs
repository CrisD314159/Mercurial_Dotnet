namespace MercurialBackendDotnet.Dto.OutputDTO;

public record GetChecklistDTO(long Id, List<NodeDTO> Nodes);