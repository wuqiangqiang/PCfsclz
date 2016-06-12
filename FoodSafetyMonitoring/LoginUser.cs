using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using FoodSafety.DI;
using FoodSafety.Model;
using FoodSafety.Service.Contract;
using FoodSafetyMonitoring.Common;

namespace FoodSafetyMonitoring
{
    public class LoginUser
    {
        /// <summary>
        /// 使用委托
        /// </summary>
        public delegate void userLogin();
        public event userLogin userlogined;
        public event userLogin userlogouted;
        private ISysSetContract sysSetContract = ServiceFactory.GetWcfService<ISysSetContract>();
       
        FoodSafety.Model.sys_client_user logUserModel = new FoodSafety.Model.sys_client_user();



        #region LoginUser 属性
        /// <summary>
        /// 用户ID
        /// </summary>
        public string LoginUserid = string.Empty;
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string LoginUserName = string.Empty;
        /// <summary>
        /// 用户PWD
        /// </summary>
        public string LoginUserPwd = string.Empty;
       
        #endregion

        #region 用户登录 td  2016-6-6 优化
        public bool userlogin()
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
            if (LoginUserid == string.Empty || LoginUserPwd == string.Empty)
            {
                return false;
            }
            MD5 md5 = new MD5CryptoServiceProvider();
            string password = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(this.LoginUserPwd))).Replace("-", "");
            logUserModel.userId = LoginUserid;
            logUserModel.userPassword = password;
            string fexpired = string.Empty;
            if (sysSetContract.GetUserModel(logUserModel) == null)
            {
                Microsoft.Windows.Controls.MessageBox.Show("您输入的密码和账户名不匹配，请重新输入！");
                return false;
            }
            else
            {
                fexpired = logUserModel.expired;
                if (fexpired == "0")
                {
                    Microsoft.Windows.Controls.MessageBox.Show(logUserModel.userId + "用户已经被禁用！");
                    return false;
                }
                userlogined();
                return true;
            }
        }
        #endregion

    }
}
