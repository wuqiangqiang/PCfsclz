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
    public static class Sys_client_userDal
    {
        public static bool ExistsUserId(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(id) from sys_client_user ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().Exists(strSql.ToString());
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetSingle异常");
                throw new Exception(e.Message);
            }
        }

        public static bool DeleteClientUser(string id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_client_user ");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.VarChar,50)			};
            parameters[0].Value = id;

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
        /// 增加一条数据
        /// </summary>
        public static bool AddClientUser(FoodSafety.Model.sys_client_user model)
        {
            
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_client_user(");
            strSql.Append("userId,userName,userPassword,deptId,cdate,roleId,cuserId,expired)");
            strSql.Append(" values (");
            strSql.Append("@userId,@userName,@userPassword,@deptId,@cdate,@roleId,@cuserId,@expired)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@userId", MySqlDbType.VarChar,50), 
					new MySqlParameter("@userName", MySqlDbType.VarChar,50),
					new MySqlParameter("@userPassword", MySqlDbType.VarChar,1),
					new MySqlParameter("@deptId", MySqlDbType.VarChar,50),
					new MySqlParameter("@cdate", MySqlDbType.DateTime),
					new MySqlParameter("@roleId", MySqlDbType.VarChar,50),
					new MySqlParameter("@cuserId", MySqlDbType.VarChar,50),
					new MySqlParameter("@expired", MySqlDbType.VarChar,50)
				
				
                                        };
            parameters[0].Value = model.userId;
            parameters[1].Value = model.userName;
            parameters[2].Value = model.userPassword;
            parameters[3].Value = model.deptId;
            parameters[4].Value = model.cdate;
            parameters[5].Value = model.roleId;
            parameters[6].Value = model.cuserId;
            parameters[7].Value = model.expired;

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

        public static bool AddUpdateClientUser(string strSql)
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

        public static DataTable GetSysClientUser(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,userName,userId FROM sys_client_user ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql.ToString()).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetClientUser异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable  GetUserModel(FoodSafety.Model.sys_client_user loginUser)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,userId,UserName,UserPassword,expired from sys_client_user ");
            strSql.Append(" where userId=@userId and UserPassword=@Password");
            MySqlParameter[] parameters = {
					new MySqlParameter("@userId", MySqlDbType.VarChar,20), 
                    new MySqlParameter("@Password",MySqlDbType.VarChar,50)
                                        };
            parameters[0].Value = loginUser.userId;
            parameters[1].Value = loginUser.userPassword;


            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql.ToString(), parameters).Tables[0];
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
