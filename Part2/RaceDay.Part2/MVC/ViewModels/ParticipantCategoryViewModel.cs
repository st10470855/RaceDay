namespace MVC.ViewModels;

public class ParticipantCategoryViewModel
{
    public int CategoryID { get; set; }

    public int EventID { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int? MinAge { get; set; }

    public int? MaxAge { get; set; }

    public decimal? MinDistance { get; set; }

    public decimal? MaxDistance { get; set; }

    public int SortOrder { get; set; }
}