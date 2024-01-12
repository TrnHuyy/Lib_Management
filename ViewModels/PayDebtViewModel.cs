using System.ComponentModel.DataAnnotations;

namespace Lib2.Models;

public class PayDebtViewModel
{

    [Required(ErrorMessage = "Please enter a date.")]
    public DateTime Date { get; set; }


}
