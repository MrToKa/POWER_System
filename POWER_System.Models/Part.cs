using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using POWER_System.Models;
using POWER_System.Models.Constants;
using POWER_System.Models.Enum;

public class Part
{
    public Part()
    {
        this.Enclosure = new HashSet<Enclosure>();
        this.Storage = new HashSet<Storage>();
    }

    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the manufacturer name of the part.
    /// </summary>
    [Required]
    [MaxLength(PartConstants.ManufacturerMaxLength)]
    public string Manufacturer { get; set; }

    /// <summary>
    /// Gets or sets the description of the part. It can include nominal voltage and current and so on.
    /// </summary>
    [Required]
    [MaxLength(PartConstants.DescriptionMaxLength)]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the manufacturers order number.
    /// </summary>
    [Required]
    public string OrderNumber { get; set; }

    /// <summary>
    /// Gets or sets the measurement unit of the part.
    /// </summary>
    [Required]
    public string Measure { get; set; }

    /// <summary>
    /// Gets or sets the required or on stock quantity of the part.
    /// </summary>
    [Required]
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the price of the part. It can be used for calculating the price of the enclosure or the order.
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    /// Gets or sets the scope of the delivery of the part.
    /// </summary>
    [Required]
    public OrderDelivery Delivery { get; set; }

    public virtual IEnumerable<Enclosure> Enclosure { get; set; }

    public virtual IEnumerable<Storage> Storage { get; set; }
}
