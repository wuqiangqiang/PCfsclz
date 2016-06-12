using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using FoodSafety.Model;
using System.Data;

namespace FoodSafety.Service.Contract
{
    [ServiceContract]
    public interface ISysSetContract
    {
        [OperationContract]
        bool Add(FoodSafety.Model.Sys_client_sysdept model);
        [OperationContract]
        bool Update(FoodSafety.Model.Sys_client_sysdept model);
        [OperationContract]
        bool Delete(string deptId);
        [OperationContract]
        bool ExistsDept(string deptId);
        [OperationContract]
        string GetDeptName(string fkDeptId);
        [OperationContract]
        bool ExistsCity(string cityName);
        [OperationContract]
        string GetCityId(string cityName, string pid);
        [OperationContract]
        bool ExistsUserId(string strWhere);
        [OperationContract]
        bool ExistsDeleteDept(string deptId);
        [OperationContract]
        string GetSubName(string roleId);
       
        [OperationContract]//标识方法
        bool AddClientUser(FoodSafety.Model.sys_client_user model);//定义方法

        [OperationContract]
        bool DeleteClientUser(string id);

        [OperationContract]
        bool AddUpdateClientUser(string strSql);
        [OperationContract]
        bool ExistsRole(string roleId);
        [OperationContract]
        bool DeleteRole(string roleId);
        [OperationContract]
        bool AddUpdateRole(string strSql);
        [OperationContract]
        int LoadPicture(string deptId, byte[] img);

        [OperationContract]
        bool UpdateClientDept(string title, string deptId);

        [OperationContract]
        bool AddSjhc(string strSql);

        [OperationContract]
        DataTable GetUserModel(FoodSafety.Model.sys_client_user loginUser);
    }
}
