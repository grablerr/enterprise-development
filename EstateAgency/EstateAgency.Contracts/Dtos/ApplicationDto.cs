namespace EstateAgency.Contracts.Dtos;
/// <summary>
/// Represents a real estate transaction application.
/// </summary>
public class ApplicationDto
{
    /// <summary>
    /// Unique identifier for the application
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identifier of the counterparty involved in the application.
    /// </summary>
    public required int CounterpartyId { get; set; }

    /// <summary>
    /// Identifier of the real estate involved in the application.
    /// </summary>
    public required int RealEstateId { get; set; }

    /// <summary>
    /// The amount of the financial transaction.
    /// </summary>
    public required decimal TransactionAmount { get; set; }

    /// <summary>
    /// Type of the application, specifying the transaction nature (e.g., Buy or Sell).
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// Date of the application or transaction.
    /// </summary>
    public required DateTime Date { get; set; }
}