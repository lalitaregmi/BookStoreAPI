//using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class PathConstant
    {

        public static string file_loc() // to save the image in this location
        {
            return "C:\\easysoft\\assets\\happyhomes\\";
        }

        public static string file_url() // to access the image through this link
        {
            return "https://testing.esnep.com/assets/happyhomes/";
        }

    }
}