namespace EstateAgency.Domain.Enums;

/// <summary>
/// Defines the types of real estate transaction applications supported by the system.
/// Represents the primary operation direction for property transactions.
/// </summary>
public enum ApplicationType
{
    /// <summary>
    /// Represents a purchase application where a client seeks to acquire a property
    /// </summary>
    Buy,

    /// <summary>
    /// Represents a sale application where a client offers a property for acquisition
    /// </summary>
    Sell
}