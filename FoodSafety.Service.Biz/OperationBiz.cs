using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodSafety.Model;
using System.Data;
using FoodSafety.Service.Dal;

namespace FoodSafety.Service.Biz
{
    public static class OperationBiz
    {
        public static UserInfo GetMenu(string loginName, string password)
        {
            return OperationDal.GetMenu(loginName, password);
        }

        public static DataTable GetDepartment(string userId)
        {
            return OperationDal.GetDepartment(userId);
        }

        public static DataTable GetProvinceCity() 
        {
            return OperationDal.GetProvinceCity();
        }

        public static DataTable GetComparisonAndAnalysiseData(string userId, string theme, DateTime startTime, DateTime endTime)
        {
            return OperationDal.GetComparisonAndAnalysiseData(userId, theme, startTime, endTime);
        }

        public static DataTable GetCompany(string userId)
        {
            return OperationDal.GetCompany(userId);
        }

        public static string GetNewVersion(string supplierid)
        {
            return OperationDal.GetNewVersion(supplierid);
        }

        public static DataTable GetRolePermission(string userid)
        {
            return OperationDal.GetRolePermission(userid);
        }
        public static DataTable GetSupplier()
        {
            return OperationDal.GetSupplier(); 
        }
        public static DataTable GetUserSupplier(string supplierId)
        {
            return OperationDal.GetUserSupplier(supplierId);
        }
       public static DataTable GetDeptProvinceCity(string deptid)
       {
           return OperationDal.GetDeptProvinceCity(deptid);
       }
       public static string GetProvince(string detptid)
       {
           return OperationDal.GetProvince(detptid);
       }
       public static DataTable GetUserSampleProduce(string userid, string dtStart, string dtEnd, string sampleNo)
       {
           return OperationDal.GetUserSampleProduce(userid, dtStart, dtEnd,sampleNo);
       }
       public static DataTable GetUserSampleFishery(string userid, string dtStart, string dtEnd, string sampleNo)
       {
           return OperationDal.GetUserSampleFishery(userid, dtStart, dtEnd,sampleNo);
       }
       public static DataTable GetUserSampleAnimal(string userid, string dtStart, string dtEnd,string sampleNo)
       {
           return OperationDal.GetUserSampleAnimal(userid, dtStart, dtEnd, sampleNo);
       }

        public static DataTable GetSysDept()
        {
            return OperationDal.GetSysDept();
        }

        public static DataTable GetClientUser(string deptId)
        {
            return OperationDal.GetClientUser(deptId);
        }
        public static DataTable GetRole(string strWhere)
        {
            return OperationDal.GetRole(strWhere);
        }

        public static string GetSubName(string roleId)
        {
            return OperationDal.GetSubName(roleId);
        }

        public static DataTable GetClientUserDept(string deptId)
        {
            return OperationDal.GetClientUserDept(deptId);
        }

        public static string GetFirstPageUrl()
        {
            return OperationDal.GetFirstPageUrl();
        }

        public static int LoadPicture(string deptId, byte[] img)
        {
            return OperationDal.LoadPicture(deptId, img);
        }

        public static DataTable GetClientLog(string strSql)
        {
            return OperationDal.GetClientLog(strSql);
        }
        public static DataTable ExecuteProSjhc(string Id,string kssj,string jssj)
        {
            return OperationDal.ExecuteProSjhc(Id,kssj,jssj);
        }

        public static bool AddSjhc(string strSql)
        {
            return OperationDal.AddSjhc(strSql);
        }

        public static DataTable ExecuteProCxtj(string userId)
        {
            return OperationDal.ExecuteProCxtj(userId);
        }
        public static DataTable ExecuteProCxtjAll(string userId)
        {
            return OperationDal.ExecuteProCxtjAll(userId);
        }

        public static DataTable ExecuteProDetUser(string userId)
        {
            return OperationDal.ExecuteProDetUser(userId);
        }

        public static DataTable ExecuteProSignIn(string f1, string f2, string f3, string f4, string f5, string f6,string f7)
        {
            return OperationDal.ExecuteProSignIn(f1, f2, f3, f4, f5, f6,f7);
        }

        public static DataTable ExecuteProSignDetails(string f1, string f2, string f3, int f4, int f5)
        {
            return OperationDal.ExecuteProSignDetails(f1, f2, f3, f4, f5);
        }

        public static string GetPictureUrl()
        {
            return OperationDal.GetPictureUrl();
        }
        public static string GetUrl(string id)
        {
            return OperationDal.GetUrl(id);
        }

        public static DataTable ExecuteProUserCompany(string userId, string field)
        {
            return OperationDal.ExecuteProUserCompany(userId, field);
        }

        public static DataTable ExecuteProUserDept(string userId)
        {
            return OperationDal.ExecuteProUserDept(userId);
        }

        public static DataTable GetComboExecuteProDetect(string category,object detItem, object detObject)
        {
            return SysInfoQueryDal.GetComboExecuteProDetect(category,detItem, detObject);
        }

        public static DataTable ExecuteProDetectDetailsAnimal(int id)
        {
            return SysInfoQueryDal.ExecuteProDetectDetailsAnimal(id);
        }
        public static DataTable ExecuteProDetectDetailsFishery(int id)
        {
            return SysInfoQueryDal.ExecuteProDetectDetailsFishery(id);
        }

        public static string GetFisheryUrl()
        {
            return OperationDal.GetFisheryUrl();
        }

        public static DataTable GetComboDetItemAnimal()
        {
            return SysInfoQueryDal.GetComboDetItemAnimal();
        }
        public static DataTable GetComboDetObjectAnimal()
        {
            return SysInfoQueryDal.GetComboDetObjectAnimal();
        }
        public static DataTable GetComboDetReagentAnimal()
        {
            return SysInfoQueryDal.GetComboDetReagentAnimal();
        }
        public static DataTable GetComboDetResult()
        {
            return SysInfoQueryDal.GetComboDetResult();
        }
        public static DataTable GetComboDetSource()
        {
            return SysInfoQueryDal.GetComboDetSource();
        }

        public static DataTable ExecuteProQueryDetectAnimal(string f0, string f1, string f2, string f3, string f4,
                                                            string f5, string f6, string f7, string f8, string f9,
                                                            string f10, string f11, string f12, string f13, int f14,
                                                            int f15)
        {
            return SysInfoQueryDal.ExecuteProQueryDetectAnimal(f0, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15);
        }
        public static DataTable GetComboDetItemProduce()
        {
            return SysInfoQueryDal.GetComboDetItemProduce();
        }
        public static DataTable GetComboDetObjectProduce()
        {
            return SysInfoQueryDal.GetComboDetObjectProduce();
        }
        public static DataTable GetComboDetReagentProduce()
        {
            return SysInfoQueryDal.GetComboDetReagentProduce();
        }
        public static DataTable ExecuteProQueryDetectProduce(string f0, string f1, string f2, string f3, string f4,
                                                           string f5, string f6, string f7, string f8, string f9,
                                                           string f10, string f11, string f12, string f13, int f14,
                                                           int f15)
        {
            return SysInfoQueryDal.ExecuteProQueryDetectProduce(f0, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15);
        }
        public static DataTable GetComboDetItemFishery()
        {
            return SysInfoQueryDal.GetComboDetItemFishery();
        }
        public static DataTable GetComboDetObjectFishery()
        {
            return SysInfoQueryDal.GetComboDetObjectFishery();
        }
        public static DataTable GetComboDetReagentFishery()
        {
            return SysInfoQueryDal.GetComboDetReagentFishery();
        }
        public static DataTable ExecuteProQueryDetectFishery(string f0, string f1, string f2, string f3, string f4,
                                                        string f5, string f6, string f7, string f8, string f9,
                                                        string f10, string f11, string f12, string f13, int f14,
                                                        int f15)
        {
            return SysInfoQueryDal.ExecuteProQueryDetectFishery(f0, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15);
        }

        public static DataTable GetMessage(string strWhere)
        {
            return OperationDal.GetMessage(strWhere);
        }

        public static bool AddMessage(string f1, string f2, string f3, DateTime f4)
        {
            return OperationDal.AddMessage(f1, f2, f3, f4);
        }

        public static DataTable ExecuteProDayReport(string function, string id, DateTime date, string item,
                                                    string itemId, string resultId)
        {
            return SysInfoQueryDal.ExecuteProDayReport(function, id, date, item, itemId, resultId);
        }

        public static DataTable ExecuteProDayReportDetails(string category, string sj, string deptId, string itemId,
                                                           string resultId, int rowN, int tRowN)
        {
            return SysInfoQueryDal.ExecuteProDayReportDetails(category, sj, deptId, itemId, resultId, rowN, tRowN);
        }

        public static DataTable ExecuteProDayReportCountry(string function, string sj, string deptId, string itemId,
                                                           string resultId)
        {
            return SysInfoQueryDal.ExecuteProDayReportCountry(function, sj, deptId, itemId, resultId);
        }
        public static DataTable ExecuteProMonthReport(string function, string id, string date, string item,
                                                 string itemId, string resultId)
        {
            return SysInfoQueryDal.ExecuteProMonthReport(function, id, date, item, itemId, resultId);
        }
        public static DataTable ExecuteProMonthReportCountry(string function, string sj, string deptId, string itemId,
                                                           string resultId)
        {
            return SysInfoQueryDal.ExecuteProMonthReportCountry(function, sj, deptId, itemId, resultId);
        }
        public static DataTable ExecuteProMonthReportDetails(string category, string sj, string deptId, string itemId,
                                                          string resultId, int rowN, int tRowN)
        {
            return SysInfoQueryDal.ExecuteProMonthReportDetails(category, sj, deptId, itemId, resultId, rowN, tRowN);
        }

        public static DataTable ExecuteProYearReport(string function, string id, string startDate, string endDate, string item,
                                                  string itemId, string resultId, string objectId)
        {
            return SysInfoQueryDal.ExecuteProYearReport(function,id,startDate,endDate,item,itemId,resultId,objectId);
        }

        public static DataTable ExecuteProYearReportDetails(string category, string startDate, string endDate,
                                                            string DeptId,
                                                            string itemId, string resultId, string objectId, int rowN,
                                                            int tRowN)
        {
            return SysInfoQueryDal.ExecuteProYearReportDetails(category, startDate, endDate, DeptId, itemId, resultId,
                                                               objectId, rowN, tRowN);
        }

        public static DataTable ExecuteProYearReportCountry(string function, string kssj, string jssj, string deptId,
                                                            string itemId, string resultId, string objectId)
        {
            return SysInfoQueryDal.ExecuteProYearReportCountry(function, kssj, jssj, deptId, itemId, resultId, objectId);
        }

        public static DataTable ExecuteProComparisonAnalysis(string function, string userId, DateTime startDate,
                                                             DateTime endDate)
        {
            return SysInfoQueryDal.ExecuteProComparisonAnalysis(function, userId, startDate, endDate);
        }

        public static DataTable ExecuteProTrendAnalysis(string deptType, string userId)
        {
            return SysInfoQueryDal.ExecuteProTrendAnalysis(deptType, userId);
        }

        public static DataTable ExecuteProNdxmqs(string function, string userId, string year)
        {
            return SysInfoQueryDal.ExecuteProNdxmqs(function, userId, year);
        }

        public static DataTable ExecuteProQyfx(string function, string userId, DateTime startDate, DateTime endDate)
        {
            return SysInfoQueryDal.ExecuteProQyfx(function, userId, startDate, endDate);
        }

        public static DataTable ExecuteProTaskReport(string function, string userId, string year, string item,
                                                     string itemId)
        {
            return SysInfoQueryDal.ExecuteProTaskReport(function, userId, year, item, itemId);
        }

        public static DataTable ExecuteProTaskReportDetails(string category, string sj, string deptId, string itemId,
                                                            int rowN, int tRowN)
        {
            return SysInfoQueryDal.ExecuteProTaskReportDetails(category,sj,deptId,itemId,rowN,tRowN);
        }

        public static DataTable ExecuteProTaskReportCountry(string function, string sj, string deptId, string itemId)
        {
            return SysInfoQueryDal.ExecuteProTaskReportCountry(function,sj,deptId,itemId);
        }

        public static DataTable ExecuteProWarningInfo(string function, string userId, int rowN, int tRowN)
        {
            return SysInfoQueryDal.ExecuteProWarningInfo(function,userId,rowN,tRowN);
        }

        public static DataTable ExecuteProWarningDetails(string category, string deptId, string itemId, string objectId,
                                                          int rowN, int tRowN)
        {
            return SysInfoQueryDal.ExecuteProWarningDetails(category,deptId,itemId,objectId,rowN,tRowN);
        }

        public static DataTable ExecuteProWarningInfoCountry(string function, string deptId, string itemId,
                                                             string objectId, int rowN, int tRowN)
        {
            return SysInfoQueryDal.ExecuteProWarningInfoCountry(function,deptId,itemId,objectId,rowN,tRowN);
        }

        public static DataTable ExecuteProReviewInfo(string category, string userId, DateTime startDate,
                                                     DateTime endDate, string deptItem, string item, int rowN, int tRowN)
        {
            return SysInfoQueryDal.ExecuteProReviewInfo(category,userId,startDate,endDate,deptItem,item,rowN,tRowN);
        }

        public static DataTable ExecuteProWarningReport(string function, string userId, DateTime startDate,
                                                        DateTime endDate, string item, string itemId, string reviewId)
        {
            return SysInfoQueryDal.ExecuteProWarningReport(function,userId,startDate,endDate,item,itemId,reviewId);
        }

        public static DataTable ExecuteProWarningReportDetails(string category, string kssj, string jssj, string deptId,
                                                     string itemId, string reviewFlag, int rowN, int tRowN)
        {
            return SysInfoQueryDal.ExecuteProWarningReportDetails(category, kssj, jssj, deptId, itemId, reviewFlag, rowN,
                                                                  tRowN);
        }

        public static string GetReviewFlag(string category, int detectId)
        {
            return SysInfoQueryDal.GetReviewFlag(category, detectId);
        }

        public static DataTable ExecuteProWarningReportCountry(string function, string kssj, string jssj, string deptId,
                                                               string itemId, string reviewFlag, string deptType)
        {
            return SysInfoQueryDal.ExecuteProWarningReportCountry(function, kssj, jssj, deptId, itemId, reviewFlag,
                                                                  deptType);
        }

        public static DataTable ExecuteProReviewDetails(string kssj, string jssj, string deptId, string itemId,
                                                        string reviewId, int rowN, int tRowN)
        {
            return SysInfoQueryDal.ExecuteProReviewDetails(kssj, jssj, deptId, itemId, reviewId, rowN, tRowN);
        }

        public static DataTable ExecuteProReviewLog(string function, string userId, DateTime kssj, DateTime jssj,
                                                    string deptItem, string item, int rowN, int tRowN)
        {
            return SysInfoQueryDal.ExecuteProReviewLog(function, userId, kssj, jssj, deptItem, item, rowN, tRowN);
        }

        public static DataTable ExecuteProQueryDevice(string userId, string deviceId, string deptType)
        {
            return SysInfoQueryDal.ExecuteProQueryDevice(userId, deviceId, deptType);
        }

        public static DataTable GetComboDetReagentProduceMode(string mode)
        {
            return SysInfoQueryDal.GetComboDetReagentProduceMode(mode);
        }

        public static DataTable ExecuteProDetect(string category,int id)
        {
            return SysInfoQueryDal.ExecuteProDetect(category,id);
        }

        public static string GetCompanyArea(string companyId)
        {
            return OperationDal.GetCompanyArea(companyId);
        }

        public static DataTable GetComboUserCompany(string userId)
        {
            return SysInfoQueryDal.GetComboUserCompany(userId);
        }

        public static DataTable GetComboSampleNo(string category, string userId)
        {
            return SysInfoQueryDal.GetComboSampleNo(category,userId);
        }

        public static string GetCompanyId(string companyId, string deptId)
        {
            return OperationDal.GetCompanyId(companyId, deptId);
        }

        public static bool AddCompany(string f1, string f2, string f3, string f4, string f5, DateTime f6)
        {
            return OperationDal.AddCompany(f1, f2, f3, f4, f5, f6);
        }

        public static DataTable GetComboDetSample(string category)
        {
            return SysInfoQueryDal.GetComboDetSample(category);
        }

        public static DataTable GetComboDetReviewReagent(string category)
        {
            return SysInfoQueryDal.GetComboDetReviewReagent(category);
        }

        public static DataTable GetDeptCompany(string companyId)
        {
            return OperationDal.GetDeptCompany(companyId);
        }

        public static DataTable ExecuteProQueryCompany(string f0, string f1, string f2, string f3, string f4, string f5)
        {
            return OperationDal.ExecuteProQueryCompany(f0, f1, f2, f3, f4, f5);
        }

        public static bool AddTCompany(string f0, string f1, string f2, string f3, string f4, DateTime f5, string f6,
                                       string f7)
        {
            return OperationDal.AddTCompany(f0, f1, f2, f3, f4, f5, f6, f7);
        }

        public static bool UpdateTCompany(string companyName, string phone, string areaId, string contacter,
                                          string companyId)
        {
            return OperationDal.UpdateTCompany(companyName, phone, areaId, contacter, companyId);
        }
    }
}
