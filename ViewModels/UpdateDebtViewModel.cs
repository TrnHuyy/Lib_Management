using System.ComponentModel.DataAnnotations;

namespace Lib2.Models;

public class UpdateDebtViewModel
{
    [Required(ErrorMessage = "Please select user.")]
    public int SelectedUserId { get; set; }

    public long NewDebt {get; set;}

    [Required(ErrorMessage = "Please enter an end date.")]
    public DateTime EndDate { get; set; }

    public List<User> AvailableUsers { get; set; }
}
