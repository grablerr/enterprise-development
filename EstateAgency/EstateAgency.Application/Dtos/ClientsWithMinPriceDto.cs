namespace EstateAgency.Application.Dtos;
public class ClientsWithMinPriceDto
{
    public required decimal MinPrice { get; set; }
    public required List<CounterpartyDto> Counterparties { get; set; }
}
