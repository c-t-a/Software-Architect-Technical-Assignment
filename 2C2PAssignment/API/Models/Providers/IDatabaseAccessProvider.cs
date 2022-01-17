using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Providers
{
    public interface IDatabaseAccessProvider
    {
        public void CreateDatabase(string dbName);
    }
}
