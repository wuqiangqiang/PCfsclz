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
    public class SysTaskBiz
    {
        public static DataTable ExecuteProTaskDetails(string function, string userId)
        {
            return SysTaskDal.ExecuteProTaskDetails(function, userId);
        }

        public static DataTable GetTaskTable(string deptType, string deptId)
        {
            return SysTaskDal.GetTaskTable(deptType, deptId);
        }

        public static DataTable GetTaskAssignTable(string deptType, string deptId)
        {
            return SysTaskDal.GetTaskAssignTable(deptType, deptId);
        }

        public static int ExecuteProTask(string function, string deptId, string sb)
        {
            return SysTaskDal.ExecuteProTask(function, deptId, sb);
        }

        public static DataTable ExecuteProTaskCheck(string function, string userId, string year, int rowN, int tRowN)
        {
            return SysTaskDal.ExecuteProTaskCheck(function, userId, year, rowN, tRowN);
        }

        public static DataTable ExecuteProTaskGrade(string userId)
        {
            return SysTaskDal.ExecuteProTaskGrade(userId);
        }

        public static string GetTaskParameter(string deptId, string gradeId)
        {
            return SysTaskDal.GetTaskParameter(deptId, gradeId);
        }

        public static bool ExistsGradeId(string deptId, string gradeId)
        {
            return SysTaskDal.ExistsGradeId(deptId, gradeId);
        }

        public static bool AddDeptGrade(string f1, string f2, string f3, string f4, string f5, DateTime f6)
        {
            return SysTaskDal.AddDeptGrade(f1, f2, f3, f4, f5, f6);
        }

        public static bool UpdateDeptGrade(string f1, string f2, string f3, DateTime f4, string f5, string f6)
        {
            return SysTaskDal.UpdateDeptGrade(f1, f2, f3, f4, f5, f6);
        }
    }
}
