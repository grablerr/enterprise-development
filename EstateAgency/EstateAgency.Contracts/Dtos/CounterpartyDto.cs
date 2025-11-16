namespace EstateAgency.Contracts.Dtos;

/// <summary>
/// Data Transfer Object representing a counterparty.
/// </summary>
public class CounterpartyDto
{
    /// <summary>
    /// Unique identifier for the counterparty
    /// </summary>
    public int Id { get; set; }

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
}