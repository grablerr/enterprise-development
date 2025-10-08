using EstateAgency.Domain.Enums;

namespace Application.Dtos;
internal class ApplicationDto
{
    public required int CounterpartyId { get; set; }

    public required int RealEstateId { get; set; }

    public required decimal TransactionAmount { get; set; }

    public required ApplicationType Type { get; set; }

    public required DateTime Date { get; set; }
}
