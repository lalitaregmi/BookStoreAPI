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
    public class UserService : IUser
    {

        public UserService() { }
        public async Task<dynamic> user(Models.Model.User users)
        {
            var res = new ResponseValues();
            {               
                var sql = "sp_user";
                var parameters = new DynamicParameters();
                parameters.Add("@firstname", users.FirstName);
                parameters.Add("@middlename", users.MiddleName);
                parameters.Add("@lastname", users.LastName);
                parameters.Add("@username", users.UserName);
                parameters.Add("@userimage", users.UserImage);

                parameters.Add("@usertype", users.UserType);
                parameters.Add("@email", users.Email);
                parameters.Add("@userid", users.UserID);
                parameters.Add("@password", users.Password);
                parameters.Add("@flag", users.Flag);
                parameters.Add("@authcode", users.AuthCode);

                parameters.Add("@phnnum", users.PhnNum);
                parameters.Add("@address", users.Address);
                parameters.Add("@district", users.District);
                parameters.Add("@isactive", users.IsActive);
                parameters.Add("@imgurl", PathConstant.file_url() + "photo");

                if (!String.IsNullOrEmpty(users.UserImage))
                {
                    var img = Convert.FromBase64String(users.UserImage);//yasle user le pathako base 64 lai image ma convert garxa

                    var imagename = DateTime.Now.Ticks + ".png";

                    var image = System.Drawing.Image.FromStream(new MemoryStream(img));

                    String PathFile = PathConstant.file_loc() + "photo//";

                    if (!Directory.Exists(PathFile))
                    {
                        Directory.CreateDirectory(PathFile);
                    }

                    image.Save(PathFile + imagename, ImageFormat.Png);

                    parameters.Add("@userimage", imagename);
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
