using FastPizza.Domain.Entities.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace FastPizza.DataAccess.Interfaces.OrderIteams
{
    public interface IOrderIteam : IRepository<Item, Item>,
    IGetAll<Item>, ISearchable<Item>
    {
        public Task<int> CreateAsync(List<Item> items);
    }
}
