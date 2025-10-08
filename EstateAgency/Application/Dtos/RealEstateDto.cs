using EstateAgency.Domain.Enums;

namespace Application.Dtos;

public class RealEstateDto
{
    public required RealEstateType Type { get; set; }

    public required RealEstatePurpose Purpose { get; set; }

    public required string CadastralNumber { get; set; }

    public required string Address { get; set; }

    public int? FloorNumber { get; set; }

    public int? Floors { get; set; }

    public required float Square { get; set; }

    public int? Rooms { get; set; }

    public double? CeilingHeight { get; set; }

    public required bool IsEncumbrance { get; set; }
    public int? Count { get; init; }
}
