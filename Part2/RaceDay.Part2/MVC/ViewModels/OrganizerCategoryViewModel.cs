using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels;

public class OrganizerCategoryViewModel
{
    public int CategoryID { get; set; }

    public int EventID { get; set; }

    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(100)]
    public string CategoryName { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public int? MinAge { get; set; }

    public int? MaxAge { get; set; }

    public decimal? MinDistance { get; set; }

    public decimal? MaxDistance { get; set; }

    public int SortOrder { get; set; }
}