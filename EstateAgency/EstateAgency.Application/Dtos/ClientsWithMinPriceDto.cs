namespace EstateAgency.Application.Dtos;
/// <summary>
/// Data Transfer Object used for returning counterparties along with their minimum price requests.
/// Contains the minimum price value and a list of counterparties with such requests.
/// </summary>
public class ClientsWithMinPriceDto
{
    /// <summary>
    /// The minimum price value among counterparties requests.
    /// </summary>
    public required decimal MinPrice { get; set; }

    /// <summary>
    /// A list of counterparties who have requests with the minimum price.
    /// </summary>
    public required List<CounterpartyDto> Counterparties { get; set; }
}
