using FastPizza.Domain.Entities.Branches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.DataAccess.Interfaces.Branches
{
    public interface IBranchRepository : IRepository<Branch, Branch>,
        IGetAll<Branch>
    {
    }
}
