namespace MercurialBackendDotnet.Dto.OutputDTO;

public record GetChecklistDTO(string Id, List<NodeDTO> Nodes);