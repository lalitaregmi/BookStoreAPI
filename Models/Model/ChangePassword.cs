using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class ChangePassword
    {
        public string? UserID { get; set; }
        public string? OldPwd { get; set; }
        public string? NewPwd { get; set; }
        public string? AuthCode { get; set; }
    }



}

