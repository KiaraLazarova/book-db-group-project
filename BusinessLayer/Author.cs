using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string FirstName { get; set; }

        [Required, MaxLength(35)]
        public string LastName { get; set; }

        [Required, Range(1, 100)]
        public int Age { get; set; }

        public Country Country { get; set; }
    }
}