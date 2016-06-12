using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodSafety.Model;
using System.Data;
using DBUtility;
using MySql.Data.MySqlClient;

namespace FoodSafety.Service.Dal
{
    public static class SysTaskDal
    {
        public static DataTable ExecuteProTaskDetails(string function,string userId)
        {
            string sql = string.Format("call {0}('{1}')", function, userId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProTaskDetails异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable GetTaskTable(string deptType,string deptId)
        {
     
            string sql = string.Empty;
            switch (deptType)
            {
                case "0":
                    sql = string.Format("select t_det_item_produce.itemName,task from t_task_assign_produce left JOIN t_det_item_produce ON t_task_assign_produce.iid = t_det_item_produce.itemId  where t_task_assign_produce.did ='{0}'",deptId);
                    break;
                case "1":
                    sql = string.Format("select t_det_item_fishery.itemName,task from t_task_assign_fishery left JOIN t_det_item_fishery ON t_task_assign_fishery.iid = t_det_item_fishery.itemId where t_task_assign_fishery.did ='{0}'",deptId);
                    break;
                case "2":
                    sql = string.Format("select t_det_item_animal.itemName,task  from t_task_assign_animal left JOIN t_det_item_animal ON t_task_assign_animal.iid = t_det_item_animal.itemId where t_task_assign_animal.did = '{0}'",deptId);
                                       
                    break;
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetTaskTable异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable GetTaskAssignTable(string deptType, string deptId)
        {

            string sql = string.Empty;
            switch (deptType)
            {
                case "0":
                    sql = string.Format("SELECT itemId,itemName,t.task FROM t_det_item_produce i left join t_task_assign_produce t on i.itemId = t.iid and t.did = '{0}' where openFlag = '1'", deptId);
                    break;
                case "1":
                    sql = string.Format("SELECT itemId,itemName,t.task FROM t_det_item_fishery i left join t_task_assign_fishery t on i.itemId = t.iid and t.did = '{0}'where openFlag = '1' ", deptId);
                    break;
                case "2":
                    sql = string.Format("SELECT itemId,itemName,t.task FROM t_det_item_animal i left join t_task_assign_animal t on i.itemId = t.iid and t.did = '{0}'where openFlag = '1'", deptId);

                    break;
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetTaskAssignTable异常");
                throw new Exception(e.Message);
            }
        }
        public static int ExecuteProTask(string function, string deptId,string sb)
        {
            string sql = string.Format("call {0} ('{1}','{2}')", function, deptId,sb);

            try
            {
                return DbHelperMySQL.CreateDbHelper().ExecuteSql(sql);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProTask异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProTaskCheck(string function, string userId, string year,int rowN,int tRowN)
        {
            string sql = string.Format("call {0}('{1}','{2}',{3},{4})", function, userId, year,rowN,tRowN);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProTaskCheck异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable ExecuteProTaskGrade(string userId)
        {
            string sql = string.Format("call p_task_grade('{0}')",userId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProTaskGrade异常");
                throw new Exception(e.Message);
            }
        }

        public static string GetTaskParameter(string deptId, string gradeId)
        {
            string sql = string.Empty;
            string sql2 = string.Empty;
            switch (gradeId)
            {
                case "2":
                    sql =string.Format("select parameterDown from t_dept_grade where deptId = '{0}' and gradeId = '{1}'", deptId,"1");
                    sql2 = string.Format("select parameterUp from t_dept_grade where deptId = '{0}' and gradeId = '{1}'",deptId,"3");
                    break;
                case "3":
                     sql =string.Format("select parameterDown from t_dept_grade where deptId = '{0}' and gradeId = '{1}'", deptId,"2");
                    sql2 = string.Format("select parameterUp from t_dept_grade where deptId = '{0}' and gradeId = '{1}'",deptId,"4");
                    break;
                case "4":
                    sql =string.Format("select parameterDown from t_dept_grade where deptId = '{0}' and gradeId = '{1}'", deptId,"3");
                    sql2 = string.Format("select parameterUp from t_dept_grade where deptId = '{0}' and gradeId = '{1}'",deptId,"5");
                    break;
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetSingle(sql).ToString() + "_" +
                       DbHelperMySQL.CreateDbHelper().GetSingle(sql2).ToString();

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetTaskParameter异常");
                throw new Exception(e.Message);
            }
        }
        public static bool ExistsGradeId(string deptId,string gradeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(gradeId) from t_dept_grade  ");
            strSql.AppendFormat("where deptId = '{0}' and gradeId = '{1}'",deptId,gradeId);
            try
            {
                return DbHelperMySQL.CreateDbHelper().Exists(strSql.ToString());
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExistsGradeId异常");
                throw new Exception(e.Message);
            }
        }

        public static bool AddDeptGrade(string f1,string f2,string f3,string f4,string f5,DateTime f6)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into t_dept_grade(deptId,gradeId,parameterDown,parameterUp,cuserid,cdate) ");
            sb.AppendFormat("values('{0}','{1}','{2}','{3}','{4}', '{5}')",f1,f2,f3,f4,f5,f6);
       
            int rows = DbHelperMySQL.CreateDbHelper().ExecuteSql(sb.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UpdateDeptGrade(string f1, string f2, string f3, DateTime f4, string f5, string f6)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("update t_dept_grade ");
            sb.AppendFormat("set parameterDown='{0}',parameterUp = '{1}',cuserId='{2}',cdate='{3}' where deptId = '{4}' and gradeId = '{5}'", f1, f2, f3, f4, f5, f6);

            int rows = DbHelperMySQL.CreateDbHelper().ExecuteSql(sb.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
