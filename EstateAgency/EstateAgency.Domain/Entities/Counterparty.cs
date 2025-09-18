namespace EstateAgency.Domain.Entities;

/// <summary>
/// Represents a counterparty in the real estate agency system.
/// Stores personal identification and contact information for individuals involved in transactions.
/// </summary>
public class Counterparty
{
    /// <summary>
    /// Unique identifier for the counterparty
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Full legal name of the counterparty
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Official passport or identification document number
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Primary contact phone number for communication
    /// </summary>
    public required string PhoneNumber { get; set; }
}