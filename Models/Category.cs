using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<Book> Books { get; set; } = new List<Book>();
    public DbSet<Category>? Categories { get; set; }
}