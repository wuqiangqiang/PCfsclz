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
    public static class SysInfoQueryDal
    {
        public static DataTable GetComboDetItemAnimal()
        {
            string strSql = string.Format("SELECT itemId,itemName FROM t_det_item_animal where openFlag = '1'");
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComboDetItemAnimal异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable GetComboDetObjectAnimal()
        {
            string strSql = string.Format("SELECT  objectId,objectName FROM t_det_object_animal where openFlag = '1'");
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComboDetObjectAnimal异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable GetComboDetResult()
        {
            string strSql = string.Format("SELECT  resultId,resultName FROM t_det_result where openFlag = '1' order by orderid");
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComboDetResult异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable GetComboDetReagentAnimal()
        {
            string strSql = string.Format("SELECT  reagentId,reagentName FROM t_det_reagent_animal where openFlag = '1'");
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComboDetReagentAnimal异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable GetComboDetSource()
        {
            string strSql = string.Format("SELECT  sourceId,sourceName FROM t_det_source  where openFlag = '1'");
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComboDetSource异常");
                throw new Exception(e.Message);
            }
        }
        
        public static DataTable GetComboDetItemProduce()
        {
            string strSql = string.Format("SELECT itemId,itemName FROM t_det_item_produce where openFlag = '1'");
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComboDetItemAnimal异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable GetComboDetObjectProduce()
        {
            string strSql = string.Format("SELECT  objectId,objectName FROM t_det_object_produce where openFlag = '1'");
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComboDetObjectAnimal异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable GetComboDetReagentProduce()
        {
            string strSql = string.Format("SELECT  reagentId,reagentName FROM t_det_reagent_produce where openFlag = '1'");
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComboDetReagentProduce异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable GetComboDetReagentProduceMode(string mode)
        {
            string strSql = string.Empty;
            switch (mode)
            {
                case "0":
                    strSql = string.Format("SELECT reagentId,reagentName FROM t_det_reagent_produce WHERE openFlag = '1' and detectMode = '0'");
                    break;
                case "1":
                    strSql = strSql = string.Format("SELECT reagentId,reagentName FROM t_det_reagent_produce WHERE openFlag = '1' and detectMode = '1'");
                    break;
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComboDetReagentProduceMode异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable GetComboDetReviewReagent(string category)
        {
            string strSql = string.Empty;
            switch (category)
            {
                case "f":
                    strSql = string.Format("SELECT reagentId,reagentName FROM t_det_reagent_fishery WHERE openFlag = '1' and reagentId <> '1'");
                    break;
                case "a":
                    strSql = string.Format("SELECT reagentId,reagentName FROM t_det_reagent_animal WHERE openFlag = '1' and reagentId <> '1'");
                    break;
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComboDetReviewReagent异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable GetComboDetItemFishery()
        {
            string strSql = string.Format("SELECT itemId,itemName FROM t_det_item_fishery where openFlag = '1'");
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComboDetItemAnimal异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable GetComboDetObjectFishery()
        {
            string strSql = string.Format("SELECT  objectId,objectName FROM t_det_object_fishery where openFlag = '1'");
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComboDetObjectAnimal异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable GetComboDetReagentFishery()
        {
            string strSql = string.Format("SELECT  reagentId,reagentName FROM t_det_reagent_fishery where openFlag = '1'");
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComboDetReagentAnimal异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable GetComboDetSample(string category)
        {
            string strSql = string.Empty;
            switch (category)
            {
                case "f":
                    strSql = string.Format("SELECT sampleId,sampleName FROM t_det_sample_fishery WHERE openFlag = '1'");
                    break;
                case "a":
                    strSql = string.Format("SELECT sampleId,sampleName FROM t_det_sample_animal WHERE openFlag = '1'");
                    break;
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComboDetSample异常");
                throw new Exception(e.Message);
            }
        }


        public static DataTable GetComboSampleNo(string category,string userId)
        {
            string strSql = string.Empty;
            switch (category)
            {
                case "p":
                    strSql = string.Format("call p_sampleno_produce('{0}')", userId);
                    break;
                case "f":
                    strSql = string.Format("call p_sampleno_fishery('{0}')", userId);
                    break;
                case "a":
                    strSql = string.Format("call p_sampleno_animal('{0}')", userId);
                    break;
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComboSampleNo异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable GetComboUserCompany(string userId)
        {
            string strSql = string.Format("SELECT  companyId,companyName FROM v_user_company WHERE userId ='{0}'",userId);
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComboUserCompany异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable ExecuteProDetect(string category,int id)
        {
            string sql = string.Empty;
            switch (category)
            {case "p":
                    sql = string.Format("call p_detect_details_produce('{0}')", id);
                    break;
                case "f":
                    sql = string.Format("call p_detect_details_fishery('{0}')", id);
                    break;
                case "a":
                    sql = string.Format("call p_detect_details_animal('{0}')", id);
                    break;

            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProDetect异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable GetComboExecuteProDetect(string category,object detItem,object detObject)
        {
            string sql = string.Empty;
            switch (category)
            {
                case "a":
                    sql = string.Format("call p_detect_sensitivity_animal('{0}','{1}')", detItem, detObject);
                    break;
                case "f":
                    sql = string.Format("call p_detect_sensitivity_fishery('{0}','{1}')", detItem, detObject);
                    break;
              
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetComboExecuteProDetect异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable ExecuteProDetectDetailsAnimal(int id)
        {
            string sql = string.Format("call p_detect_details_animal('{0}')", id);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProDetectDetailsAnimal异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProDetectDetailsFishery(int id)
        {
            string sql = string.Format("call p_detect_details_fishery('{0}')", id);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProDetectDetailsFishery异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable ExecuteProQueryDetectAnimal(string f0,string f1,string f2,string f3,string f4,string f5,string f6,string f7,string f8,string f9,string f10,string f11,string f12,string f13,int f14,int f15)
        {
            string sql = string.Format("call p_query_detect_animal({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',{14},{15})",f0,f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,f13,f14,f15);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProDetectDetailsAnimal异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProQueryDetectProduce(string f0, string f1, string f2, string f3, string f4, string f5, string f6, string f7, string f8, string f9, string f10, string f11, string f12, string f13, int f14, int f15)
        {
            string sql = string.Format("call p_query_detect_produce({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',{14},{15})", f0, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProQueryDetectProduce异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProQueryDetectFishery(string f0, string f1, string f2, string f3, string f4, string f5, string f6, string f7, string f8, string f9, string f10, string f11, string f12, string f13, int f14, int f15)
        {
            string sql = string.Format("call p_query_detect_fishery({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',{14},{15})", f0, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProQueryDetectFishery异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable ExecuteProDayReport(string function, string id, DateTime date, string item,
                                                    string itemId, string resultId)
        {
            string sql = string.Format("call {0}('{1}','{2}','{3}','{4}','{5}')", function,id,date,item,itemId,resultId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProDayReport异常");
                throw new Exception(e.Message);
            }
        }
        //category:农产品p  水产品f  畜产品a 根据不同类别分别执行农、水、畜日报详情存储过程
        public static DataTable ExecuteProDayReportDetails(string category, string sj, string deptId, string itemId,
                                                           string resultId, int rowN, int tRowN)
        {
            string sql = string.Empty;
            switch(category)
            {
                case "p":
                    sql = string.Format("call  p_report_day_details_produce('{0}','{1}','{2}','{3}',{4},{5})", sj, deptId, itemId, resultId, rowN, tRowN);
                    break;
                case "f":
                    sql = string.Format("call  p_report_day_details_fishery('{0}','{1}','{2}','{3}',{4},{5})", sj, deptId, itemId, resultId, rowN, tRowN);
                    break;
                case "a":
                    sql = string.Format("call  p_report_day_details_animal('{0}','{1}','{2}','{3}',{4},{5})", sj, deptId,
                                        itemId, resultId, rowN, tRowN);
                    break;
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProDayReportDetails异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable ExecuteProDayReportCountry(string function,string sj,string deptId,string itemId,string resultId)
        {
            string sql = string.Format("call {0}('{1}','{2}','{3}','{4}')", function, sj, deptId, itemId, resultId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProDayReportCountry异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProMonthReport(string function, string id, string date, string item,
                                                 string itemId, string resultId)
        {
            string sql = string.Format("call {0}('{1}','{2}','{3}','{4}','{5}')", function, id, date, item, itemId, resultId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProMonthReport异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProMonthReportCountry(string function, string sj, string deptId, string itemId, string resultId)
        {
            string sql = string.Format("call {0}('{1}','{2}','{3}','{4}')", function, sj, deptId, itemId, resultId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProDayReportCountry异常");
                throw new Exception(e.Message);
            }
        }

        //category:农产品p  水产品f  畜产品a 根据不同类别分别执行农、水、畜月报详情存储过程
        public static DataTable ExecuteProMonthReportDetails(string category,string sj, string deptId, string itemId,
                                                           string resultId, int rowN, int tRowN)
        {
            string sql = string.Empty;
            switch (category)
            {
                case "p":
                    sql = string.Format("call  p_report_month_details_produce('{0}','{1}','{2}','{3}',{4},{5})", sj, deptId, itemId, resultId, rowN, tRowN);
                    break;
                case "f":
                    sql = string.Format("call  p_report_month_details_fishery('{0}','{1}','{2}','{3}',{4},{5})", sj, deptId, itemId, resultId, rowN, tRowN);
                    break;
                case "a":
                    sql = string.Format("call  p_report_month_details_animal('{0}','{1}','{2}','{3}',{4},{5})", sj, deptId,
                                        itemId, resultId, rowN, tRowN);
                    break;
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProMonthReportDetails异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable ExecuteProYearReport(string function, string id, string startDate,string endDate, string item,string itemId, string resultId,string objectId)
        {
            string sql = string.Format("call {0}('{1}','{2}','{3}','{4}','{5}','{6}','{7}')", function, id, startDate,endDate, item, itemId, resultId,objectId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteYearReport异常");
                throw new Exception(e.Message);
            }
        }
        //category:农产品p  水产品f  畜产品a 根据不同类别分别执行农、水、畜自定义报详情存储过程
        public static DataTable ExecuteProYearReportDetails(string category, string startDate, string endDate, string DeptId,
                                                 string itemId, string resultId, string objectId, int rowN, int tRowN)
        {
            string sql = string.Empty;
            switch (category)
            {
                case "p":
                    sql = string.Format("call  p_report_year_details_produce('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7})",startDate, endDate, DeptId,itemId,resultId,objectId,rowN,tRowN);
                    break;
                case "f":
                    sql = string.Format("call  p_report_year_details_fishery('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7})", startDate, endDate, DeptId, itemId, resultId, objectId, rowN, tRowN);
                    break;
                case "a":
                    sql = string.Format("call  p_report_year_details_animal('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7})", startDate, endDate, DeptId, itemId, resultId, objectId, rowN, tRowN);
                    break;
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProYearReportDetails异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProYearReportCountry(string function, string kssj,string jssj, string deptId, string itemId, string resultId,string objectId)
        {
            string sql = string.Format("call {0}('{1}','{2}','{3}','{4}','{5}','{6}')", function, kssj,jssj, deptId, itemId, resultId,objectId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProYearReportCountry异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable ExecuteProComparisonAnalysis(string function,string userId,DateTime startDate,DateTime endDate)
        {
            string sql = string.Format("call {0}({1},'{2}','{3}')", function, userId, startDate, endDate);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProComparisonAnalysis异常");
                throw new Exception(e.Message);
            }
        }
        //depttype 根据不同类别执行不同存储过程
        public static DataTable ExecuteProTrendAnalysis(string deptType,string userId)
        {
            string sql = string.Empty;
            switch (deptType)
            {
                case "0":
                    sql = string.Format("call  p_year_count_produce({0})",userId);
                    break;
                case "1":
                    sql = string.Format("call  p_year_count_fishery({0})", userId);
                    break;
                case "2":
                    sql = string.Format("call  p_year_count_animal({0})", userId);
                    break;
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProTrendAnalysis异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable ExecuteProNdxmqs(string function,string userId,string year)
        {
            string sql = string.Format("call {0}('{1}','{2}')", function, userId, year);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProNdxmqs异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProQyfx(string function, string userId, DateTime startDate, DateTime endDate)
        {
            string sql = string.Format("call {0}('{1}','{2}','{3}')", function,userId, startDate, endDate);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProQyfx异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable ExecuteProTaskReport(string function,string userId,string year,string item,string itemId)
        {
            string sql = string.Format("call {0}('{1}','{2}','{3}','{4}')", function, userId, year, item,itemId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProTaskReport异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProTaskReportDetails(string category, string sj, string deptId, string itemId, int rowN, int tRowN)
        {
            string sql = string.Empty;
            switch (category)
            {
                case "p":
                    sql = string.Format("call  p_task_report_details_produce('{0}','{1}','{2}',{3},{4})", sj,deptId,itemId,rowN,tRowN);
                    break;
                case "f":
                    sql = string.Format("call  p_task_report_details_fishery('{0}','{1}','{2}',{3},{4})", sj, deptId, itemId, rowN, tRowN);
                    break;
                case "a":
                    sql = string.Format("call  p_task_report_details_animal('{0}','{1}','{2}',{3},{4})", sj, deptId, itemId, rowN, tRowN);
                    break;
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProTaskReportDetails异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable ExecuteProTaskReportCountry(string function,string sj,string deptId,string itemId)
        {
            string sql = string.Format("call {0}('{1}','{2}','{3}')", function, sj, deptId, itemId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProTaskReportCountry异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable ExecuteProWarningInfo(string function, string userId, int rowN, int tRowN)
        {
            string sql = string.Format("call {0}('{1}',{2},{3})", function, userId, rowN, tRowN);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProWwarningInfo异常");
                throw new Exception(e.Message);
            }
        }
        //category 根据农、水、畜执行不同存储过程
        public static DataTable ExecuteProWarningDetails(string category,string deptId, string itemId, string objectId, int rowN, int tRowN)
        {
            string sql = string.Empty;
            switch (category)
            {
                case "p":
                    sql = string.Format("call p_warning_details_produce('{0}','{1}','{2}',{3},{4})", deptId,itemId,objectId,rowN,tRowN);
                    break;
                case "f":
                    sql = string.Format("call p_warning_details_fishery('{0}','{1}','{2}',{3},{4})", deptId, itemId, objectId, rowN, tRowN);
                    break;
                case "a": 
                    sql = string.Format("call p_warning_details_animal('{0}','{1}','{2}',{3},{4})", deptId, itemId, objectId, rowN, tRowN);
                    break;
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProWarningDetails异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProWarningInfoCountry(string function,string deptId, string itemId, string objectId, int rowN, int tRowN)
        {
            string sql = string.Format("call {0}('{1}','{2}','{3}',{4},{5})", function,deptId, itemId,objectId,rowN,tRowN);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProWarningInfoCountry异常");
                throw new Exception(e.Message);
            }
        }
        //category 根据农、水、畜执行不同存储过程   预警复核
        public static DataTable ExecuteProReviewInfo(string category, string userId,DateTime startDate,DateTime endDate,string deptItem,string item,int rowN,int tRowN)
        {
            string sql = string.Empty;
            switch (category)
            {
                case "p":
                    sql = string.Format("call  p_review_details_produce('{0}','{1}','{2}','{3}','{4}',{5},{6})", userId, startDate, endDate,deptItem,item, rowN, tRowN);
                    break;
                case "f":
                    sql = string.Format("call p_review_details_fishery('{0}','{1}','{2}','{3}','{4}',{5},{6})", userId, startDate, endDate, deptItem, item, rowN, tRowN);
                    break;
                case "a":
                    sql = string.Format("call p_review_details_animal('{0}','{1}','{2}','{3}','{4}',{5},{6})", userId, startDate, endDate,deptItem,item, rowN, tRowN);
                    break;
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProReviewInfo异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProWarningReport(string function, string userId, DateTime startDate, DateTime endDate, string item, string itemId, string reviewId)
        {
            string sql = string.Format("call {0}('{1}','{2}','{3}','{4}','{5}','{6}')", function, userId, startDate, endDate,item,itemId,reviewId);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProWarningReport异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProWarningReportDetails(string category, string kssj, string jssj,string deptId,string itemId,string reviewFlag,int rowN,int tRowN)
        {
            string sql = string.Empty;
            switch (category)
            {
                case "p":
                    sql = string.Format("call p_warning_report_details_produce('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",kssj,jssj, deptId, itemId, reviewFlag, rowN, tRowN);
                    break;
                case "f":
                    sql = string.Format("call p_warning_report_details_fishery('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", kssj, jssj, deptId, itemId, reviewFlag, rowN, tRowN);
                    break;
                case "a":
                    sql = string.Format("call p_warning_report_details_animal('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", kssj, jssj, deptId, itemId, reviewFlag, rowN, tRowN);
                    break;
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProWarningReportDetails异常");
                throw new Exception(e.Message);
            }
        }

        public static string GetReviewFlag(string category,int detectId)
        {
            string sql = string.Empty;
            switch (category)
            {
                case "p":
                    sql = string.Format("select reviewFlag from t_detect_report_produce where detectId = '{0}'",detectId);
                    break;
                case "f":
                    sql = string.Format("select reviewFlag from t_detect_report_fishery where detectId = '{0}'", detectId);
                    break;
                case "a":
                    sql = string.Format("select reviewFlag from t_detect_report_animal where detectId = '{0}'", detectId);
                    break;
            }
            try
            {
                return DbHelperMySQL.CreateDbHelper().GetSingle(sql).ToString();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetReviewFlag异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProWarningReportCountry(string function, string kssj, string jssj,string deptId,string itemId,string reviewFlag,string deptType)
        {
            string sql = string.Format("call {0}('{1}','{2}','{3}','{4}','{5}')", function, kssj, jssj, deptId, itemId, reviewFlag, deptType);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProWarningReportCountry异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable ExecuteProReviewDetails(string kssj,string jssj,string deptId,string itemId,string reviewId,int rowN,int tRowN)
        {
            string sql = string.Format("call p_review_details('{0}','{1}','{2}','{3}','{4}',{5},{6})", kssj, jssj, deptId,itemId,reviewId,rowN,tRowN);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProReviewDetails异常");
                throw new Exception(e.Message);
            }
        }
        public static DataTable ExecuteProReviewLog(string function, string userId, DateTime kssj, DateTime jssj, string deptItem, string item, int rowN, int tRowN)
        {
            string sql = string.Format("call {0}('{1}','{2}','{3}','{4}','{5}',{6},{7})", function,userId, kssj, jssj,deptItem,item,rowN,tRowN );

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProReviewLog异常");
                throw new Exception(e.Message);
            }
        }

        public static DataTable ExecuteProQueryDevice(string userId,string deviceId,string deptType)
        {
            string sql = string.Format("call p_query_device('{0}','{1}','{2}')", userId, deviceId,deptType);

            try
            {
                return DbHelperMySQL.CreateDbHelper().GetDataSet(sql).Tables[0];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ExecuteProQueryDevice异常");
                throw new Exception(e.Message);
            }
        }
    }
}
