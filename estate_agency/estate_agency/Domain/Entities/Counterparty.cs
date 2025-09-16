namespace EstateAgency.Domain.Entities;

internal class Counterparty
{
    public int Id { get; set; }
    public required string Fio {  get; set; }
    public required string PassportNumber { get; set; }
    public required string PhoneNumber { get; set; }

}
