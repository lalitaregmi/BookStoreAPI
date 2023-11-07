using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class Login
    {
        public string? UserID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        
      public string? AuthCode { get; set; }
    }
    public class ResponseValues : CommonResponse

    {
        public List<dynamic> Values
        { get; set; }
    }


}

