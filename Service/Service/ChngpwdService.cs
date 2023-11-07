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
    public class ChngpwdService : IChangepwd
    {


        public ChngpwdService() { }

        public async Task<dynamic> changepassword(ChangePassword change)
        {
            var res = new ResponseValues();

            {
                var sql = "sp_changepassword";
                var parameters = new DynamicParameters();

                parameters.Add("@userid", change.UserID);
                parameters.Add("@oldpwd", change.OldPwd);
                parameters.Add("@newpwd", change.NewPwd);
                parameters.Add("@authcode", change.AuthCode);







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
