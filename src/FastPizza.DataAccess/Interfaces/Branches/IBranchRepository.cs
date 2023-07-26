using FastPizza.Domain.Entities.Branches;

namespace FastPizza.DataAccess.Interfaces.Branches
{
    public interface IBranchRepository : IRepository<Branch, Branch>,
        IGetAll<Branch>
    {
    }
}
