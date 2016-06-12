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
   public static class Sys_client_roleDal
    {

       public static DataTable GetClientRole(string strWhere)
       {
           StringBuilder sb = new StringBuilder();
           //sb.Append("SELECT roleId,roleName,roleExpl,roleLevel FROM sys_client_role ");
           sb.Append("select roleId,cdeptId,cuserId,roleName,roleExpl,roleLevel from sys_client_role");
           if (strWhere.Trim() != "")
           {
               sb.Append(" where " + strWhere);
           }

           try
           {
               return DbHelperMySQL.CreateDbHelper().GetDataSet(sb.ToString()).Tables[0];
           }
           catch (Exception e)
           {
               System.Diagnostics.Debug.WriteLine("GetClientRole异常");
               throw new Exception(e.Message);
           }
       }

       public static bool ExistsRole(string roleId)
       {
           string sql = string.Format("select id from sys_client_user where roleId ='{0}'", roleId);

           try
           {
               return DbHelperMySQL.CreateDbHelper().Exists(sql);
           }
           catch (Exception e)
           {
               System.Diagnostics.Debug.WriteLine("ExistsRole异常");
               throw new Exception(e.Message);
           }
       }

       public static bool DeleteRole(string roleId)
       {

           StringBuilder strSql = new StringBuilder();
           strSql.Append("delete from sys_client_role ");
           strSql.Append(" where roleId=@roleId ");
           MySqlParameter[] parameters = {
					new MySqlParameter("@roleId", MySqlDbType.VarChar,50)			};
           parameters[0].Value = roleId;

           int rows = DbHelperMySQL.CreateDbHelper().ExecuteSql(strSql.ToString(), parameters);
           if (rows > 0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }

       public static bool AddUpdateRole(string strSql)
       {
           int rows = DbHelperMySQL.CreateDbHelper().ExecuteSql(strSql.ToString());
           if (rows > 0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }
       public static DataTable GetRoleSub()
       {
           string sql = "SELECT s.subId,s.subName,s.fkSubId FROM sys_sub s  order by subOrderId";

           try
           {
               return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
           }
           catch (Exception e)
           {
               System.Diagnostics.Debug.WriteLine("GetRoleSub异常");
               throw new Exception(e.Message);
           }
       }

       public static DataTable GetUserRoleMenu(string id)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("SELECT rp.subId,s.subName FROM sys_sub s ,sys_rolepermission rp ")
             .Append("WHERE s.subId = rp.subId ")
             .AppendFormat(" and rp.roleId ='{0}' ", id);

           string sql = sb.ToString();
           try
           {
               return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
           }
           catch (Exception e)
           {
               System.Diagnostics.Debug.WriteLine("GetUserRoleMenu异常");
               throw new Exception(e.Message);
           }
       }

       public static int ExecuteFunRoleSub(string field1,string field2)
       {
           string sql = string.Format("SELECT f_role_sub('{0}','{1}')",field1 ,field2 );

           try
           {
               return DbHelperMySQL.CreateDbHelper().ExecuteSql(sql);
           }
           catch (Exception e)
           {
               System.Diagnostics.Debug.WriteLine("GetRoleSub异常");
               throw new Exception(e.Message);
           }
       }
    }
}
