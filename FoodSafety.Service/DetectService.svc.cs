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
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“DetectService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 DetectService.svc 或 DetectService.svc.cs，然后开始调试。
    public class DetectService : IDetectContract
    {
        public  bool DeleteDetectReportAnimal(string detectId)
        {
            return DetectBiz.DeleteDetectReportAnimal(detectId);
        }

        public bool DeleteDetectReviewAnimal(string detectId)
        {
            return DetectBiz.DeleteDetectReviewAnimal(detectId);
        }

        public bool ExistsDetectReviewAnimal(string detectId)
        {
            return DetectBiz.ExistsDetectReviewAnimal(detectId);
        }
        public bool ExistsDetectReviewProduce(string detectId)
        {
            return DetectBiz.ExistsDetectReviewProduce(detectId);
        }
        public bool DeleteDetectReviewProduce(string detectId)
        {
            return DetectBiz.DeleteDetectReviewProduce(detectId);
        }
        public bool DeleteDetectReportProduce(string detectId)
        {
            return DetectBiz.DeleteDetectReportProduce(detectId);
        }

        public bool ExistsDetectReviewFishery(string detectId)
        {
            return DetectBiz.ExistsDetectReviewFishery(detectId);
        }
        public bool DeleteDetectReviewFishery(string detectId)
        {
            return DetectBiz.DeleteDetectReviewFishery(detectId);
        }
        public bool DeleteDetectReportFishery(string detectId)
        {
            return DetectBiz.DeleteDetectReportFishery(detectId);
        }
        public  bool AddUpdateDetectReport(string strSql)
        {
            return DetectBiz.AddUpdateDetectReport(strSql);
        }
        public  bool ExecuteProAddDetect(string f0, string f1, string f2, string f3, string f4, string f5,
                                              string f6, string f7, string f8, string f9)
        {
            return DetectBiz.ExecuteProAddDetect(f0, f1, f2, f3, f4, f5, f6, f7, f8, f9);
        }
        public  bool ExecuteProAddDetectFishery(string f0, string f1, string f2, string f3, string f4, string f5,
                                                      string f6, string f7, string f8, string f9, DateTime f10)
        {
            return DetectBiz.ExecuteProAddDetectFishery(f0, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10);
        }
        public  bool ExecuteProAddDetectAnimal(string f0, string f1, string f2, string f3, string f4, string f5,
                                                     string f6, string f7, string f8, string f9, string f10, string f11,
                                                     string f12, DateTime f13)
        {
            return DetectBiz.ExecuteProAddDetectAnimal(f0, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13);
        }
        public  string ExecuteFunObjectTable(string deptId)
        {
            return DetectBiz.ExecuteFunObjectTable(deptId);
        }
        public  DataTable GetDetItem(string category, string itemName)
        {
            return DetectBiz.GetDetItem(category, itemName);
        }
        public  bool DeleteDetect(string category, string itemId)
        {
            return DetectBiz.DeleteDetect(category, itemId);
        }
        public  DataTable GetDetItemAll(string category, string itemId)
        {
            return DetectBiz.GetDetItemAll(category, itemId);
        }
        public  bool UpdateDetect(string category, string itemName, string openFlag, string itemId)
        {
            return DetectBiz.UpdateDetect(category, itemName, openFlag, itemId);
        }
        public  bool AddDetect(string category, string itemName, string openFlag, string cuserId, DateTime cdate)
        {
            return DetectBiz.AddDetect(category, itemName, openFlag, cuserId, cdate);
        }
    }
}
