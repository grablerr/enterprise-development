using EstateAgency.Domain.Enums;

namespace Application.Dtos;
/// <summary>
/// Represents a real estate transaction application.
/// </summary>
internal class ApplicationDto
{
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
    public required ApplicationType Type { get; set; }

    /// <summary>
    /// Date of the application or transaction.
    /// </summary>
    public required DateTime Date { get; set; }
}