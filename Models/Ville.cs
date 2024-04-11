namespace AirNoSQL.Models
{
    public class Ville
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? CodePostal { get; set; }

        public virtual Vol? Vol { get; set; }
    }
}
