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
    public interface IDetectContract
    {
        [OperationContract]
        bool DeleteDetectReportAnimal(string detectId);

        [OperationContract]
        bool DeleteDetectReviewAnimal(string detectId);

        [OperationContract]
        bool ExistsDetectReviewAnimal(string detectId);
        [OperationContract]
        bool DeleteDetectReportProduce(string detectId);

        [OperationContract]
        bool DeleteDetectReviewProduce(string detectId);

        [OperationContract]
        bool ExistsDetectReviewProduce(string detectId);
        [OperationContract]
        bool ExistsDetectReviewFishery(string detectId);

        [OperationContract]
        bool DeleteDetectReviewFishery(string detectId);

        [OperationContract]
        bool DeleteDetectReportFishery(string detectId);

        [OperationContract]
        bool AddUpdateDetectReport(string strSql);

        [OperationContract]
        bool ExecuteProAddDetect(string f0, string f1, string f2, string f3, string f4, string f5,
                                 string f6, string f7, string f8, string f9);

        [OperationContract]
        bool ExecuteProAddDetectFishery(string f0, string f1, string f2, string f3, string f4, string f5,
                                        string f6, string f7, string f8, string f9, DateTime f10);

        [OperationContract]
        bool ExecuteProAddDetectAnimal(string f0, string f1, string f2, string f3, string f4, string f5,
                                       string f6, string f7, string f8, string f9, string f10, string f11,
                                       string f12, DateTime f13);

        [OperationContract]
        string ExecuteFunObjectTable(string deptId);

        [OperationContract]
        DataTable GetDetItem(string category, string itemName);

        [OperationContract]
        bool DeleteDetect(string category, string itemId);

        [OperationContract]
        DataTable GetDetItemAll(string category, string itemId);

        [OperationContract]
        bool UpdateDetect(string category, string itemName, string openFlag, string itemId);

        [OperationContract]
        bool AddDetect(string category, string itemName, string openFlag, string cuserId, DateTime cdate);
    }
}
