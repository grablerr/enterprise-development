namespace EstateAgency.Application.Dtos;

/// <summary>
/// Data transfer object representing the count of requests by real estate typeю
/// </summary>
public class RealEstateTypeRequestCountDto
{
    /// <summary>
    /// The type of real estate as a string
    /// </summary>
    public required string RealEstateType { get; set; }

    /// <summary>
    /// The count of requests for this real estate type.
    /// </summary>
    public required int Count { get; set; }
}