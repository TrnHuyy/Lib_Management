using System.ComponentModel.DataAnnotations;

namespace Lib2.Models;

public class CreateLoanViewModel
{
    [Required(ErrorMessage = "Please select a book.")]
    public int SelectedBookId { get; set; }

    [Required(ErrorMessage = "Please enter a start date.")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "Please enter an end date.")]
    public DateTime EndDate { get; set; }

    public List<Book> AvailableBooks { get; set; }
}
