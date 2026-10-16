namespace RaceDay.API.Models;

public class Organizer
{
    public int OrganizerID { get; set; }

    public int UserID { get; set; }

    public string OrganisationName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Website { get; set; }

    public User User { get; set; } = null!;
}