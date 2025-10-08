namespace Application.Dtos;

public class CounterpartyDto
{
    public required string FullName { get; set; }

    public required string PassportNumber { get; set; }

    public required string PhoneNumber { get; set; }
    public int? RequestCount { get; set; }
}
