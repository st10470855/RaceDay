using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels;

public class CreateEnrolmentViewModel
{
    [Required]
    public int EventID { get; set; }

    [Range(1, int.MaxValue,
     ErrorMessage = "Please select an event category.")]
    public int CategoryID { get; set; }

    public string EventName { get; set; } = string.Empty;

    public List<ParticipantCategoryViewModel> Categories { get; set; }
        = new();
}  