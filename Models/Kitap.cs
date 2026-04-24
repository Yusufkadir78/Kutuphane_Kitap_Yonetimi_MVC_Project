using System.ComponentModel.DataAnnotations;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Başlık boş olamaz")]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    public int CategoryId { get; set; }   // Foreign Key
    public Category? Category { get; set; } // Navigation Property

    [Required(ErrorMessage = "Yazar boş olamaz")]
    [StringLength(100)]
    public string Author { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalı")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stok negatif olamaz")]
    public int Stock { get; set; }
}