using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class Books
    {
        public string? BookID { get; set; }
        public string? Title { get; set; }
        public string? Year { get; set;}
        public string? ISBN { get; set; }//International Standard Book Number
        public string? Summary { get; set; }
        public string? BookImage { get; set; }
        public string? Price { get; set; }
        public string? AuthorID { get; set; }
        public string? UserID { get; set; }
        public string? AuthCode { get; set; }
        public string? IsActive { get; set; }

        public string? Flag { get; set; }
    }
}
