namespace FastPizza.Domain.Entities.Branches
{
    public class Branch : Auditable
    {
        public string Name { get; set; } = string.Empty;
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
