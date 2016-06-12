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
    public static class DetectDal
    {
        public static bool ExistsDetectReviewAnimal(string detectId)
        {
            string sql = string.Format("select count(id) from t_detect_review_animal where detectId = '{0}'", detectId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().Exists(sql);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExistsDetectReviewAnimal异常");
                throw new Exception(e.Message);
            }
        }
        public static bool DeleteDetectReviewAnimal(string detectId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_detect_review_animal ");
            strSql.Append(" where detectId=@detectId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@detectId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = detectId;

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
        public static bool DeleteDetectReportAnimal(string detectId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_detect_report_animal ");
            strSql.Append(" where detectId=@detectId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@detectId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = detectId;

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

        public static bool ExistsDetectReviewProduce(string detectId)
        {
            string sql = string.Format("select count(id) from t_detect_review_produce where detectId = '{0}'", detectId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().Exists(sql);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExistsDetectReviewProduce异常");
                throw new Exception(e.Message);
            }
        }
        public static bool DeleteDetectReviewProduce(string detectId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_detect_review_produce ");
            strSql.Append(" where detectId=@detectId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@detectId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = detectId;

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
        public static bool DeleteDetectReportProduce(string detectId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_detect_report_produce ");
            strSql.Append(" where detectId=@detectId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@detectId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = detectId;

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

        public static bool ExistsDetectReviewFishery(string detectId)
        {
            string sql = string.Format("select count(id) from t_detect_review_fishery where detectId = '{0}'", detectId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().Exists(sql);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExistsDetectReviewProduce异常");
                throw new Exception(e.Message);
            }
        }
        public static bool DeleteDetectReviewFishery(string detectId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_detect_review_fishery ");
            strSql.Append(" where detectId=@detectId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@detectId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = detectId;

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
        public static bool DeleteDetectReportFishery(string detectId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_detect_report_fishery ");
            strSql.Append(" where detectId=@detectId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@detectId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = detectId;

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

        public static bool AddUpdateDetectReport(string strSql)
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

        public static bool ExecuteProAddDetect(string f0,string f1,string f2,string f3,string f4,string f5,string f6,string f7,string f8,string f9)
        {
            string sql = string.Format("call p_insert_detect_produce('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", f0,f1,f2,f3,f4,f5,f6,f7,f8,f9);

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
        public static bool ExecuteProAddDetectFishery(string f0, string f1, string f2, string f3, string f4, string f5, string f6, string f7, string f8, string f9,DateTime f10)
        {
            string sql = string.Format("call p_insert_detect_fishery('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", f0, f1, f2, f3, f4, f5, f6, f7, f8, f9,f10);

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
        public static bool ExecuteProAddDetectAnimal(string f0, string f1, string f2, string f3, string f4, string f5, string f6, string f7, string f8, string f9, string f10,string f11,string f12,DateTime f13)
        {
            string sql = string.Format("call p_insert_detect_animal('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", f0, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10,f11,f12,f13);

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

        public static string ExecuteFunObjectTable(string deptId)
        {
            try
            {
                string sql = string.Format("select f_create_objectlable('{0}')",deptId);
                return DbHelperMySQL.CreateDbHelper().GetSingle(sql).ToString();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteFunObjectTable异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable GetDetItem(string category,string itemName)
        {
            string sql = string.Empty;

            switch (category)
            {case"p":
                    sql =
                        string.Format(
                            "select itemId,itemName,case when openFlag = '1' then ' 启用' else '禁用' end as openFlag  from t_det_item_produce where itemName like '{0}%'",itemName);
                    break;
                case "f":
                    sql = string.Format("select itemId,itemName,case when openFlag = '1' then ' 启用' else '禁用' end as openFlag  from t_det_item_fishery where itemName like '{0}%'",itemName);
                    break;
                case "a":
                    sql = string.Format("select itemId,itemName,case when openFlag = '1' then ' 启用' else '禁用' end as openFlag  from t_det_item_animal where itemName like '{0}%'", itemName);
                    break;
                    
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetDetItem异常");
                throw new Exception(e.Message);
            }
        }
        public static bool DeleteDetect(string category,string itemId)
        {
            string talbeName = string.Empty;
            switch (category)
            {
                case "p":
                    talbeName = "t_det_item_produce";
                    break;
                case "f":
                    talbeName = "t_det_item_fishery";
                    break;
                case "a":
                    talbeName = "t_det_item_animal";
                    break;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ");
            strSql.AppendFormat("{0}", talbeName);
            strSql.Append(" where itemId=@itemId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@itemId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = itemId;

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

        public static DataTable GetDetItemAll(string category, string itemId)
        {
            string talbeName = string.Empty;
            switch (category)
            {
                case "p":
                    talbeName = "t_det_item_produce";
                    break;
                case "f":
                    talbeName = "t_det_item_fishery";
                    break;
                case "a":
                    talbeName = "t_det_item_animal";
                    break;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select itemName,openFlag  from ");
            strSql.AppendFormat("{0}", talbeName);
            strSql.Append(" where itemId=@itemId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@itemId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = itemId;
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql.ToString(), parameters).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetDetItemAll异常");
                throw new Exception(e.Message);
                
            }
        }

        public static bool UpdateDetect(string category,string itemName,string openFlag,string itemId)
        {
            string talbeName = string.Empty;
            switch (category)
            {
                case "p":
                    talbeName = "t_det_item_produce";
                    break;
                case "f":
                    talbeName = "t_det_item_fishery";
                    break;
                case "a":
                    talbeName = "t_det_item_animal";
                    break;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ");
            strSql.AppendFormat("{0}", talbeName);
            strSql.Append(" set itemName =@itemName,");
            strSql.Append("openFlag = @openFlag ");
            strSql.Append("   where itemId=@itemId");
            MySqlParameter[] parameters = {
					new MySqlParameter("@itemName", MySqlDbType.VarChar,50),
	                new MySqlParameter("@openFlag", MySqlDbType.VarChar,50),
                    new MySqlParameter("@itemId", MySqlDbType.VarChar,50)	
                                          };
            parameters[0].Value = itemName;
            parameters[1].Value = openFlag;
            parameters[2].Value = itemId;
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

        public static bool AddDetect(string category,string itemName,string openFlag,string cuserId,DateTime cdate)
        {
            string talbeName = string.Empty;
            switch (category)
            {
                case "p":
                    talbeName = "t_det_item_produce";
                    break;
                case "f":
                    talbeName = "t_det_item_fishery";
                    break;
                case "a":
                    talbeName = "t_det_item_animal";
                    break;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ");
            strSql.AppendFormat("{0}", talbeName);
            strSql.Append(" (itemName,openFlag,cuserId,cdate)");
            strSql.Append("values(");
            strSql.Append("@itemName,@openFlag,@cuserId,@cdate)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@itemName", MySqlDbType.VarChar,50),
	                new MySqlParameter("@openFlag", MySqlDbType.VarChar,10),
                    new MySqlParameter("@cuserId", MySqlDbType.VarChar,50),
                    new MySqlParameter("@cdate", MySqlDbType.DateTime)	
                                          };
            parameters[0].Value = itemName;
            parameters[1].Value = openFlag;
            parameters[2].Value = cuserId;
            parameters[3].Value = cdate;
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
