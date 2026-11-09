using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels;

public class CreateOrganizerCategoryViewModel
{
    public int EventID { get; set; }

    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(100)]
    public string CategoryName { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(0, 120)]
    public int? MinAge { get; set; }

    [Range(0, 120)]
    public int? MaxAge { get; set; }

    [Range(0, 500)]
    public decimal? MinDistance { get; set; }

    [Range(0, 500)]
    public decimal? MaxDistance { get; set; }

    public int SortOrder { get; set; }
}