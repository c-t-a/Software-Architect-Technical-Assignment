using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions
{
    public class AppException : Exception
    {
        public ResponseData ResponseData { get; set; }

        public AppException(string message) : base(message)
        {
            ResponseData = new ResponseData
            {
                Code = "error",
                Message = message
            };
        }

        public AppException(ResponseData data) : base(data.Message)
        {
            ResponseData = data;
        }
    }
}
