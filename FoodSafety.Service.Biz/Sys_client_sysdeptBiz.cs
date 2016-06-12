using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodSafety.Model;
using FoodSafety.Service.Dal;

namespace FoodSafety.Service.Biz
{
    public class Sys_client_sysdeptBiz
    {
        public static bool Add(FoodSafety.Model.Sys_client_sysdept model)
        {
            return Sys_client_sysdeptDal.Add(model);
        }
        public static bool Update(FoodSafety.Model.Sys_client_sysdept model)
        {
            return Sys_client_sysdeptDal.Update(model);
        }
        public static bool Delete(string deptId)
        {
            return Sys_client_sysdeptDal.Delete(deptId);
        }
        public static bool ExistsDept(string deptId)
        {
            return Sys_client_sysdeptDal.ExistsDept(deptId);
        }
        public static string GetDeptName(string fkDeptId)
        {
            return Sys_client_sysdeptDal.GetDeptName(fkDeptId);
        }
        public static bool ExistsCity(string cityName)
        {
            return Sys_client_sysdeptDal.ExistsCity(cityName);
        }
        public static string GetCityId(string cityName, string pid)
        {
            return Sys_client_sysdeptDal.GetCityId(cityName,pid);
        }
        public static bool ExistsDeleteDept(string deptId)
        {
            return Sys_client_sysdeptDal.ExistsDeleteDept(deptId);
        }

        public static bool UpdateClientDept(string title, string deptId)
        {
            return Sys_client_sysdeptDal.UpdateClientDept(title, deptId);
        }
    }
}
