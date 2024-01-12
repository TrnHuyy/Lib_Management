namespace Lib2.Models;

public class BookDetailsViewModel
{
    public Book book {get; set;}
    public List<Comment> comments {get; set;}
    public BookDetailsViewModel(Book book1, List<Comment> comments1)
    {
        book = book1;
        comments = comments1;
    }

}