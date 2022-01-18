using API.Models.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using API.Models.Entities;
using System.Collections.Generic;
using Model;

namespace API.Controllers
{
    [Route("api/invoices")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceAccessProvider invoiceAccessProvider;

        public InvoiceController(IInvoiceAccessProvider invoiceAccessProvider)
        {
            this.invoiceAccessProvider = invoiceAccessProvider;
        }

        [HttpGet]
        public IActionResult GetInvoices()
        {
            try
            {
                var invoices = invoiceAccessProvider.GetInvoices();
                return Ok(invoices);
            }
            catch (Exception e)
            {
                return ResultBadRequest(e.Message);
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadInvoice(JObject data)
        {
            try
            {
                invoiceAccessProvider.UploadInvoice(data);
                return Ok();
            }
            catch (Exception e) { 
                return ResultBadRequest(e.Message);
            }
        }

        [HttpGet("status/{status}")]
        public IActionResult GetInvoicesByStatus(char status)
        {
            try
            {
                var invoices = invoiceAccessProvider.GetInvoicesByStatus(status);
                return Ok(invoices);
            }
            catch (Exception e)
            {
                return ResultBadRequest(e.Message);
            }
        }

        [HttpGet("currency/{cCode}")]
        public IActionResult GetInvoicesByCurrencyCode(string cCode)
        {
            try
            {
                var invoices = invoiceAccessProvider.GetInvoicesByCurrencyCode(cCode);
                return Ok(invoices);
            }
            catch (Exception e)
            {
                return ResultBadRequest(e.Message);
            }
        }
    }
}
