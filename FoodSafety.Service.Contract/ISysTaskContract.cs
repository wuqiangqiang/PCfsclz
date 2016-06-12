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
    public interface ISysTaskContract
    {
        [OperationContract] 
        DataTable ExecuteProTaskDetails(string function, string userId);

        [OperationContract]
        DataTable GetTaskTable(string deptType, string deptId);

        [OperationContract]
        DataTable GetTaskAssignTable(string deptType, string deptId);

        [OperationContract]
        int ExecuteProTask(string function, string deptId, string sb);

        [OperationContract]
        DataTable ExecuteProTaskCheck(string function, string userId, string year, int rowN, int tRowN);

        [OperationContract]
        DataTable ExecuteProTaskGrade(string userId);

        [OperationContract]
        string GetTaskParameter(string deptId, string gradeId);

        [OperationContract]
        bool ExistsGradeId(string deptId, string gradeId);

        [OperationContract]
        bool AddDeptGrade(string f1, string f2, string f3, string f4, string f5, DateTime f6);

        [OperationContract]
        bool UpdateDeptGrade(string f1, string f2, string f3, DateTime f4, string f5, string f6);
    }
}
