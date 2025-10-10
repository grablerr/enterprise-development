using EstateAgency.Domain.Enums;

namespace Application.Dtos;

/// <summary>
/// Data Transfer Object representing a real estate entity with detailed attributes.
/// </summary>
public class RealEstateDto
{
    /// <summary>
    /// Type of the real estate (e.g., apartment, house).
    /// </summary>
    public required RealEstateType Type { get; set; }

    /// <summary>
    /// Purpose of the real estate (e.g., residential, commercial).
    /// </summary>
    public required RealEstatePurpose Purpose { get; set; }

    /// <summary>
    /// Official cadastral number for identification.
    /// </summary>
    public required string CadastralNumber { get; set; }

    /// <summary>
    /// Physical address of the real estate.
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Floor number where the real estate is located, if applicable.
    /// </summary>
    public int? FloorNumber { get; set; }

    /// <summary>
    /// Total number of floors in the building, if applicable.
    /// </summary>
    public int? Floors { get; set; }

    /// <summary>
    /// Total area in square meters.
    /// </summary>
    public required float Square { get; set; }

    /// <summary>
    /// Number of rooms, if applicable.
    /// </summary>
    public int? Rooms { get; set; }

    /// <summary>
    /// Height of the ceilings in meters, if applicable.
    /// </summary>
    public double? CeilingHeight { get; set; }

    /// <summary>
    /// Indicates if there are any legal encumbrances affecting the property.
    /// </summary>
    public required bool IsEncumbrance { get; set; }

    /// <summary>
    /// Optional count of related requests or transactions.
    /// </summary>
    public int? Count { get; init; }
}