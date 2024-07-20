using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess;

public partial class OilPaintingArt
{
    public int OilPaintingArtId { get; set; }

    [Required]
    [MinLength(5)]
    public string ArtTitle { get; set; } = null!;

    [Required]
    public string? OilPaintingArtLocation { get; set; }

    [Required]
    public string? OilPaintingArtStyle { get; set; }

    [Required]
    public string? Artist { get; set; }

    [Required]
    public string? NotablFeatures { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public decimal? PriceOfOilPaintingArt { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int? StoreQuantity { get; set; }

    public DateTime? CreatedDate { get; set; }
    [Required]
    public string? SupplierId { get; set; }

    public virtual SupplierCompany? Supplier { get; set; }
}
