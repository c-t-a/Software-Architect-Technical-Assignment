using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services
{
    public interface IRestService
    {
        //Task<T> GetAsync<T>(string url, JObject queryParameters = null);
        //Task<string> PostAsync(string url, JObject requestBody);
        Task<string> PostAsync(string url, JObject requestBody);
    }
}
