using Dapper;
using Helper.Dapper;
using Models.Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class RegisterService:IRegister
    {
        public RegisterService() { }
        public async Task<dynamic> register(Register reg)
        {
            var res = new ResponseValues();

            {
                var sql = "sp_register";
                var parameters = new DynamicParameters();
                parameters.Add("@firstname", reg.FirstName);
                parameters.Add("@middlename", reg.MiddleName);
                parameters.Add("@lastname", reg.LastName);
                parameters.Add("@username", reg.UserName);
                parameters.Add("@usertype", reg.UserType);
                parameters.Add("@email", reg.Email);
                parameters.Add("@userimage", reg.UserImage);
                parameters.Add("@authcode", reg.AuthCode);
                parameters.Add("@password", reg.Password);
                parameters.Add("@phnnum", reg.PhoneNum);
                parameters.Add("@address", reg.Address);
                parameters.Add("@district", reg.District);
                parameters.Add("@userid", reg.UserID);

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
