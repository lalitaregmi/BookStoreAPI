using Dapper;
using Helper.Dapper;
using Models.Model;
using Org.BouncyCastle.Ocsp;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class LoginService:ILogin
    {
        
            public LoginService() { }

            public async Task<dynamic> Logins(Login login)
            {
                var res = new ResponseValues();

                {
                    var sql = "sp_login";
                    var parameters = new DynamicParameters();
                parameters.Add("@userid", login.UserID);

                parameters.Add("@username", login.UserName);
                    parameters.Add("@password", login.Password);
                parameters.Add("@authcode", login.AuthCode);





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
