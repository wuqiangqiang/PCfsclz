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
    public static class DetectBiz
    {
        public static bool ExistsDetectReviewAnimal(string detectId)
        {
            return DetectDal.ExistsDetectReviewAnimal(detectId);
        }
        public static bool DeleteDetectReviewAnimal(string detectId)
        {
            return DetectDal.DeleteDetectReviewAnimal(detectId);
        }
        public static bool DeleteDetectReportAnimal(string detectId)
        {
            return DetectDal.DeleteDetectReportAnimal(detectId);
        }

        public static bool ExistsDetectReviewProduce(string detectId)
        {
            return DetectDal.ExistsDetectReviewProduce(detectId);
        }
        public static bool DeleteDetectReviewProduce(string detectId)
        {
            return DetectDal.DeleteDetectReviewProduce(detectId);
        }
        public static bool DeleteDetectReportProduce(string detectId)
        {
            return DetectDal.DeleteDetectReportProduce(detectId);
        }

        public static bool ExistsDetectReviewFishery(string detectId)
        {
            return DetectDal.ExistsDetectReviewFishery(detectId);
        }
        public static bool DeleteDetectReviewFishery(string detectId)
        {
            return DetectDal.DeleteDetectReviewFishery(detectId);
        }
        public static bool DeleteDetectReportFishery(string detectId)
        {
            return DetectDal.DeleteDetectReportFishery(detectId);
        }

        public static bool AddUpdateDetectReport(string strSql)
        {
            return DetectDal.AddUpdateDetectReport(strSql);
        }

        public static bool ExecuteProAddDetect(string f0, string f1, string f2, string f3, string f4, string f5,
                                               string f6, string f7, string f8, string f9)
        {
            return DetectDal.ExecuteProAddDetect(f0, f1, f2, f3, f4, f5, f6, f7, f8, f9);
        }

        public static bool ExecuteProAddDetectFishery(string f0, string f1, string f2, string f3, string f4, string f5,
                                                      string f6, string f7, string f8, string f9, DateTime f10)
        {
            return DetectDal.ExecuteProAddDetectFishery(f0, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10);
        }

        public static bool ExecuteProAddDetectAnimal(string f0, string f1, string f2, string f3, string f4, string f5,
                                                     string f6, string f7, string f8, string f9, string f10, string f11,
                                                     string f12, DateTime f13)
        {
            return DetectDal.ExecuteProAddDetectAnimal(f0, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13);
        }

        public static string ExecuteFunObjectTable(string deptId)
        {
            return DetectDal.ExecuteFunObjectTable(deptId);
        }

        public static DataTable GetDetItem(string category, string itemName)
        {
            return DetectDal.GetDetItem(category, itemName);
        }

        public static bool DeleteDetect(string category, string itemId)
        {
            return DetectDal.DeleteDetect(category, itemId);
        }

        public static DataTable GetDetItemAll(string category, string itemId)
        {
            return DetectDal.GetDetItemAll(category, itemId);
        }

        public static bool UpdateDetect(string category, string itemName, string openFlag, string itemId)
        {
            return DetectDal.UpdateDetect(category, itemName, openFlag, itemId);
        }

        public static bool AddDetect(string category, string itemName, string openFlag, string cuserId, DateTime cdate)
        {
            return DetectDal.AddDetect(category, itemName, openFlag, cuserId, cdate);
        }
    }
}
