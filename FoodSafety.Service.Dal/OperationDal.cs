using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodSafety.Model;
using System.Data;
using DBUtility;
using MySql.Data.MySqlClient;

namespace FoodSafety.Service.Dal
{
    public static class OperationDal
    {
        //private static string userId;

        public static UserInfo GetMenu(string loginName, string password)
        {
            UserInfo userInfo = new UserInfo();

            StringBuilder sb = new StringBuilder();
            sb.Append("select id uid, userName uname, deptId deptid,deptLevel flagtier,supplierId, subName menu,userId ")
              .Append("from v_user_sub ")
              .AppendFormat("where userId ='{0}' and  userPassword = '{1}'", loginName, password);
            string sql = sb.ToString();
            try
            {
                DataTable table = DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];

                if (table.Rows.Count == 1)
                {
                    //userId = table.Rows[0][0].ToString();
                    userInfo.ID = table.Rows[0][0].ToString();
                    userInfo.ShowName = table.Rows[0][1].ToString();
                    userInfo.DepartmentID = table.Rows[0][2].ToString();
                    userInfo.FlagTier = table.Rows[0][3].ToString();
                    userInfo.SupplierId = table.Rows[0][4].ToString();
                    userInfo.Menus.AddRange(table.Rows[0][5].ToString().Split(new char[] { ',' }));
                    userInfo.LoginName = table.Rows[0][6].ToString();
                }


                return userInfo;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetMenu异常");
                throw new Exception(e.Message);
            }
        }

        //根据用户ID获取树型部门列表
        public static DataTable GetDepartment(string userId)
        {
            string sql = string.Format("call p_get_department('{0}')", userId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetDepartment异常");
                throw new Exception(e.Message);
            }
        }

        //获取省市信息
        public static DataTable GetProvinceCity()
        {
            string sql = "select cityId,cityName,pid from sys_city";

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetProvinceCity异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable GetComparisonAndAnalysiseData(string userId, string theme, DateTime startTime, DateTime endTime)
        {
            string sql = string.Format("select t.info_name,t.report_count " +
                "from (select info_name,count(*) report_count " +
                "from (select INFO_CODE,INFO_NAME " +
                "from sys_client_sysdept where INFO_CODE like '{0}%' and flag_tier = 4) m " +
                "INNER JOIN t_detect_report n on m.INFO_CODE = n.DETECTID and " +
                "TO_DAYS(DETECTDATE) BETWEEN TO_DAYS('{1}') and TO_DAYS('{2}')  GROUP BY info_code) t ",
                userId, startTime.ToShortDateString(), endTime.ToShortDateString());
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComparisonAndAnalysiseData异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable GetCompany(string userId)
        {
            try
            {
                string sql = string.Format("call p_user_company_details('{0}')", userId);
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetCompany异常");
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取软件的最新版本
        /// </summary>
        /// <param name="supplierid"></param>
        /// <returns></returns>
        public static string GetNewVersion(string supplierid) 
        {
            try
            {
                string sql = string.Format("select version from t_version where id = (select max(id) from t_version where supplierId ='{0}')", supplierid);
                return DbHelperMySQL.CreateDbHelper().GetSingle(sql).ToString();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetNewVersion异常");
                throw new Exception(e.Message);
            }
        }
        //用户的查看权限
        public static DataTable GetRolePermission(string userid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT rp.subId,s.subName,s.fkSubId,s.subUrl,s.subSelectUrl ")
              .Append("FROM sys_sub s ,sys_rolepermission rp , sys_client_user u  ")
              .Append("WHERE s.subId = rp.subId  and rp.roleId = u.roleId ")
              .AppendFormat(" and u.id='{0}' order by s.subOrderId asc",userid);

        
            string sql = sb.ToString();
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetProvinceCity异常");
                throw new Exception(e.Message);
            }

        }

        public static DataTable GetUserSupplier(string supplierId)
        {
            string sql = string.Format("select companyName,phone from t_supplier where supplierId ='{0}'",supplierId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetProvinceCity异常");
                throw new Exception(e.Message);
            }
        }
       
        public static DataTable GetDeptProvinceCity(string deptid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select proviceId as id , cityName ")
              .Append(" from t_set_area LEFT JOIN sys_city  ON t_set_area.proviceId = sys_city.cityId ")
              .AppendFormat(" where  deptId ='{0}'", deptid);


            string sql = sb.ToString();
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetDeptProvinceCity异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable GetDeptCompany(string companyId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select companyName,t_company.deptId,sys_client_sysdept.deptName,areaId,t_company.contacter,t_company.phone  ")
              .Append(" from t_company left join sys_client_sysdept ").Append(" on t_company.deptId = sys_client_sysdept.deptId")
              .AppendFormat(" where  companyId ='{0}'", companyId);


            string sql = sb.ToString();
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetDeptCompany异常");
                throw new Exception(e.Message);
            }
        }
        public static string GetProvince(string deptid)
        {
            string sql = string.Format("select province from sys_client_sysdept where deptId='{0}'", deptid);
          
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetSingle(sql).ToString();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetProvince异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable GetUserSampleProduce(string userid, string dtStart, string dtEnd, string sampleNo)
        {
            string sql = string.Format("call p_user_sample_produce('{0}','{1}','{2}','{3}')", userid, dtStart, dtEnd,sampleNo);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetUserSampleProduce异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable GetUserSampleFishery(string userid, string dtStart, string dtEnd, string sampleNo)
        {
            string sql = string.Format("call p_user_sample_fishery('{0}','{1}','{2}','{3}')", userid, dtStart, dtEnd,sampleNo);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetUserSampleFishery异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable GetUserSampleAnimal(string userid, string dtStart, string dtEnd,string sampleNo)
        {
            string sql = string.Format("call p_user_sample_animal('{0}','{1}','{2}','{3}')", userid, dtStart, dtEnd,sampleNo);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetUserSampleAnimal异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable GetSupplier()
        {
            string sql = string.Format("select supplierId,supplierName from t_supplier");

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetSupplier异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable GetSysDept()
        {
            string sql = string.Format("select deptId,deptName from sys_client_sysdept");

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetSysDept异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable GetClientUser(string deptId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select id,userId,sys_client_user.userName,sys_client_sysdept.deptName,sys_client_role.roleName as role_expl ")
            .Append(" FROM sys_client_user ,sys_client_sysdept,sys_client_role ")
            .Append("WHERE sys_client_user.deptId = sys_client_sysdept.deptId ")
            .Append("and sys_client_user.roleId = sys_client_role.roleId ")
            .AppendFormat("and sys_client_user.deptId ='{0}'", deptId);
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sb.ToString()).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetClientUser异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable GetRole(string strWhere)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT roleId,roleName FROM sys_client_role ");
          	if(strWhere.Trim()!="")
			{
				sb.Append(" where "+strWhere);
			}
         
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sb.ToString()).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetRole异常");
                throw new Exception(e.Message);
            }
        }

        public static string GetSubName(string roleId)
        {
            string sql = string.Format("select GROUP_CONCAT(subName) as subName from v_sub_details where roleId = '{0}' and fkSubId = '0'", roleId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetSingle(sql).ToString();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetProvince异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable GetClientUserDept(string deptId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT id,userId,userName,userPassword,sys_client_user.deptId,sys_client_sysdept.deptName,roleId,expired ")
            .Append(" FROM sys_client_user ,sys_client_sysdept  ")
            .Append(" WHERE sys_client_user.deptId = sys_client_sysdept.deptId  ")
            .AppendFormat("AND sys_client_user.id  ='{0}'", deptId);
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sb.ToString()).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetClientUser异常");
                throw new Exception(e.Message);
            }
        }

        public static int LoadPicture(string deptId, byte[] img)
        {

            try
            {
                return DbHelperMySQL.CreateDbHelper().Load_Picture(deptId,img);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetRoleSub异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable GetClientLog(string strSql)
        {
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetClientUser异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable ExecuteProSjhc(string Id,string kssj,string jssj)
        {
            string sql = string.Format("call p_user_sjhc('{0}','{1}','{2}')",Id,kssj,jssj);
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetClientUser异常");
                throw new Exception(e.Message);
            }
        }

        public static bool AddSjhc(string strSql)
        {
            int rows = DbHelperMySQL.CreateDbHelper().ExecuteSql(strSql);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DataTable ExecuteProCxtj(string userId)
        {
            string sql = string.Format("call p_dept_cxtj('{0}')", userId);
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProCxtj异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProCxtjAll(string userId)
        {
            string sql = string.Format("call p_dept_cxtj_all('{0}')", userId);
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProCxtjAll异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProDetUser(string userId)
        {
            string sql = string.Format("call p_user_detuser('{0}')", userId);
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProDetUser异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable ExecuteProSignIn(string f1,string f2,string f3,string f4,string f5,string f6,string f7)
        {
            string sql = string.Format("call p_user_sign_in({0},'{1}','{2}','{3}','{4}',{5},{6})", f1,f2,f3,f4,f5,f6,f7);
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProSignIn异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProSignDetails(string f1, string f2, string f3, int f4, int f5)
        {
            string sql = string.Format("call p_user_sign_details({0},'{1}','{2}',{3},{4})", f1, f2, f3, f4, f5);
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProSignDetails异常");
                throw new Exception(e.Message);
            }
        }

        public static string GetPictureUrl()
        {
            string sql = string.Format("select pictureurl from t_url");
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetSingle(sql).ToString();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetPictureUrl异常");
                throw new Exception(e.Message);
            }

        }
        public static string GetFirstPageUrl()
            {
                string sql = string.Format("select firstpageurl from t_url");
                try
                {
                    return DbHelperMySQL.CreateDbHelper().GetSingle(sql).ToString();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("GetPictureUrl异常");
                    throw new Exception(e.Message);
                }
        }
        public static string GetFisheryUrl()
        {
            string sql = string.Format("select fisheryUrl from t_url");
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetSingle(sql).ToString();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetFisheryUrl异常");
                throw new Exception(e.Message);
            }

        }
        public static string GetUrl(string id)
        {
            string sql = string.Format("select url from sys_sign_in where id = '{0}'",id);
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetSingle(sql).ToString();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetUrl异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable ExecuteProUserCompany(string userId,string field)
        {
            string sql = string.Format("call p_user_company('{0}','{1}')", userId,field);
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProUserCompany异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProUserDept(string userId)
        {
            string sql = string.Format("call p_user_dept('{0}')", userId);
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProUserDept异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable GetMessage(string strWhere)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT messageTitle,messageContent from t_message  ");
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
                System.Diagnostics.Debug.WriteLine("GetMessage异常");
                throw new Exception(e.Message);
            }
        }

        public static bool AddMessage(string f1,string f2,string f3,DateTime f4)
        {
            string sql = string.Format("insert into t_message(messagetitle,messagecontent,cuserid,cdate) values ('{0}','{1}','{2}','{3}')",f1, f2,f3, f4);
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

        public static string GetCompanyArea(string companyId)
        {
            string sql = string.Format("SELECT areaId from t_company where companyId ='{0}'", companyId);
            try
            {

                return DbHelperMySQL.CreateDbHelper().GetSingle(sql).ToString();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetCompanyArea异常");
                throw new Exception(e.Message);
            }
           
        }

        public static string GetCompanyId(string companyId,string deptId)
        {
            string strSql = string.Format( "SELECT companyId from t_company where companyName ='{0}' and deptId = '{1}' ", companyId, deptId);
            try
            {

                return DbHelperMySQL.CreateDbHelper().GetSingle(strSql).ToString();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetCompanyId异常");
                throw new Exception(e.Message);
            }
        }

        public static bool AddCompany(string f1,string f2,string f3,string f4,string f5,DateTime f6)
        {
            string sql = string.Format("INSERT INTO t_company (companyName,areaId,openFlag,deptId,cuserId,cdate) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", f1, f2, f3, f4,f5,f6);
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

        public static DataTable ExecuteProQueryCompany(string f0,string f1,string f2,string f3,string f4,string f5)
        {
            string sql = string.Format("call p_query_company('{0}','{1}','{2}','{3}','{4}','{5}')",f0, f1, f2, f3, f4, f5);
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProQueryCompany异常");
                throw new Exception(e.Message);
            }
        }

        public static bool AddTCompany(string f0,string f1, string f2, string f3, string  f4, DateTime f5, string f6,string f7)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_company (");
            strSql.Append("itemName,openFlag,cuserId,cdate)");
            strSql.Append("values(");
            strSql.Append("@f0,@f1,@f2,@f3,@f4,@f5,@f6,@f7)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@f0", MySqlDbType.VarChar,50),
	                new MySqlParameter("@f1", MySqlDbType.VarChar,10),
                    new MySqlParameter("@f2", MySqlDbType.VarChar,50),
                    new MySqlParameter("@f3", MySqlDbType.VarChar,50),
                    new MySqlParameter("@f4", MySqlDbType.VarChar,50),
                    new MySqlParameter("@f5", MySqlDbType.DateTime),
                    new MySqlParameter("@f6", MySqlDbType.VarChar,50),
                    new MySqlParameter("@f7", MySqlDbType.VarChar,50)
                                          };
            parameters[0].Value = f0;
            parameters[1].Value = f1;
            parameters[2].Value = f2;
            parameters[3].Value = f3;
            parameters[4].Value = f4;
            parameters[5].Value = f5;
            parameters[6].Value = f6;
            parameters[7].Value = f7;
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

        public static bool UpdateTCompany(string companyName, string phone, string areaId, string contacter, string companyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_company set ");
            strSql.Append("companyName =@itemName,");
            strSql.Append("phone = @phone,");
            strSql.Append("areaId = @areaId,");
            strSql.Append("contacter = @contacter ");
            strSql.Append("   where companyId=@companyId");
            MySqlParameter[] parameters = {
					new MySqlParameter("@companyName", MySqlDbType.VarChar,50),
	                new MySqlParameter("@phone", MySqlDbType.VarChar,50),
                    new MySqlParameter("@areaId", MySqlDbType.VarChar,50),
	                new MySqlParameter("@contacter", MySqlDbType.VarChar,50),
                    new MySqlParameter("@companyId", MySqlDbType.VarChar,50)	
                                          };
            parameters[0].Value = companyName;
            parameters[1].Value = phone;
            parameters[2].Value = areaId;
            parameters[3].Value = contacter;
            parameters[4].Value = companyId;
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
    }
}
