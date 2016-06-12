using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodSafety.Model;
using System.Data;
namespace FoodSafetyMonitoring.Common
{
   public static class PubClass
   {
       public static string supplierid = string.Empty;
       public static UserInfo userInfo;

       public static LoginUser userinfo = new LoginUser();
       public static DataTable ProvinceCityTable;
 
       public static MainWindow FrmPubMain = null;
   }
}
