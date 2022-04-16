using System.ComponentModel.DataAnnotations;

namespace SF.Module25.Model;

public class User
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Email { get; set; } 
    public List<Book> Books { get; set; }

    public User()
    {
        Books = new List<Book>();
    }

    public override string ToString()
    {
        var books = string.Join(", ", Books.Select(b => b.Name));
        return string.Format($"{Id, -4} {Name, -10} {Email, -15} {books}");
    }
}
