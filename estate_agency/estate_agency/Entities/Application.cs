namespace EstateAgency.Domain.Entities;

public class Application
{
    public int Id { get; set; }
    public required int AgentId { get; set; }
    public Counterparty? AgentInfo { get; set; }
    public required int ObjectId { get; set; }
    public EstateObject? ObjectInfo { get; set; }
    public required decimal TransactionAmount { get; set; }
    public required Application Type { get; set; }
    public DateTime? Date { get; set; }
}
