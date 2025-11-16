using System.ComponentModel.DataAnnotations;

namespace EstateAgency.Contracts.Dtos;

/// <summary>
/// Data Transfer Object representing a real estate entity with detailed attributes.
/// </summary>
public class RealEstateCreateDto
{
    /// <summary>
    /// Type of the real estate (e.g., apartment, house).
    /// </summary>
    [Required(ErrorMessage = "Type is required")]
    public required string Type { get; set; }

    /// <summary>
    /// Purpose of the real estate (e.g., residential, commercial).
    /// </summary>
    [Required(ErrorMessage = "Purpose is required")]
    public required string Purpose { get; set; }

    /// <summary>
    /// Official cadastral number for identification.
    /// </summary>
    [Required(ErrorMessage = "CadastralNumber is required")]
    [StringLength(64, ErrorMessage = "CadastralNumber cannot be longer than 64 characters")]
    public required string CadastralNumber { get; set; }

    /// <summary>
    /// Physical address of the real estate.
    /// </summary>
    [Required(ErrorMessage = "Address is required")]
    [StringLength(256, ErrorMessage = "Address cannot be longer than 256 characters")]
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
    [Required(ErrorMessage = "Square is required")]
    public float Square { get; set; }

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
    public bool IsEncumbrance { get; set; }
}