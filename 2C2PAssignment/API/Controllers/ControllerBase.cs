using Microsoft.AspNetCore.Mvc;
using System;
using Model;
using Model.Exceptions;

namespace API.Controllers
{
    public class ControllerBase : Controller
    {
        public ControllerBase()
        {

        }

        public BadRequestObjectResult ResultBadRequest(string code = "Error", string message = "An error occurred. Please try again.", object data = null)
        {
            return BadRequest(new ResponseData
            {
                Code = code,
                Message = message,
                Data = data,
            });
        }

        public BadRequestObjectResult ResultBadRequest(Exception e)
        {
            if (e is AppException exception)
            {
                return BadRequest(exception.ResponseData);
            }

            return BadRequest(new ResponseData
            {
                Code = "Error",
                Message = "An error occurred. Please try again.",
                Data = null,
            });
        }
    }
}
