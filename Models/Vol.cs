namespace AirNoSQL.Models
{
    public class Vol
    {
        public string? Id { get; set; }

        public string? Reference { get; set; }

        public DateTime DateDepart { get; set; }

        public string? VilleDepart { get; set; }

        public DateTime DateArrivee { get; set; }

        public string? VilleArrivee { get; set; }

        public virtual Ville? Ville { get; set; }
    }
}
