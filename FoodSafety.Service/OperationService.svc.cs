using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using FoodSafety.Service.Contract;
using FoodSafety.Model;
using System.Data;
using FoodSafety.Service.Biz;

namespace FoodSafety.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“OperationService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 OperationService.svc 或 OperationService.svc.cs，然后开始调试。
    public class OperationService : IOperationContract
    {

        public UserInfo GetMenu(string loginName, string password)
        {
            return OperationBiz.GetMenu(loginName, password);
        }

        public DataTable GetDepartment(string userId)
        {
            return OperationBiz.GetDepartment(userId);
        }

        public DataTable GetProvinceCity()
        {
            return OperationBiz.GetProvinceCity();
        }

        public DataTable GetComparisonAndAnalysiseData(string userId, string theme, DateTime startTime, DateTime endTime)
        {
            return OperationBiz.GetComparisonAndAnalysiseData(userId, theme, startTime, endTime);
        }

        public DataTable GetCompany(string userId)
        {
            return OperationBiz.GetCompany(userId);
        }
        public  string GetFirstPageUrl()
        {
            return OperationBiz.GetFirstPageUrl();
        }

        public string GetNewVersion(string supplierid)
        {
            return OperationBiz.GetNewVersion(supplierid);
        }

        public DataTable GetRolePermission(string userid)
        {
           return OperationBiz.GetRolePermission(userid);
        }

        public DataTable GetUserSupplier(string supplierId)
        {
            return OperationBiz.GetUserSupplier(supplierId); 
        }
        public DataTable GetSupplier()
        {
            return OperationBiz.GetSupplier();
        }

        public DataTable GetDeptProvinceCity(string deptid)
        {
            return OperationBiz.GetDeptProvinceCity(deptid);
        }

        public string GetProvince(string detptid)
        {
            return OperationBiz.GetProvince(detptid);
        }
        public DataTable GetUserSampleProduce(string userid, string dtStart, string dtEnd, string sampleNo)
        {
            return OperationBiz.GetUserSampleProduce(userid, dtStart, dtEnd,sampleNo);
        }
        public DataTable GetUserSampleFishery(string userid, string dtStart, string dtEnd, string sampleNo)
        {
            return OperationBiz.GetUserSampleFishery(userid, dtStart, dtEnd,sampleNo);
        }
        public DataTable GetUserSampleAnimal(string userid, string dtStart, string dtEnd,string sampleNo)
        {
            return OperationBiz.GetUserSampleAnimal(userid, dtStart, dtEnd,sampleNo);
        }

        public DataTable GetSysDept()
        {
            return OperationBiz.GetSysDept();
        }

        public DataTable GetClientUser(string deptId)
        {
            return OperationBiz.GetClientUser(deptId);
        }

        public DataTable GetRole(string strWhere)
        {
            return OperationBiz.GetRole(strWhere);
        }
        public DataTable GetClientRole(string strWhere)
        {
            return Sys_client_roleBiz.GetClientRole(strWhere);
        }
        public string GetSubName(string roleId)
        {
            return OperationBiz.GetSubName(roleId);
        }

        public DataTable GetClientUserDept(string deptId)
        {
            return OperationBiz.GetClientUserDept(deptId);
        }

        public DataTable GetRoleSub()
        {
            return Sys_client_roleBiz.GetRoleSub();
        }

        public DataTable GetUserRoleMenu(string id)
        {
            return Sys_client_roleBiz.GetUserRoleMenu(id);
        }

        public int ExecuteFunRoleSub(string field1, string field2)
        {
            return Sys_client_roleBiz.ExecuteFunRoleSub(field1, field2);
        }

        public DataTable GetClientLog(string strSql)
        {
            return OperationBiz.GetClientLog(strSql);
        }
        public DataTable ExecuteProSjhc(string Id, string kssj, string jssj)
        {
            return OperationBiz.ExecuteProSjhc(Id,kssj,jssj);
        }
        public DataTable ExecuteProCxtj(string userId)
        {
            return OperationBiz.ExecuteProCxtj(userId);
        }
        public DataTable ExecuteProDetUser(string userId)
        {
            return OperationBiz.ExecuteProDetUser(userId);
        }

        public DataTable GetSysClientUser(string strWhere)
        {
            return Sys_client_userBiz.GetSysClientUser(strWhere);
        }
        public  DataTable ExecuteProSignIn(string f1, string f2, string f3, string f4, string f5, string f6,string f7)
        {
            return OperationBiz.ExecuteProSignIn(f1, f2, f3, f4, f5, f6,f7);
        }
        public  DataTable ExecuteProSignDetails(string f1, string f2, string f3, int f4, int f5)
        {
            return OperationBiz.ExecuteProSignDetails(f1, f2, f3, f4, f5);
        }
        public  string GetPictureUrl()
        {
            return OperationBiz.GetPictureUrl();
        }
        public  string GetUrl(string id)
        {
            return OperationBiz.GetUrl(id);
        }
        public string GetFisheryUrl()
        {
            return OperationBiz.GetFisheryUrl();
        }
        public  DataTable ExecuteProUserCompany(string userId, string field)
        {
            return OperationBiz.ExecuteProUserCompany(userId, field);
        }

        public  DataTable ExecuteProUserDept(string userId)
        {
            return OperationBiz.ExecuteProUserDept(userId);
        }

        public DataTable ExecuteProDetect(string category,int id)
        {
            return OperationBiz.ExecuteProDetect(category,id);
        }
        public DataTable ExecuteProDetectDetailsAnimal(int id)
        {
            return OperationBiz.ExecuteProDetectDetailsAnimal(id);
        }

        public DataTable ExecuteProDetectDetailsFishery(int id)
        {
            return OperationBiz.ExecuteProDetectDetailsFishery(id);
        }

        public DataTable GetComboDetItemAnimal()
        {
            return OperationBiz.GetComboDetItemAnimal();
        }
        public DataTable GetComboDetObjectAnimal()
        {
            return OperationBiz.GetComboDetObjectAnimal();
        }
        public DataTable GetComboDetReagentAnimal()
        {
            return OperationBiz.GetComboDetReagentAnimal();
        }
        public DataTable GetComboDetResult()
        {
            return OperationBiz.GetComboDetResult();
        }
        public DataTable GetComboDetSource()
        {
            return OperationBiz.GetComboDetSource();
        }

        public DataTable ExecuteProQueryDetectAnimal(string f0, string f1, string f2, string f3, string f4,
                                                     string f5, string f6, string f7, string f8, string f9,
                                                     string f10, string f11, string f12, string f13, int f14,
                                                     int f15)
        {
            return OperationBiz.ExecuteProQueryDetectAnimal(f0, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15);

        }
        public  DataTable GetComboDetItemProduce()
        {
            return OperationBiz.GetComboDetItemProduce();
        }
        public  DataTable GetComboDetObjectProduce()
        {
            return OperationBiz.GetComboDetObjectProduce();
        }

        public DataTable GetComboDetReagentProduce()
        {
            return OperationBiz.GetComboDetReagentProduce();
        }
        public DataTable ExecuteProQueryDetectProduce(string f0, string f1, string f2, string f3, string f4,
                                                    string f5, string f6, string f7, string f8, string f9,
                                                    string f10, string f11, string f12, string f13, int f14,
                                                    int f15)
        {
            return OperationBiz.ExecuteProQueryDetectProduce(f0, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15);

        }
        public DataTable GetComboDetItemFishery()
        {
            return OperationBiz.GetComboDetItemFishery();
        }
        public DataTable GetComboDetObjectFishery()
        {
            return OperationBiz.GetComboDetObjectFishery();
        }

        public DataTable GetComboDetReagentFishery()
        {
            return OperationBiz.GetComboDetReagentFishery();
        }
        public DataTable ExecuteProQueryDetectFishery(string f0, string f1, string f2, string f3, string f4,
                                                   string f5, string f6, string f7, string f8, string f9,
                                                   string f10, string f11, string f12, string f13, int f14,
                                                   int f15)
        {
            return OperationBiz.ExecuteProQueryDetectFishery(f0, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15);

        }

        public DataTable GetMessage(string strWhere)
        {
            return OperationBiz.GetMessage(strWhere);
        }

        public bool AddMessage(string f1, string f2, string f3, DateTime f4)
        {
            return OperationBiz.AddMessage(f1, f2, f3, f4);
        }

        public DataTable ExecuteProDayReport(string function, string id, DateTime date, string item,
                                             string itemId, string resultId)
        {
            return OperationBiz.ExecuteProDayReport(function, id, date, item, itemId, resultId);
        }

        public DataTable ExecuteProDayReportDetails(string category, string sj, string deptId, string itemId,
                                                    string resultId, int rowN, int tRowN)
        {
            return OperationBiz.ExecuteProDayReportDetails(category, sj, deptId, itemId, resultId, rowN, tRowN);
        }

        public DataTable ExecuteProDayReportCountry(string function, string sj, string deptId, string itemId,
                                                    string resultId)
        {
            return OperationBiz.ExecuteProDayReportCountry(function, sj, deptId, itemId, resultId);
        }
        public DataTable ExecuteProMonthReport(string function, string id, string date, string item,
                                            string itemId, string resultId)
        {
            return OperationBiz.ExecuteProMonthReport(function, id, date, item, itemId, resultId);
        }
        public DataTable ExecuteProMonthReportCountry(string function, string sj, string deptId, string itemId,
                                                    string resultId)
        {
            return OperationBiz.ExecuteProMonthReportCountry(function, sj, deptId, itemId, resultId);
        }
        public DataTable ExecuteProMonthReportDetails(string category, string sj, string deptId, string itemId,
                                                   string resultId, int rowN, int tRowN)
        {
            return OperationBiz.ExecuteProMonthReportDetails(category, sj, deptId, itemId, resultId, rowN, tRowN);
        }
        public DataTable ExecuteProYearReport(string function, string id, string startDate, string endDate, string item,
                                                  string itemId, string resultId, string objectId)
        {
            return OperationBiz.ExecuteProYearReport(function, id, startDate, endDate, item, itemId, resultId, objectId);
        }

        public DataTable ExecuteProYearReportDetails(string category, string startDate, string endDate,
                                                     string DeptId,
                                                     string itemId, string resultId, string objectId, int rowN,
                                                     int tRowN)
        {
            return OperationBiz.ExecuteProYearReportDetails(category, startDate, endDate, DeptId, itemId, resultId,
                                                               objectId, rowN, tRowN);
        }
        public DataTable  ExecuteProYearReportCountry(string function, string kssj, string jssj, string deptId, string itemId,
                                            string resultId, string objectId)
        {
            return OperationBiz.ExecuteProYearReportCountry(function, kssj, jssj, deptId, itemId, resultId, objectId);
        }

        public DataTable ExecuteProComparisonAnalysis(string function, string userId, DateTime startDate,
                                                      DateTime endDate)
        {
            return OperationBiz.ExecuteProComparisonAnalysis(function, userId, startDate, endDate);
        }

        public DataTable ExecuteProTrendAnalysis(string deptType, string userId)
        {
            return OperationBiz.ExecuteProTrendAnalysis(deptType, userId);
        }
        public  DataTable ExecuteProNdxmqs(string function, string userId, string year)
        {
            return OperationBiz.ExecuteProNdxmqs(function, userId, year);
        }
        public  DataTable ExecuteProQyfx(string function, string userId, DateTime startDate, DateTime endDate)
        {
            return OperationBiz.ExecuteProQyfx(function, userId, startDate, endDate);
        }

        public DataTable ExecuteProTaskReport(string function, string userId, string year, string item,
                                              string itemId)
        {
            return OperationBiz.ExecuteProTaskReport(function, userId, year, item, itemId);
        }

        public DataTable ExecuteProTaskReportDetails(string category, string sj, string deptId, string itemId,
                                                     int rowN, int tRowN)
        {
            return OperationBiz.ExecuteProTaskReportDetails(category, sj, deptId, itemId, rowN, tRowN);
        }

        public DataTable ExecuteProTaskReportCountry(string function, string sj, string deptId, string itemId)
        {
            return OperationBiz.ExecuteProTaskReportCountry(function, sj, deptId, itemId);
        }

        public DataTable ExecuteProWarningInfo(string function, string userId, int rowN, int tRowN)
        {
            return OperationBiz.ExecuteProWarningInfo(function, userId, rowN, tRowN);
        }

        public DataTable ExecuteProWarningDetails(string category, string deptId, string itemId, string objectId,
                                                   int rowN, int tRowN)
        {
            return OperationBiz.ExecuteProWarningDetails(category, deptId, itemId, objectId, rowN, tRowN);
        }

        public DataTable ExecuteProWarningInfoCountry(string function, string deptId, string itemId,
                                                      string objectId, int rowN, int tRowN)
        {
            return OperationBiz.ExecuteProWarningInfoCountry(function, deptId, itemId, objectId, rowN, tRowN);
        }
        public  DataTable ExecuteProReviewInfo(string category, string userId, DateTime startDate,
                                                 DateTime endDate, string deptItem, string item, int rowN, int tRowN)
        {
            return OperationBiz.ExecuteProReviewInfo(category, userId, startDate, endDate, deptItem, item, rowN, tRowN);
        }
        public  DataTable ExecuteProCxtjAll(string userId)
        {
            return OperationBiz.ExecuteProCxtjAll(userId);
        }
        public  DataTable ExecuteProWarningReport(string function, string userId, DateTime startDate,
                                                       DateTime endDate, string item, string itemId, string reviewId)
        {
            return OperationBiz.ExecuteProWarningReport(function, userId, startDate, endDate, item, itemId, reviewId);
        }
        public  DataTable ExecuteProWarningReportDetails(string category, string kssj, string jssj, string deptId,
                                                    string itemId, string reviewFlag, int rowN, int tRowN)
        {
            return OperationBiz.ExecuteProWarningReportDetails(category, kssj, jssj, deptId, itemId, reviewFlag, rowN,
                                                                  tRowN);
        }
        public  string GetReviewFlag(string category, int detectId)
        {
            return OperationBiz.GetReviewFlag(category, detectId);
        }
        public  DataTable ExecuteProWarningReportCountry(string function, string kssj, string jssj, string deptId,
                                                             string itemId, string reviewFlag, string deptType)
        {
            return OperationBiz.ExecuteProWarningReportCountry(function, kssj, jssj, deptId, itemId, reviewFlag,
                                                                  deptType);
        }
        public  DataTable ExecuteProReviewDetails(string kssj, string jssj, string deptId, string itemId,
                                                       string reviewId, int rowN, int tRowN)
        {
            return OperationBiz.ExecuteProReviewDetails(kssj, jssj, deptId, itemId, reviewId, rowN, tRowN);
        }
        public  DataTable ExecuteProReviewLog(string function, string userId, DateTime kssj, DateTime jssj,
                                                   string deptItem, string item, int rowN, int tRowN)
        {
            return OperationBiz.ExecuteProReviewLog(function, userId, kssj, jssj, deptItem, item, rowN, tRowN);
        }
        public  DataTable ExecuteProQueryDevice(string userId, string deviceId, string deptType)
        {
            return OperationBiz.ExecuteProQueryDevice(userId, deviceId, deptType);
        }
        public  DataTable GetComboDetReagentProduceMode(string mode)
        {
            return OperationBiz.GetComboDetReagentProduceMode(mode);
        }
        public  string GetCompanyArea(string companyId)
        {
            return OperationBiz.GetCompanyArea(companyId);
        }
        public  DataTable GetComboUserCompany(string userId)
        {
            return OperationBiz.GetComboUserCompany(userId);
        }
        public DataTable GetComboSampleNo(string category, string userId)
        {
            return OperationBiz.GetComboSampleNo(category,userId);
        }
        public  string GetCompanyId(string companyId, string deptId)
        {
            return OperationBiz.GetCompanyId(companyId, deptId);
        }
        public  bool AddCompany(string f1, string f2, string f3, string f4, string f5, DateTime f6)
        {
            return OperationBiz.AddCompany(f1, f2, f3, f4, f5, f6);
        }
        public  DataTable GetComboDetSample(string category)
        {
            return OperationBiz.GetComboDetSample(category);
        }
        public DataTable GetComboExecuteProDetect(string category, object detItem, object detObject)
        {
            return OperationBiz.GetComboExecuteProDetect(category, detItem, detObject);
        }
        public  DataTable GetComboDetReviewReagent(string category)
        {
            return OperationBiz.GetComboDetReviewReagent(category);
        }
        public  DataTable GetDeptCompany(string companyId)
        {
            return OperationBiz.GetDeptCompany(companyId);
        }
        public  DataTable ExecuteProQueryCompany(string f0, string f1, string f2, string f3, string f4, string f5)
        {
            return OperationBiz.ExecuteProQueryCompany(f0, f1, f2, f3, f4, f5);
        }
        public  bool AddTCompany(string f0, string f1, string f2, string f3, string f4, DateTime f5, string f6,
                                      string f7)
        {
            return OperationBiz.AddTCompany(f0, f1, f2, f3, f4, f5, f6, f7);
        }
        public  bool UpdateTCompany(string companyName, string phone, string areaId, string contacter,
                                         string companyId)
        {
            return OperationBiz.UpdateTCompany(companyName, phone, areaId, contacter, companyId);
        }
    }
}
