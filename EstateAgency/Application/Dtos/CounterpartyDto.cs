namespace Application.Dtos;

/// <summary>
/// Data Transfer Object representing a counterparty.
/// </summary>
public class CounterpartyDto
{
    /// <summary>
    /// Full name of the counterparty.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Passport number of the counterparty.
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Phone number of the counterparty.
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Optional count of requests associated with the counterparty.
    /// </summary>
    public int? RequestCount { get; set; }
}