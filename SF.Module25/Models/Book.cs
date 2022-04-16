using System.ComponentModel.DataAnnotations;

namespace SF.Module25.Model;

public class Book
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public int Year { get; set; }
    [Required]
    public string? Author { get; set; }
    public string? Genre { get; set; }

    public int? UserId { get; set; }
    public User? User { get; set; }

    public override string ToString()
    {
        return string.Format($"{Id,-4} {Name,-10} {Year,-5} {Author,-15} {Genre,-12}");
    }

}