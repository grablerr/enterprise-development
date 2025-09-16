using EstateAgency.Domain.Enums;

namespace EstateAgency.Domain.Entities;

public class EstateObject
{
    public required int Id { get; set; } 
    public ObjectType Type { get; set; }
    public ObjectPurpose Purpose { get; set; }
    public required int CadastralNumber { get; set; }
    public required string Address { get; set; }
    public int FloorNumber { get; set; }
    public int Floors {  get; set; }
    public required float Square { get; set; }
    public int Rooms { get; set; }
    public int CeilingHeight { get; set; }
    public required bool IsEncumbrance { get; set; }

}
