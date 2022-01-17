using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message = "Data not found!.", object data = null) : base(new ResponseData{
                Code = "Not Exist",
                Message = message,
                Data = data,
            })
        {
            
        }
    }
}
