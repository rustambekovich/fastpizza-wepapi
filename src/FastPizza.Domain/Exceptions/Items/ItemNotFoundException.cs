namespace FastPizza.Domain.Exceptions.Items
{
    public class ItemNotFoundException : NotFoundException
    {
        public ItemNotFoundException()
        {
            this.TitleMessage = "Item not found";
        }
    }
}
