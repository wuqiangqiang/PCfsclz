using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FoodSafety.Model
{
    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public DateTime LastLoginTime { get; set; }

        [DataMember]
        public string LoginName { get; set; }

        [DataMember]
        public string ID { get; set; }

        [DataMember]
        public string ShowName { get; set; }

        [DataMember]
        public string DepartmentID { get; set; }

        [DataMember]
        public string FlagTier { get; set; }

        [DataMember]
        public string SupplierId { get; set; }

        private List<string> menus = new List<string>();

        [DataMember]
        public List<string> Menus
        {
            get { return menus; }
            set { menus = value; }
        }
    }
}
