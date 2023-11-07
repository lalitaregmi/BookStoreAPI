using Dapper;
using Helper.Dapper;
using Models.Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class AuthorsService:IAuthors
    {
        public AuthorsService() { }

        public async Task<dynamic> Authors(Authors authors)
        {
            var res = new ResponseValues();

            {
                var sql = "sp_authors";
                var parameters = new DynamicParameters();
                parameters.Add("@authorid", authors.AuthorID);
                parameters.Add("@userid", authors.UserID);
                parameters.Add("@firstname", authors.FirstName);
               

                parameters.Add("@lastname", authors.LastName);
                parameters.Add("@authorname", authors.AuthorName);

                parameters.Add("@bio", authors.Bio);
                 parameters.Add("@flag", authors.Flag);
                parameters.Add("@authcode", authors.AuthCode);
                parameters.Add("@authorimage", authors.AuthorImage);
                parameters.Add("@isactive", authors.IsActive);

                parameters.Add("@imgurl", PathConstant.file_url() + "photo");

                if (!String.IsNullOrEmpty(authors.AuthorImage))
                {
                    var img = Convert.FromBase64String(authors.AuthorImage);//yasle user le pathako base 64 lai image ma convert garxa

                    var imagename = DateTime.Now.Ticks + ".png";

                    var image = System.Drawing.Image.FromStream(new MemoryStream(img));

                    String PathFile = PathConstant.file_loc() + "photo//";

                    if (!Directory.Exists(PathFile))
                    {
                        Directory.CreateDirectory(PathFile);
                    }

                    image.Save(PathFile + imagename, ImageFormat.Png);

                    parameters.Add("@authorimage", imagename);
                }

                var data = await DbHelper.RunProc<dynamic>(sql, parameters);
                if (data.Count() != 0 && data.FirstOrDefault().Message == null)
                {
                    res.Values = data.ToList();
                    res.StatusCode = 200;
                    res.Message = "Success";

                }
                else if (data.Count() == 1 && data.FirstOrDefault().Message != null)
                {
                    res.Values = null;
                    res.StatusCode = data.FirstOrDefault().StatusCode;
                    res.Message = data.FirstOrDefault().Message;

                }
                else
                {
                    res.Values = null;
                    res.StatusCode = 400;
                    res.Message = "no data";

                }
            }
            return res;
        }
    }
}
