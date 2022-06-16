using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(56)]
        public string Name { get; set; }
    }
}