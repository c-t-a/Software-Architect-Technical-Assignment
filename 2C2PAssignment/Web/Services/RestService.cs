using Model;
using Model.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;

namespace Web.Services
{
    public class RestService : IRestService
    {
        public async Task<string> PostAsync(string url, JObject requestBody)
        {

            HttpContent httpContent = new StringContent(requestBody.ToString());
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await (await CreateHttpClient()).PostAsync(url,httpContent);
            await HandleResponse(response);

            return await response.Content.ReadAsStringAsync();
        }
        private async Task<HttpClient> CreateHttpClient()
        {
            HttpClientHandler ch = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            HttpClient client = new HttpClient(ch)
            {
                Timeout = TimeSpan.FromSeconds(120)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
        private async Task HandleResponse(HttpResponseMessage response)
        {
            string fileName = Invoice.fileName;
            string filePath = $"{Invoice.filePath}\\log.txt";

            // wirte log file
            try
            {
                if (File.Exists(filePath))
                {
                    //FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Write);
                    StreamWriter writer = new StreamWriter(filePath,append:true);
                    writer.WriteLine($"{fileName},{response.StatusCode}");
                    writer.Close();
                    //fs.Close();
                }
                else {
                    FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                    StreamWriter writer = new StreamWriter(fs);
                    writer.WriteLine($"{fileName},{response.StatusCode}{Environment.NewLine}");
                    writer.Close();
                    fs.Close();
                }
            }
            catch (Exception e)
            {

            }

            if (!response.IsSuccessStatusCode)
            {
                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.BadRequest)
                {
                    try
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<ResponseData>(json);
                        if (data.Code == "not_exist")
                        {
                            throw new NotFoundException(message: data.Message, data: data.Data);
                        }

                        throw new AppException(data);
                    }
                    catch
                    {
                        throw;
                    }
                }

                throw new Exception("An error occured. Please try again.");
            }
        }
    }
}
