namespace RaceDay.API.Models;

public class Participant
{
    public int ParticipantID { get; set; }

    public int UserID { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Club { get; set; }

    public User User { get; set; } = null!;
}