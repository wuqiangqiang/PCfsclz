using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using FoodSafety.Service.Biz;
using FoodSafety.Service.Contract;

namespace FoodSafety.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“SysSetService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 SysSetService.svc 或 SysSetService.svc.cs，然后开始调试。
    public class SysSetService : ISysSetContract
    {
        public bool Add(FoodSafety.Model.Sys_client_sysdept model)
        {
            return Sys_client_sysdeptBiz.Add(model);
        }
        public bool Update(FoodSafety.Model.Sys_client_sysdept model)
        {
            return Sys_client_sysdeptBiz.Update(model);
        }

        public bool AddClientUser(FoodSafety.Model.sys_client_user model)
        {
            return Sys_client_userBiz.AddClientUser(model);
        }

        public bool Delete(string deptId)
        {
            return Sys_client_sysdeptBiz.Delete(deptId);
        }

        public bool ExistsDept(string deptId)
        {
            return Sys_client_sysdeptBiz.ExistsDept(deptId);
        }

        public string GetDeptName(string fkDeptId)
        {
            return Sys_client_sysdeptBiz.GetDeptName(fkDeptId);
        }

        public bool ExistsCity(string cityName)
        {
            return Sys_client_sysdeptBiz.ExistsCity(cityName);
        }
        public string GetCityId(string cityName, string pid)
        {
            return Sys_client_sysdeptBiz.GetCityId(cityName,pid);
        }
        public bool ExistsUserId(string strWhere)
        {
            return Sys_client_userBiz.ExistsUserId(strWhere);
        }
        public bool ExistsDeleteDept(string deptId)
        {
            return Sys_client_sysdeptBiz.ExistsDeleteDept(deptId);
        }

        public string GetSubName(string roleId)
        {
            return OperationBiz.GetSubName(roleId);
        }

        public bool DeleteClientUser(string id)
        {
            return Sys_client_userBiz.DeleteClientUser(id);
        }
        public bool AddUpdateClientUser(string strSql)
        {
            return Sys_client_userBiz.AddUpdateClientUser(strSql);
        }

        public bool ExistsRole(string roleId)
        {
            return Sys_client_roleBiz.ExistsRole(roleId);
        }

        public bool DeleteRole(string roleId)
        {
            return Sys_client_roleBiz.DeleteRole(roleId);
        }

        public bool AddUpdateRole(string strSql)
        {
            return Sys_client_roleBiz.AddUpdateRole(strSql);
        }

        public int LoadPicture(string deptId, byte[] img)
        {
            return OperationBiz.LoadPicture(deptId, img);
        }

        public bool UpdateClientDept(string title, string deptId)
        {
            return Sys_client_sysdeptBiz.UpdateClientDept(title, deptId);
        }

        public bool AddSjhc(string strSql)
        {
            return OperationBiz.AddSjhc(strSql);
        }
        public  DataTable GetUserModel(FoodSafety.Model.sys_client_user loginUser)
        {
            return Sys_client_userBiz.GetUserModel(loginUser);
        }
    }
}
