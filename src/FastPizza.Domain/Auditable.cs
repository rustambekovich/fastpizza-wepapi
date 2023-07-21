namespace FastPizza.Domain
{
    public class Auditable : Base
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
