using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.DataAccess.Repositories
{
    public class BaseRepository
    {
        protected NpgsqlConnection _connection;
        public BaseRepository()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            this._connection = new NpgsqlConnection("Host=localhost; Port=5432; Database=Fast-Pizza-db; User Id=postgres; Password=2151;");
        }
    }
}
