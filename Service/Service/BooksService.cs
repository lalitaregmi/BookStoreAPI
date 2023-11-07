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
    public class BooksService:IBooks
    {
        public async Task<dynamic> Books(Books books)
        {
            var res = new ResponseValues();

            {
                var sql = "sp_books";
                var parameters = new DynamicParameters();
                parameters.Add("@bookid", books.BookID);
                parameters.Add("@userid", books.UserID);
                parameters.Add("@authorid", books.AuthorID);
                parameters.Add("@title", books.Title);

                parameters.Add("@year", books.Year);
                parameters.Add("@isbn", books.ISBN);
                parameters.Add("@summary", books.Summary);
                parameters.Add("@price", books.Price);
                parameters.Add("@bookimage", books.BookImage);

                parameters.Add("@flag", books.Flag);
                parameters.Add("@authcode", books.AuthCode);
                parameters.Add("@isactive", books.IsActive);

                parameters.Add("@imgurl", PathConstant.file_url() + "photo");

                if (!String.IsNullOrEmpty(books.BookImage))
                {
                    var img = Convert.FromBase64String(books.BookImage);//yasle user le pathako base 64 lai image ma convert garxa

                    var imagename = DateTime.Now.Ticks + ".png";

                    var image = System.Drawing.Image.FromStream(new MemoryStream(img));

                    String PathFile = PathConstant.file_loc() + "photo//";

                    if (!Directory.Exists(PathFile))
                    {
                        Directory.CreateDirectory(PathFile);
                    }

                    image.Save(PathFile + imagename, ImageFormat.Png);

                    parameters.Add("@bookimage", imagename);
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
