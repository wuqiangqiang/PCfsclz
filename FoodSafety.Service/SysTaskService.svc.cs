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
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“SysTaskService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 SysTaskService.svc 或 SysTaskService.svc.cs，然后开始调试。
    public class SysTaskService : ISysTaskContract
    {
        public  DataTable ExecuteProTaskDetails(string function, string userId)
        {
            return SysTaskBiz.ExecuteProTaskDetails(function, userId);
        }
        public  DataTable GetTaskTable(string deptType, string deptId)
        {
            return SysTaskBiz.GetTaskTable(deptType, deptId);
        }
        public  DataTable GetTaskAssignTable(string deptType, string deptId)
        {
            return SysTaskBiz.GetTaskAssignTable(deptType, deptId);
        }
        public int ExecuteProTask(string function, string deptId, string sb)
        {
            return SysTaskBiz.ExecuteProTask(function, deptId, sb);
        }
        public  DataTable ExecuteProTaskCheck(string function, string userId, string year, int rowN, int tRowN)
        {
            return SysTaskBiz.ExecuteProTaskCheck(function, userId, year, rowN, tRowN);
        }
        public  DataTable ExecuteProTaskGrade(string userId)
        {
            return SysTaskBiz.ExecuteProTaskGrade(userId);
        }
        public string GetTaskParameter(string deptId, string gradeId)
        {
            return SysTaskBiz.GetTaskParameter(deptId, gradeId);
        }
        public bool ExistsGradeId(string deptId, string gradeId)
        {
            return SysTaskBiz.ExistsGradeId(deptId, gradeId);
        }
        public  bool AddDeptGrade(string f1, string f2, string f3, string f4, string f5, DateTime f6)
        {
            return SysTaskBiz.AddDeptGrade(f1, f2, f3, f4, f5, f6);
        }

        public  bool UpdateDeptGrade(string f1, string f2, string f3, DateTime f4, string f5, string f6)
        {
            return SysTaskBiz.UpdateDeptGrade(f1, f2, f3, f4, f5, f6);
        }
    }
}
