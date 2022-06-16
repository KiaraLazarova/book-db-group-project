using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer;

public class Book
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(85)]
    public string Title { get; set; }

    [Required, MaxLength(300)]
    public string Annotation { get; set; }

    [Required]
    public DateTime DateTimeOfPublication { get; set; }

    public Author Author { get; set; }
}