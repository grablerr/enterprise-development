using System.ComponentModel.DataAnnotations;

namespace EstateAgency.Application.Dtos;
/// <summary>
/// For creating a real estate transaction application.
/// </summary>
public class ApplicationCreateDto
{
    /// <summary>
    /// Identifier of the counterparty involved in the application.
    /// </summary>
    [Required(ErrorMessage = "CounterpartyId is required")]
    public required int CounterpartyId { get; set; }

    /// <summary>
    /// Identifier of the real estate involved in the application.
    /// </summary>
    [Required(ErrorMessage = "RealEstateId is required")]
    public required int RealEstateId { get; set; }

    /// <summary>
    /// The amount of the financial transaction.
    /// </summary>
    [Required(ErrorMessage = "Transaction amount is required")]
    public required decimal TransactionAmount { get; set; }

    /// <summary>
    /// Type of the application, specifying the transaction nature (e.g., Buy or Sell).
    /// </summary>
    [Required(ErrorMessage = "Application type is required")]
    public required string Type { get; set; }

    /// <summary>
    /// Date of the application or transaction.
    /// </summary>
    [Required(ErrorMessage = "Date is required")]
    public required DateTime Date { get; set; }
}