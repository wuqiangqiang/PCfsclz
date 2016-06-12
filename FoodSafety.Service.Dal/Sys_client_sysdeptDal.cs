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
    public static class Sys_client_sysdeptDal
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(FoodSafety.Model.Sys_client_sysdept model)
        { 
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_client_sysdept(");
            strSql.Append("deptId,deptName,deptLevel,fkDeptId,province,city,country,address,contacter,contacterphone,supplierId,maintypes,town,principal,principalphone,depttype)");
            strSql.Append(" values (");
            strSql.Append("@deptId,@deptName,@deptLevel,@fkDeptId,@province,@city,@country,@address,@contacter,@contacterphone,@supplierId,@maintypes,@town,@principal,@principalphone,@depttype)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@deptId", MySqlDbType.VarChar,50), 
					new MySqlParameter("@deptName", MySqlDbType.VarChar,50),
					new MySqlParameter("@deptLevel", MySqlDbType.VarChar,1),
					new MySqlParameter("@fkDeptId", MySqlDbType.VarChar,50),
					new MySqlParameter("@province", MySqlDbType.VarChar,50),
					new MySqlParameter("@city", MySqlDbType.VarChar,50),
					new MySqlParameter("@country", MySqlDbType.VarChar,50),
					new MySqlParameter("@address", MySqlDbType.VarChar,50),
					new MySqlParameter("@contacter", MySqlDbType.VarChar,50),
					new MySqlParameter("@contacterphone", MySqlDbType.VarChar,50),
					new MySqlParameter("@supplierId", MySqlDbType.VarChar,50),
					new MySqlParameter("@maintypes", MySqlDbType.VarChar,50),
                    new MySqlParameter("@town", MySqlDbType.VarChar,50),
                     new MySqlParameter("@principal", MySqlDbType.VarChar,50),
                     new MySqlParameter("@principalphone", MySqlDbType.VarChar,50),
                      new MySqlParameter("@depttype", MySqlDbType.VarChar,50)
				
                                        };
            parameters[0].Value = model.deptId;
            parameters[1].Value = model.deptName;
            parameters[2].Value = model.deptLevel;
            parameters[3].Value = model.fkDeptId;
            parameters[4].Value = model.province;
            parameters[5].Value = model.city;
            parameters[6].Value = model.country;
            parameters[7].Value = model.address;
            parameters[8].Value = model.contacter;
            parameters[9].Value = model.contacterphone;
            parameters[10].Value = model.supplierId;
            parameters[11].Value = model.maintypes;
            parameters[12].Value = model.town;
            parameters[13].Value = model.principal;
            parameters[14].Value = model.principalphone;
            parameters[15].Value = model.depttype;

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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(FoodSafety.Model.Sys_client_sysdept model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE sys_client_sysdept set ");
            strSql.Append("deptName=@deptName,");
            strSql.Append("address=@address,");
            strSql.Append("depttype=@depttype,");
            strSql.Append("contacter=@contacter,");
            strSql.Append("contacterphone=@contacterphone,");
            strSql.Append("supplierId=@supplierId,");
            strSql.Append("province=@province,");
            strSql.Append("city=@city,");
            strSql.Append("country=@country,");
            strSql.Append("town=@town,");
            strSql.Append("maintypes=@maintypes,");
            strSql.Append("principal=@principal,");
            strSql.Append("principalphone=@principalphone ");
            strSql.Append("  where deptId=@deptId ");

            MySqlParameter[] parameters = {
					new MySqlParameter("@deptName", MySqlDbType.VarChar,50),
					new MySqlParameter("@address", MySqlDbType.VarChar,50),
					new MySqlParameter("@depttype", MySqlDbType.VarChar,50),
					new MySqlParameter("@contacter", MySqlDbType.VarChar,50),
					new MySqlParameter("@contacterphone", MySqlDbType.VarChar,50),
                    new MySqlParameter("@supplierId", MySqlDbType.VarChar,50),
                    new MySqlParameter("@province", MySqlDbType.VarChar,50),
                    new MySqlParameter("@city", MySqlDbType.VarChar,50),
                    new MySqlParameter("@country", MySqlDbType.VarChar,50),
                    new MySqlParameter("@town", MySqlDbType.VarChar,50),
                        new MySqlParameter("@maintypes", MySqlDbType.VarChar,50),
                            new MySqlParameter("@principal", MySqlDbType.VarChar,50),
                              new MySqlParameter("@principalphone", MySqlDbType.VarChar,50),
                    new MySqlParameter("@deptId", MySqlDbType.VarChar,50)
                                          };
            parameters[0].Value = model.deptName;
            parameters[1].Value = model.address;
            parameters[2].Value = model.depttype;
            parameters[3].Value = model.contacter;
            parameters[4].Value = model.contacterphone;
            parameters[5].Value = model.supplierId;
            parameters[6].Value = model.province;
            parameters[7].Value = model.city;
            parameters[8].Value = model.country;
            parameters[9].Value = model.town;
            parameters[10].Value = model.maintypes;
            parameters[11].Value = model.principal;
            parameters[12].Value = model.principalphone;
            parameters[13].Value = model.deptId;
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
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(string deptId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_client_sysdept ");
            strSql.Append(" where deptId=@deptId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@deptId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = deptId;

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

        public static bool ExistsDept(string deptId)
        {
            string sql = string.Format("select count(deptId) from sys_client_sysdept where fkDeptId = '{0}'", deptId);
            try
            {
                return DbHelperMySQL.CreateDbHelper().Exists(sql);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetSingle异常");
                throw new Exception(e.Message);
            }
        }
        public static string GetDeptName(string fkDeptId)
        {
            string sql = string.Format("select deptName FROM sys_client_sysdept WHERE deptId = '{0}'", fkDeptId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetSingle(sql).ToString();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetDeptName异常");
                throw new Exception(e.Message);
            }
        }
        public static bool ExistsCity(string cityName)
        {
            string sql = string.Format("SELECT count(cityId) from sys_city where cityName ='{0}'", cityName);

            try
            {
                return DbHelperMySQL.CreateDbHelper().Exists(sql);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExistsCity异常");
                throw new Exception(e.Message);
            }
        }
        public static string GetCityId(string cityName,string pid)
        {
            string sql = string.Format("SELECT cityId from sys_city where cityName ='{0}' and pid = '{1}'", cityName,pid);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetSingle(sql).ToString();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetDeptName异常");
                throw new Exception(e.Message);
            }
        }

        public static bool ExistsDeleteDept(string deptId)
        {
            string sql = string.Format("select f_delete_dept_check('{0}')", deptId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().Exists(sql);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetSingle异常");
                throw new Exception(e.Message);
            }
        }
        public static bool UpdateClientDept(string title, string deptId)
        {
            string sql = String.Format("update sys_client_sysdept set title = '{0}' where deptId='{1}'", title,deptId);

            int rows = DbHelperMySQL.CreateDbHelper().ExecuteSql(sql);
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
