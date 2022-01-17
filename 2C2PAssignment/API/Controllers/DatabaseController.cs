using API.Models.Providers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/database")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IDatabaseAccessProvider provider;

        public DatabaseController(IDatabaseAccessProvider provider)
        {
            this.provider = provider;
        }

        [HttpPost("create")]
        public IActionResult CreateNewDatabase(JObject data)
        {
            try
            {
                string dbName = (string)data["dbname"];
                provider.CreateDatabase(dbName);
                return Ok();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Create New Database Failed: {e.Message}");
                return ResultBadRequest(e);
            }
        }
    }
}
