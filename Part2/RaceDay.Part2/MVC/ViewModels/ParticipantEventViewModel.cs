namespace MVC.ViewModels;

public class ParticipantEventViewModel
{
    public int EventID { get; set; }

    public string EventName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateOnly EventDate { get; set; }

    public string Location { get; set; } = string.Empty;

    public decimal Distance { get; set; }

    public string EventType { get; set; } = string.Empty;
}