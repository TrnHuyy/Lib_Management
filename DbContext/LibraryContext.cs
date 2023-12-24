using Microsoft.EntityFrameworkCore;
using Lib2.Models;
using System.Data.Common;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
    }

    public LibraryContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=LibraryManagement;User Id=sa;Password=Huy05102003@;TrustServerCertificate=True");
        }
    }

    private const string ConnectionString = @"
                Data Source=localhost,1433;
                Initial Catalog=LibraryManagement;
                User ID=SA;Password=Huy05102003@;TrustServerCertificate=True";

    public DbSet<Book> Books { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Notification> Notifications {get; set; }
    public DbSet<Librarian> Librarians {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Định nghĩa các quan hệ, ràng buộc, và cấu hình khác ở đây
        
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<Book>().HasKey(b => b.Id);
        modelBuilder.Entity<Book>().Property(b => b.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<Notification>().HasKey(n => n.Id);
        modelBuilder.Entity<Notification>().Property(n => n.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<Loan>().HasKey(n => n.Id);
        modelBuilder.Entity<Loan>().Property(n => n.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<Librarian>().HasKey(n => n.Id);
        modelBuilder.Entity<Librarian>().Property(n => n.Id).ValueGeneratedOnAdd();
    }


}
