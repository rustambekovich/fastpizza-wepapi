namespace FastPizza.Domain.Exceptions.Branches
{
    public class BrenchNotFoundException : NotFoundException
    {
        public BrenchNotFoundException()
        {
            this.TitleMessage = "Branch not found";

        }
    }
}
