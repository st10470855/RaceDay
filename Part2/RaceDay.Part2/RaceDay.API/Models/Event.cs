namespace RaceDay.API.Models;

public class Event
{
    public int EventID { get; set; }

    public int OrganizerID { get; set; }

    public string EventName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateOnly EventDate { get; set; }

    public string Location { get; set; } = string.Empty;

    public decimal Distance { get; set; }

    public string EventType { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public Organizer Organizer { get; set; } = null!;

    public ICollection<Category> Categories { get; set; } = new List<Category>();

    public ICollection<EventEnrolment> EventEnrolments { get; set; }
        = new List<EventEnrolment>();
}