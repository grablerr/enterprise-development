using System.ComponentModel.DataAnnotations;

namespace EstateAgency.Contracts.Dtos;
/// <summary>
/// For creating a counterparty.
/// </summary>
public class CounterpartyCreateDto
{
    /// <summary>
    /// Full name of the counterparty.
    /// </summary>
    [Required(ErrorMessage = "FullName is required")]
    [StringLength(100, ErrorMessage = "FullName cannot be longer than 100 characters")]
    public required string FullName { get; set; }

    /// <summary>
    /// Passport number of the counterparty.
    /// </summary>
    [Required(ErrorMessage = "PassportNumber is required")]
    [StringLength(11, ErrorMessage = "PassportNumber cannot be longer than 11 characters")]
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Phone number of the counterparty.
    /// </summary>
    ///     [Required(ErrorMessage = "PhoneNumber is required")]
    [Required(ErrorMessage = "PhoneNumber is required")]
    [StringLength(16, ErrorMessage = "PhoneNumber cannot be longer than 16 characters")]
    public required string PhoneNumber { get; set; }
}