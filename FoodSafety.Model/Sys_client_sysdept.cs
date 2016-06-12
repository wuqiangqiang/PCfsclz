using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FoodSafety.Model
{
    [DataContract]
   public class Sys_client_sysdept
    { 
        [DataMember]
        public string deptId { get; set; }
        [DataMember]
        public string deptName { get; set; }
         [DataMember]
         public string deptLevel { get; set; }
         [DataMember]
         public string fkDeptId { get; set; }
         [DataMember]
         public string province { get; set; }
         [DataMember]
         public string city { get; set; }
         [DataMember]
         public string country { get; set; }
         [DataMember]
         public string address { get; set; }
         [DataMember]
         public string contacter { get; set; }
         [DataMember]
         public string contacterphone { get; set; }
         [DataMember]
         public string supplierId { get; set; }
          [DataMember]
         public string maintypes { get; set; }
          [DataMember]
          public string town { get; set; }

          [DataMember]
          public string principal { get; set; }
          [DataMember]
          public string principalphone { get; set; }
        [DataMember]
          public string depttype { get; set; }
        
    }
}
