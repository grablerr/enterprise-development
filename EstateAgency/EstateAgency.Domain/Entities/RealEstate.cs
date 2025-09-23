using EstateAgency.Domain.Enums;

namespace EstateAgency.Domain.Entities;

/// <summary>
/// Represents a real estate property object in the agency system.
/// Contains comprehensive physical and legal characteristics of a property for listing and transaction purposes.
/// </summary>
public class RealEstate
{
    /// <summary>
    /// Unique identifier for the real estate object
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Classification type of the property
    /// </summary>
    public required ObjectType Type { get; set; }

    /// <summary>
    /// Intended usage purpose of the property
    /// </summary>
    public required ObjectPurpose Purpose { get; set; }

    /// <summary>
    /// Official cadastral registration number from the state registry
    /// </summary>
    public required string CadastralNumber { get; set; }

    /// <summary>
    /// Physical location address of the property
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Current floor number where the property is located
    /// </summary>
    public int? FloorNumber { get; set; }

    /// <summary>
    /// Total number of floors in the building
    /// </summary>
    public int? Floors { get; set; }

    /// <summary>
    /// Total area of the property in square meters
    /// </summary>
    public required float Square { get; set; }

    /// <summary>
    /// Number of rooms in the property
    /// </summary>
    public int? Rooms { get; set; }

    /// <summary>
    /// Height of ceilings in centimeters
    /// </summary>
    public double? CeilingHeight { get; set; }

    /// <summary>
    /// Indicates if the property has any legal encumbrances, liens, or restrictions
    /// </summary>
    public required bool IsEncumbrance { get; set; }
}