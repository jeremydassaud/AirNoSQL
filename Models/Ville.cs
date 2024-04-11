using System.ComponentModel.DataAnnotations;

namespace AirNoSQL.Models
{
    public class Ville
    {
        public string? IdVille { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [StringLength(5)]
        [RegularExpression(@"[0-9]*$")]
        public string? CodePostal { get; set; }

        public virtual Vol? Vol { get; set; }
    }
}
