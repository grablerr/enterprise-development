/// <summary>
/// Provides statistics summary on RabbitMQ message publishing rates for each domain entity type.
/// Contains the timestamp of the summary, and the count of messages published for counterparties, real estate, and applications.
/// </summary>
public class StatsSummaryDto
{
    /// <summary>
    /// The UTC timestamp when this statistics summary was generated.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// The number of messages for new counterparties sent during the statistics interval.
    /// </summary>
    public int CounterpartyMessagesSent { get; set; }

    /// <summary>
    /// The number of real estate messages sent during the statistics interval.
    /// </summary>
    public int RealEstateMessagesSent { get; set; }

    /// <summary>
    /// The number of application messages sent during the statistics interval.
    /// </summary>
    public int ApplicationMessagesSent { get; set; }
}