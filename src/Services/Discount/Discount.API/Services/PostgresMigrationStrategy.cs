using Discount.API.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Services
{
    internal sealed class PostgresMigrationStrategy : IDbMigrationStrategy

    {
        public void Migrate()
        {
            throw new NotImplementedException();
        }
    }
}
