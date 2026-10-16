using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs;

public class UpdateCategoryRequest
{
    [Required]
    [StringLength(100)]
    public string CategoryName { get; set; } = string.Empty;

    [StringLength(255)]
    public string? Description { get; set; }

    [Range(0, 120)]
    public int? MinAge { get; set; }

    [Range(0, 120)]
    public int? MaxAge { get; set; }

    [Range(typeof(decimal), "0.01", "9999.99")]
    public decimal? MinDistance { get; set; }

    [Range(typeof(decimal), "0.01", "9999.99")]
    public decimal? MaxDistance { get; set; }

    [Range(0, 10000)]
    public int SortOrder { get; set; }
}