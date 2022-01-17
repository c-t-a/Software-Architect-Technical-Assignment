using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Services
{
    public class AppDBContext : IAppDBContext
    {
        private readonly IConfiguration configuration;

        public AppDBContext(IConfiguration configuration)
        {
            
            this.configuration = configuration;
        }

        private DBContextBase _DbContext;

        public DBContextBase DbContext
        {
            get {
                if (_DbContext == null) {
                    var connStr = configuration["PostgreSqlConnection"];
                    var optionsBuilder = new DbContextOptionsBuilder<DBContextBase>();
                    optionsBuilder.UseNpgsql(connStr);
                    _DbContext = new DBContextBase(optionsBuilder.Options);
                }
                return _DbContext;
            }
        }
    }
}
