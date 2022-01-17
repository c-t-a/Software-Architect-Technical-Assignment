using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services;

namespace Web.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly IRestService restService;

        public DatabaseRepository(IRestService restService)
        {
            this.restService = restService;
        }

        public async Task Create(string dbName)
        {
            await restService.PostAsync(Constants.RestDatabaseCreate, new JObject { { "dbname", dbName } });
        }
    }
}
