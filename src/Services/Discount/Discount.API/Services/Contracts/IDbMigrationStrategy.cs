using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Services.Contracts
{
    public interface IDbMigrationStrategy
    {
        void Migrate();
    }
}
