using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodSafety.Model;
using System.Data;
using FoodSafety.Service.Dal;

namespace FoodSafety.Service.Biz
{
    public  class  Sys_client_roleBiz
    {
        public static DataTable GetClientRole(string strWhere)
        {
            return Sys_client_roleDal.GetClientRole(strWhere);
        }

        public static bool ExistsRole(string roleId)
        {
            return Sys_client_roleDal.ExistsRole(roleId);
        }
        public static bool DeleteRole(string roleId)
        {
            return Sys_client_roleDal.DeleteRole(roleId);
        }

        public static bool AddUpdateRole(string strSql)
        {
            return Sys_client_roleDal.AddUpdateRole(strSql);
        }

        public static DataTable GetRoleSub()
        {
            return Sys_client_roleDal.GetRoleSub();
        }

        public static DataTable GetUserRoleMenu(string id)
        {
            return Sys_client_roleDal.GetUserRoleMenu(id);
        }

        public static int ExecuteFunRoleSub(string field1, string field2)
        {
            return Sys_client_roleDal.ExecuteFunRoleSub(field1, field2);
        }
    }
}
