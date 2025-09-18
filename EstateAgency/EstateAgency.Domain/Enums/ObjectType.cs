namespace EstateAgency.Domain.Enums;

/// <summary>
/// Defines the specific architectural and structural types of real estate properties.
/// </summary>
public enum ObjectType
{
    /// <summary>
    /// Self-contained residential unit within a larger multi-story building
    /// </summary>
    Apartment,

    /// <summary>
    /// Detached residential building designed for single-family occupancy
    /// </summary>
    House,

    /// <summary>
    /// Small recreational or seasonal dwelling, often in rural or vacation areas
    /// </summary>
    Cottage,

    /// <summary>
    /// Commercial space designed for professional, administrative, or business activities
    /// </summary>
    Office,

    /// <summary>
    /// Multi-story residential unit sharing walls with adjacent properties
    /// </summary>
    Townhouse,

    /// <summary>
    /// Retail space designed for commercial sales and customer-facing activities
    /// </summary>
    Shop,

    /// <summary>
    /// Large storage facility for goods, inventory, or industrial materials
    /// </summary>
    Warehouse,

    /// <summary>
    /// Enclosed structure designed for vehicle storage and protection
    /// </summary>
    Garage
}