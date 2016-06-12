using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodSafety.Model;
using System.Data;
using FoodSafety.Service.Dal;

namespace FoodSafety.Service.Biz
{
    public class Sys_client_userBiz
    {
        public static bool ExistsUserId(string strWhere)
        {
            return Sys_client_userDal.ExistsUserId(strWhere);
        }
        public static bool DeleteClientUser(string id)
        {
            return Sys_client_userDal.DeleteClientUser(id);
        }

        public static bool AddClientUser(FoodSafety.Model.sys_client_user model)
        {
            return Sys_client_userDal.AddClientUser(model);

        }

        public static bool AddUpdateClientUser(string strSql)
        {
            return Sys_client_userDal.AddUpdateClientUser(strSql);
        }

        public static DataTable GetSysClientUser(string strWhere)
        {
            return Sys_client_userDal.GetSysClientUser(strWhere);
        }

        public static DataTable GetUserModel(FoodSafety.Model.sys_client_user loginUser)
        {
            return Sys_client_userDal.GetUserModel(loginUser);
        }
    }
}
