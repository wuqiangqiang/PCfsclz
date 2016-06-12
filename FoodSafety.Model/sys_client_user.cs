using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FoodSafety.Model
{
    [DataContract]
    public class sys_client_user
    {
        [DataMember]
        public string userId { get; set; }
         [DataMember]
        public string userName { get; set; }
         [DataMember]
        public string userPassword { get; set; }
         [DataMember]
        public string deptId { get; set; }
         [DataMember]
        public DateTime cdate { get; set; }
         [DataMember]
        public string roleId { get; set; }
         [DataMember]
        public string cuserId { get; set; }
         [DataMember]
        public string expired { get; set; }
      
    }
}
