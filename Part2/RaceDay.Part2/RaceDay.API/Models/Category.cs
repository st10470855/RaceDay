namespace RaceDay.API.Models;

public class Category
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

    public Event Event { get; set; } = null!;

    public ICollection<EventEnrolment> EventEnrolments { get; set; }
        = new List<EventEnrolment>();
}