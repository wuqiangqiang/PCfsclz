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
    public interface IOperationContract
    {
       
        /// <summary>
        /// 根据账号密码获取信息
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [OperationContract]
        UserInfo GetMenu(string loginName, string password);

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable GetDepartment(string userId);

        /// <summary>
        /// 获取省市结构表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable GetProvinceCity();

        /// <summary>
        /// 获取对比分析数据
        /// </summary>
        /// <param name="theme">主题</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">截止时间</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetComparisonAndAnalysiseData(string userId, string theme, DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取被检单位
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable GetCompany(string userId);

        /// <summary>
        /// 获取软件的最新版本
        /// </summary>
        /// <param name="supplierid"></param>
        /// <returns></returns>
        [OperationContract]
        string GetNewVersion(string supplierid);

        [OperationContract]
        DataTable GetRolePermission(string userid);

         [OperationContract]
        DataTable GetUserSupplier(string supplierId);

         [OperationContract]
         DataTable GetSupplier();

         [OperationContract]
         DataTable GetDeptProvinceCity(string detptid);

        //查找登录者部门所属的省份
         [OperationContract]
         String GetProvince(string detptid);
         //检测样本-农产品  
       [OperationContract]
         DataTable GetUserSampleProduce(string userid, string dtStart, string dtEnd,string sampleNo);
       //检测样本-水产品  
       [OperationContract]
       DataTable GetUserSampleFishery(string userid, string dtStart, string dtEnd, string sampleNo);
       //检测样本-畜产品  
       [OperationContract]
       DataTable GetUserSampleAnimal(string userid, string dtStart, string dtEnd,string sampleNo);
       [OperationContract]
       DataTable GetSysDept();
       [OperationContract]
       DataTable GetClientUser(string deptId);
       [OperationContract]
       DataTable GetRole(string strWhere);
       [OperationContract]
       DataTable GetClientRole(string strWhere);
        [OperationContract]
        string GetSubName(string roleId);

        [OperationContract]
        DataTable GetClientUserDept(string deptId);
        [OperationContract]
        DataTable GetRoleSub();
        [OperationContract]
        DataTable GetUserRoleMenu(string id);
        [OperationContract]
        int ExecuteFunRoleSub(string field1, string field2);
        [OperationContract]
        DataTable GetClientLog(string strSql);
        [OperationContract]
        DataTable ExecuteProSjhc(string Id, string kssj, string jssj);
        [OperationContract]
        DataTable ExecuteProCxtj(string userId);
        [OperationContract]
        DataTable ExecuteProDetUser(string userId);

        [OperationContract]
        DataTable GetSysClientUser(string strWhere);
        [OperationContract]
        DataTable ExecuteProSignIn(string f1, string f2, string f3, string f4, string f5, string f6,string f7);
        [OperationContract]
        DataTable ExecuteProSignDetails(string f1, string f2, string f3, int f4, int f5);

        [OperationContract]
        string  GetPictureUrl();
        [OperationContract]
        string GetUrl(string id);

        [OperationContract]
        string GetFirstPageUrl();
        [OperationContract]
        DataTable ExecuteProUserCompany(string userId, string field);

        [OperationContract]
        DataTable ExecuteProUserDept(string userId);

        [OperationContract]
        DataTable ExecuteProDetect(string category, int id);
         [OperationContract]
        DataTable ExecuteProDetectDetailsAnimal(int id);
          [OperationContract]
        DataTable ExecuteProDetectDetailsFishery(int id);
        [OperationContract]
        string GetFisheryUrl();
        [OperationContract]
        DataTable GetComboDetItemAnimal();
        [OperationContract]
        DataTable GetComboDetObjectAnimal();
        [OperationContract]
        DataTable GetComboDetReagentAnimal();
        [OperationContract]
        DataTable GetComboDetResult();
        [OperationContract]
        DataTable GetComboDetSource();
        [OperationContract]
        DataTable ExecuteProQueryDetectAnimal(string f0, string f1, string f2, string f3, string f4,
                                                     string f5, string f6, string f7, string f8, string f9,
                                                     string f10, string f11, string f12, string f13, int f14,
                                                     int f15);
        [OperationContract]
        DataTable GetComboDetItemProduce();
        [OperationContract]
        DataTable GetComboDetObjectProduce();
        [OperationContract]
        DataTable GetComboDetReagentProduce();
        [OperationContract]
        DataTable ExecuteProQueryDetectProduce(string f0, string f1, string f2, string f3, string f4,
                                                     string f5, string f6, string f7, string f8, string f9,
                                                     string f10, string f11, string f12, string f13, int f14,
                                                     int f15);
        [OperationContract]
        DataTable GetComboDetItemFishery();
        [OperationContract]
        DataTable GetComboDetObjectFishery();
        [OperationContract]
        DataTable GetComboDetReagentFishery();
        [OperationContract]
        DataTable ExecuteProQueryDetectFishery(string f0, string f1, string f2, string f3, string f4,
                                                     string f5, string f6, string f7, string f8, string f9,
                                                     string f10, string f11, string f12, string f13, int f14,
                                                     int f15);
        [OperationContract]
        DataTable GetMessage(string strWhere);

        [OperationContract]
        bool AddMessage(string f1, string f2, string f3, DateTime f4);

        [OperationContract]
        DataTable ExecuteProDayReport(string function, string id, DateTime date, string item,
                                      string itemId, string resultId);

        [OperationContract]
        DataTable ExecuteProDayReportDetails(string category, string sj, string deptId, string itemId,
                                             string resultId, int rowN, int tRowN);

        [OperationContract]
        DataTable ExecuteProDayReportCountry(string function, string sj, string deptId, string itemId,
                                             string resultId);
        [OperationContract]
        DataTable ExecuteProMonthReport(string function, string id, string date, string item,
                                      string itemId, string resultId);
        [OperationContract]
        DataTable ExecuteProMonthReportCountry(string function, string sj, string deptId, string itemId,
                                             string resultId);
        [OperationContract]
        DataTable ExecuteProMonthReportDetails(string category, string sj, string deptId, string itemId,
                                             string resultId, int rowN, int tRowN);

        [OperationContract]
        DataTable ExecuteProYearReport(string function, string id, string startDate, string endDate, string item,
                                       string itemId, string resultId, string objectId);

        [OperationContract]
        DataTable ExecuteProYearReportDetails(string category, string startDate, string endDate,
                                              string DeptId,
                                              string itemId, string resultId, string objectId, int rowN,
                                              int tRowN);

        [OperationContract]
        DataTable ExecuteProYearReportCountry(string function, string kssj, string jssj, string deptId, string itemId,
                                              string resultId, string objectId);

        [OperationContract]
        DataTable ExecuteProComparisonAnalysis(string function, string userId, DateTime startDate,
                                               DateTime endDate);

        [OperationContract]
        DataTable ExecuteProTrendAnalysis(string deptType, string userId);
        [OperationContract]
        DataTable ExecuteProNdxmqs(string function, string userId, string year);

        [OperationContract]
        DataTable ExecuteProQyfx(string function, string userId, DateTime startDate, DateTime endDate);

        [OperationContract]
        DataTable ExecuteProTaskReport(string function, string userId, string year, string item,
                                       string itemId);

        [OperationContract]
        DataTable ExecuteProTaskReportDetails(string category, string sj, string deptId, string itemId,
                                              int rowN, int tRowN);

        [OperationContract]
        DataTable ExecuteProTaskReportCountry(string function, string sj, string deptId, string itemId);

        [OperationContract]
        DataTable ExecuteProWarningInfo(string function, string userId, int rowN, int tRowN);

        [OperationContract]
        DataTable ExecuteProWarningDetails(string category, string deptId, string itemId, string objectId,
                                            int rowN, int tRowN);

        [OperationContract]
        DataTable ExecuteProWarningInfoCountry(string function, string deptId, string itemId,
                                               string objectId, int rowN, int tRowN);

        [OperationContract]
        DataTable ExecuteProReviewInfo(string category, string userId, DateTime startDate,
                                       DateTime endDate, string deptItem, string item, int rowN, int tRowN);

        [OperationContract]
        DataTable ExecuteProCxtjAll(string userId);

        [OperationContract]
        DataTable ExecuteProWarningReport(string function, string userId, DateTime startDate,
                                          DateTime endDate, string item, string itemId, string reviewId);

        [OperationContract]
        DataTable ExecuteProWarningReportDetails(string category, string kssj, string jssj, string deptId,
                                                 string itemId, string reviewFlag, int rowN, int tRowN);

        [OperationContract]
        string GetReviewFlag(string category, int detectId);

        [OperationContract]
        DataTable ExecuteProWarningReportCountry(string function, string kssj, string jssj, string deptId,
                                                 string itemId, string reviewFlag, string deptType);

        [OperationContract]
        DataTable ExecuteProReviewDetails(string kssj, string jssj, string deptId, string itemId,
                                          string reviewId, int rowN, int tRowN);

        [OperationContract]
        DataTable ExecuteProReviewLog(string function, string userId, DateTime kssj, DateTime jssj,
                                      string deptItem, string item, int rowN, int tRowN);

        [OperationContract]
        DataTable ExecuteProQueryDevice(string userId, string deviceId, string deptType);

        [OperationContract]
        DataTable GetComboDetReagentProduceMode(string mode);

        [OperationContract]
        string GetCompanyArea(string companyId);

        [OperationContract]
        DataTable GetComboUserCompany(string userId);

        [OperationContract]
        DataTable GetComboSampleNo(string category,string userId);

        [OperationContract]
        string GetCompanyId(string companyId, string deptId);

        [OperationContract]
        bool AddCompany(string f1, string f2, string f3, string f4, string f5, DateTime f6);

        [OperationContract]
        DataTable GetComboDetSample(string category);

        [OperationContract]
        DataTable GetComboExecuteProDetect(string category, object detItem, object detObject);

        [OperationContract]
        DataTable GetComboDetReviewReagent(string category);

        [OperationContract]
        DataTable GetDeptCompany(string companyId);

        [OperationContract]
        DataTable ExecuteProQueryCompany(string f0, string f1, string f2, string f3, string f4, string f5);

        [OperationContract]
        bool AddTCompany(string f0, string f1, string f2, string f3, string f4, DateTime f5, string f6,
                         string f7);

        [OperationContract]
        bool UpdateTCompany(string companyName, string phone, string areaId, string contacter,
                            string companyId);
    }
}
