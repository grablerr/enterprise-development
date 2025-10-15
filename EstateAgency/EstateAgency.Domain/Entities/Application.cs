using EstateAgency.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EstateAgency.Domain.Entities;

/// <summary>
/// Represents a real estate transaction application in the agency system.
/// Contains information about the property, counterparty, transaction details, and application metadata.
/// </summary>
public class Application
{
    /// <summary>
    /// Unique identifier for the application
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Foreign key reference to the counterparty (agent) associated with this application
    /// </summary>
    [Required(ErrorMessage = "CounterpartyId is required")]
    public required int CounterpartyId { get; set; }

    /// <summary>
    /// Foreign key reference to the real estate object involved in the transaction
    /// </summary>
    [Required(ErrorMessage = "RealEstateId is required")]
    public required int RealEstateId { get; set; }

    /// <summary>
    /// Monetary amount of the transaction in the application
    /// </summary>
    [Required(ErrorMessage = "Transaction amount is required")]
    public required decimal TransactionAmount { get; set; }

    /// <summary>
    /// Type of the application (Buy or Sell operation)
    /// </summary>
    [Required(ErrorMessage = "Application type is required")]
    public required ApplicationType Type { get; set; }

    /// <summary>
    /// Date when the application was created or submitted
    /// </summary>
    [Required(ErrorMessage = "Date is required")]
    public required DateTime Date { get; set; }
}