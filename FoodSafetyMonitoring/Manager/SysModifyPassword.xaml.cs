using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DBUtility;
using System.Security.Cryptography;
using FoodSafety.DI;
using FoodSafety.Model;
using FoodSafety.Service.Contract;
using Toolkit = Microsoft.Windows.Controls;
using FoodSafetyMonitoring.Common;
using FoodSafetyMonitoring.dao;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysModifyPassword.xaml 的交互逻辑
    /// </summary>
    public partial class SysModifyPassword : UserControl
    {
        ISysSetContract sysSetContract = ServiceFactory.GetWcfService<ISysSetContract>();
        public SysModifyPassword()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (this._password_old.Password.Trim() == "")
            {
                txtMsg.Text = "*原密码不能为空！";
                return;
            }

            if (this._password.Password.Trim() == "")
            {
                txtMsg.Text = "*修改密码不能为空！";
                return;
            }

            if (this._password_2.Password.Trim() == "")
            {
                txtMsg.Text = "*确认密码不能为空！";
                return;
            }

            if (this._password.Password != this._password_2.Password)
            {
                txtMsg.Text = "*修改密码和确认密码不一致！";
                return;
            }

            MD5 md5 = new MD5CryptoServiceProvider();
            string password_old = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(this._password_old.Password))).Replace("-", "");
            string password = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(this._password.Password))).Replace("-", "");

            if (password_old == password)
            {
                txtMsg.Text = "*新密码和原密码一致，请重新输入新密码！";
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(" id ='{0}' and userPassword = '{1}'",PubClass.userInfo.ID,password_old));
            bool exit_flag = sysSetContract.ExistsUserId(sb.ToString());
         
            if (!exit_flag)
            {
                Toolkit.MessageBox.Show("原密码输入错误，请重新输入！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string strSql = string.Format(@"UPDATE sys_client_user SET userPassword = '{0}'  WHERE id = {1}", password, PubClass.userInfo.ID);
            bool flag = sysSetContract.AddUpdateClientUser(strSql);
            try
            {
                if (flag)
                {

                    Toolkit.MessageBox.Show("保存成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    Common.SysLogEntry.WriteLog("修改密码", PubClass.userInfo.ShowName, OperationType.Modify, "修改密码");
                    txtMsg.Text = "";
                    this._password.Password = "";
                    this._password_2.Password = "";
                    this._password_old.Password = "";
                }
                else
                {
                    Toolkit.MessageBox.Show("保存失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            catch (Exception)
            {
                Toolkit.MessageBox.Show("保存失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }
    }
}
