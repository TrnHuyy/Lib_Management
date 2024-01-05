namespace Lib2.Models;

public class ViewAllLoanViewModel
{
    public List<Loan> Loans {get; set;}
    public User User {get; set;}
    public Book Book {get; set;}
    public ViewAllLoanViewModel(List<Loan> loans, User user, Book book)
    {
        Loans = loans;
        User = user;
        Book = book;
    }
}