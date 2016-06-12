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
using FoodSafety.DI;
using FoodSafety.Model;
using FoodSafety.Service.Contract;
using Toolkit = Microsoft.Windows.Controls;
using DBUtility;
using System.Data;
using FoodSafetyMonitoring.Common;
using System.ComponentModel;
using System.Collections.ObjectModel;
using FoodSafetyMonitoring.dao;
using System.Security.Cryptography;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// UcUserManager.xaml 的交互逻辑
    /// </summary>
    public partial class UcUserManager : UserControl
    {
     
        FamilyTreeViewModel departmentViewModel;
        IOperationContract operationService = ServiceFactory.GetWcfService<IOperationContract>();
        ISysSetContract sysSetContract = ServiceFactory.GetWcfService<ISysSetContract>();
        readonly Dictionary<string, string> cityLevelDictionary = new Dictionary<string, string>() { { "0", "国家" }, { "1", "省级" }, { "2", "市(州)" }, { "3", "区县" }, { "4", "检测站" } };
        private Department department;
        private DataTable ProvinceCityTable = null;
        private string user_flag_tier;
        private DataTable SupplierTable;
        private string password_old;
        //private int flag_init = 0;//初始化,0未初始化,1已初始化

        //当前选中的部门id,名称,部门等级,检测单位的类别
        private string dept_id;
        private string dept_name;
        private string dept_flag;

        public UcUserManager()
        {
            InitializeComponent();
      
            ProvinceCityTable = PubClass.ProvinceCityTable;
            user_flag_tier = PubClass.userInfo.FlagTier.ToString();
            SupplierTable = operationService.GetSupplier();
         
            //用户管理的初始化
            System.Data.DataTable dt = operationService.GetSysDept();
            ComboboxTool.InitComboboxSource(_department, dt, "lr");

            System.Data.DataTable dtRole = operationService.GetRole("");
            ComboboxTool.InitComboboxSource(_cmbRoleType,dtRole, "lr");


            _department.SelectionChanged += new SelectionChangedEventHandler(_department_SelectionChanged);

            InitUserControlUI();
        }

        public void InitUserControlUI()
        {
            //if (flag_init == 1)
            //{
            //    return;
            //}
            //flag_init = 1;

            DataTable table = operationService.GetDepartment(PubClass.userInfo.ID);
            if (table != null)
            {
                department = new Department();
                //DataRow[] rows = table.Select("FK_CODE_DEPT='0'");
                //if (rows.Length == 0)
                //{
                //    return;
                //}
                DataRow[] rows = table.Select();
                department.Name = rows[0]["deptName"].ToString();
                department.Row = rows[0];
                string deptId = "";
                deptId = rows[0]["deptId"].ToString();
                rows = table.Select("fkDeptId='" + deptId + "'");
                foreach (DataRow row1 in rows)
                {
                    Department department1 = new Department();
                    department1.Parent = department;
                    department1.Row = row1;
                    department1.Name = row1["deptName"].ToString();
                    string deptId2 = "";
                    deptId2 = row1["deptId"].ToString();
                    rows = table.Select("fkDeptId='" + deptId2 + "'");
                    foreach (DataRow row2 in rows)
                    {
                        Department department2 = new Department();
                        department2.Parent = department1;
                        department2.Row = row2;
                        department2.Name = row2["deptName"].ToString();
                        rows = table.Select("fkDeptId='" + row2["deptId"].ToString() + "'");
                        foreach (DataRow row3 in rows)
                        {
                            Department department3 = new Department();
                            department3.Parent = department2;
                            department3.Row = row3;
                            department3.Name = row3["deptName"].ToString();
                            rows = table.Select("fkDeptId='" + row3["deptId"].ToString() + "'");
                            foreach (DataRow row4 in rows)
                            {
                                Department department4 = new Department();
                                department4.Parent = department3;
                                department4.Row = row4;
                                department4.Name = row4["deptName"].ToString();
                                department3.Children.Add(department4);
                            }
                            department2.Children.Add(department3);
                        }
                        department1.Children.Add(department2);
                    }
                    department.Children.Add(department1);
                }
                departmentViewModel = new FamilyTreeViewModel(department);
                _treeView.DataContext = departmentViewModel;
            }
        }

        //省市上双击事件
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 0)
            {
                //获取当前部门树形菜单上选中的部门id和名称
                Department department = (sender as TextBlock).Tag as Department;
                DataRow row = department.Row;
                dept_id = row["deptId"].ToString();
                dept_name = row["deptName"].ToString();
                dept_flag = row["deptLevel"].ToString();
                Load_UserManager(dept_id);
                btnCreate.Visibility = Visibility.Visible;
                Clear();
            }
        }

        private void Load_UserManager(string dept_id)
        {
          
            try
            {
                DataTable dt = operationService.GetClientUser(dept_id);
                lvlist.DataContext = null;
                lvlist.DataContext = dt;
                lvlist.Tag = dt;
            }
            catch (Exception)
            {
                return;
            }
        }

        void _department_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_department.SelectedIndex > 0)
            {
                _cmbRoleType.IsEnabled = true;

                DataTable dt = operationService.GetRole(string.Format(" roleLevel='{0}'", dept_flag));
                ComboboxTool.InitComboboxSource(_cmbRoleType, dt, "lr");
                
                _cmbRoleType.SelectionChanged += new SelectionChangedEventHandler(_cmbRoleType_SelectionChanged);
            }
        }

        void _cmbRoleType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_cmbRoleType.SelectedIndex > 0)
            {
                _subDetails.Text = operationService.GetSubName((_cmbRoleType.SelectedItem as Label).Tag.ToString());
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            user_details.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            Clear();
            this._department.Text = dept_name;
            //this._department.IsEnabled = true;
            this._loginName.IsEnabled = true;
            this._loginPassword.IsEnabled = true;
            this.txtUserName.IsEnabled = true;
            this._user_manger.IsEnabled = true;
            this._user_manger_2.IsEnabled = true;
            this._user_manger.IsChecked = true;
            //this._dept_flag.Text = "(必填)";
            this._role_flag.Text = "(必填)";
            this._user_flag.Text = "(必填)";
            this._password_flag.Text = "(必填)";
            this._name_flag.Text = "(必填)";
            this._manager_flag.Text = "(必填)";
        }

        private void Clear()
        {
            this._department.SelectedIndex = 0;
            this._cmbRoleType.SelectedIndex = 0;
            this._subDetails.Text = "";
            this._loginName.Text = "";
            this._loginPassword.Password = "";
            this.txtUserName.Text = "";
            this._user_manger.IsChecked = false;
            this._user_manger_2.IsChecked = false;
            //this.txtMsg.Text = "";
            this._department.IsEnabled = false;
            this._cmbRoleType.IsEnabled = false;
            this._subDetails.IsEnabled = false;
            this._loginName.IsEnabled = false;
            this._loginPassword.IsEnabled = false;
            this.txtUserName.IsEnabled = false;
            this._user_manger.IsEnabled = false;
            this._user_manger_2.IsEnabled = false;
            this.btnSave.Tag = null;
            //this._dept_flag.Text = "";
            this._role_flag.Text = "";
            this._user_flag.Text = "";
            this._password_flag.Text = "";
            this._name_flag.Text = "";
            this._manager_flag.Text = "";
            //this.txtUserName.Focus();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Toolkit.MessageBox.Show("确定要删除该用户吗？", "系统询问", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string id = (sender as Button).Tag.ToString();
                if (DeleteUser(id))
                {
                    Toolkit.MessageBox.Show("删除成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    Common.SysLogEntry.WriteLog("系统用户管理", PubClass.userInfo.ShowName, OperationType.Delete, "删除系统用户");
                    Clear();
                    Load_UserManager(dept_id);
                }
                else
                {
                    Toolkit.MessageBox.Show("删除失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                e.Handled = false;
            }
        }

        private bool DeleteUser(string id)
        {
            bool result = false;
            try
            {
                bool flag = sysSetContract.DeleteClientUser(id);
                if (flag)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            user_details.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            //this._department.IsEnabled = true;
            this._loginName.IsEnabled = true;
            this._loginPassword.IsEnabled = true;
            this.txtUserName.IsEnabled = true;
            this._user_manger.IsEnabled = true;
            this._user_manger_2.IsEnabled = true;
            //this._dept_flag.Text = "(必填)";
            this._role_flag.Text = "(必填)";
            this._user_flag.Text = "(必填)";
            this._password_flag.Text = "(必填)";
            this._name_flag.Text = "(必填)";
            this._manager_flag.Text = "(必填)";
            string id = (sender as Button).Tag.ToString();

            DataRow dr = operationService.GetClientUserDept(id).Rows[0];

            this.txtUserName.Text = dr["userName"].ToString();
            this._loginName.Text = dr["userId"].ToString();

            for (int i = 0; i < _department.Items.Count; i++)
            {
                if ((_department.Items[i] as Label).Tag.ToString() == dr["deptId"].ToString())
                {
                    _department.SelectedItem = _department.Items[i];
                    break;
                }
            }

            for (int i = 0; i < _cmbRoleType.Items.Count; i++)
            {
                if ((_cmbRoleType.Items[i] as Label).Tag.ToString() == dr["roleId"].ToString())
                {
                    _cmbRoleType.SelectedItem = _cmbRoleType.Items[i];
                    break;
                }
            }

            this._loginPassword.Password = dr["userPassword"].ToString();
            password_old = dr["userPassword"].ToString();
            if (dr["expired"].ToString() == "1")
            {
                this._user_manger.IsChecked = true;
            }
            else if (dr["expired"].ToString() == "0")
            {
                this._user_manger_2.IsChecked = true;
            }

            this.btnSave.Tag = id;
        }


        private void FrameworkElement_GotFocus(object sender, RoutedEventArgs e)
        {
            //ClearTip(sender);
        }

        /// <summary>
        /// 清除提示信息
        /// </summary>
        /// <param name="sender">控件</param>
        //private void ClearTip(object sender)
        //{
        //    string name = (sender as FrameworkElement).Name;
        //    if (txtMsg.Tag != null)
        //    {
        //        if (name == txtMsg.Tag.ToString())
        //        {
        //            txtMsg.Text = "";
        //        }
        //    }
        //}

        private void _user_manger_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).Name == "_user_manger")
            {
                _user_manger_2.IsChecked = false;
            }
            else if ((sender as CheckBox).Name == "_user_manger_2")
            {
                _user_manger.IsChecked = false;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string user_manager_flag = "";
            if (this._department.SelectedIndex < 1)
            {
                //txtMsg.Text = "*请选择帐号使用单位！";
                //txtMsg.Tag = "_department";
                Toolkit.MessageBox.Show("请选择帐号使用单位!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (this._cmbRoleType.SelectedIndex < 1)
            {
                //txtMsg.Text = "*请选择帐号权限！";
                //txtMsg.Tag = "_cmbRoleType";
                Toolkit.MessageBox.Show("请选择帐号权限!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (this._loginName.Text.Trim() == "")
            {
                //txtMsg.Text = "*请输入登录帐号！";
                //txtMsg.Tag = "_loginName";
                Toolkit.MessageBox.Show("请输入登录帐号!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (this._loginPassword.Password.Trim() == "")
            {
                //txtMsg.Text = "*请输入密码！";
                //txtMsg.Tag = "txtPwd";
                Toolkit.MessageBox.Show("请输入密码!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (this.txtUserName.Text.Trim() == "")
            {
                //txtMsg.Text = "*请输入帐号使用人姓名！";
                //txtMsg.Tag = "txtUserName";
                Toolkit.MessageBox.Show("请输入帐号使用人姓名!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (this._user_manger.IsChecked == true)
            {
                user_manager_flag = "1";
            }
            else if (this._user_manger_2.IsChecked == true)
            {
                user_manager_flag = "0";
            }

            //根据btnSave按钮的Tag属性判断是添加还是修改（null为添加，反之为修改）
            string strSql = string.Empty;

            if (btnSave.Tag == null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("userId ='{0}'", _loginName.Text));
                bool flag = sysSetContract.ExistsUserId(sb.ToString());
                if (flag)
                {
                    Toolkit.MessageBox.Show("该登录帐号已存在，请重新输入！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                MD5 md5 = new MD5CryptoServiceProvider();
                string password = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(this._loginPassword.Password))).Replace("-", "");

                strSql = string.Format(@"INSERT INTO sys_client_user(userId,userName,userPassword,deptId,cdate,roleId,cuserId,expired) VALUES 
                                      ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                                      _loginName.Text, txtUserName.Text, password, (_department.SelectedItem as Label).Tag.ToString(),
                                      DateTime.Now, (_cmbRoleType.SelectedItem as Label).Tag.ToString(), PubClass.userInfo.ID, user_manager_flag);
            }
            else
            {
                if (this._loginPassword.Password == password_old)
                {
                    strSql = string.Format(@"UPDATE sys_client_user SET userId = '{0}', userName = '{1}', deptId = '{2}',
                                       roleId = '{3}',expired = '{4}' WHERE id = {5}",
                                           _loginName.Text, txtUserName.Text.Trim(), (_department.SelectedItem as Label).Tag.ToString(),
                                          (_cmbRoleType.SelectedItem as Label).Tag.ToString(), user_manager_flag, btnSave.Tag.ToString());
                }
                else
                {
                    MD5 md5 = new MD5CryptoServiceProvider();
                    string password = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(this._loginPassword.Password))).Replace("-", "");
                    strSql = string.Format(@"UPDATE sys_client_user SET userId = '{0}', userName = '{1}', deptId = '{2}',
                                       roleId = '{3}',expired = '{4}',userPassword = '{5}' WHERE id = {6}",
                                           _loginName.Text, txtUserName.Text.Trim(), (_department.SelectedItem as Label).Tag.ToString(),
                                          (_cmbRoleType.SelectedItem as Label).Tag.ToString(), user_manager_flag, password, btnSave.Tag.ToString());
                }
            }

            try
            {
              
                bool flag = sysSetContract.AddUpdateClientUser(strSql);
                if (flag)
                {

                    Toolkit.MessageBox.Show("保存成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (btnSave.Tag == null)
                    {
                        Common.SysLogEntry.WriteLog("系统用户管理", PubClass.userInfo.ShowName, OperationType.Add, "添加系统用户");
                    }
                    else
                    {
                        Common.SysLogEntry.WriteLog("系统用户管理", PubClass.userInfo.ShowName, OperationType.Modify, "修改系统用户");
                    }
                    Clear();
                    Load_UserManager(dept_id);
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

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void loginName_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            //if (e.DataObject.GetDataPresent(typeof(String)))
            //{
            //    String text = (String)e.DataObject.GetData(typeof(String));
            //    if (!isNumberic(text))
            //    { e.CancelCommand(); }
            //}
            //else { e.CancelCommand(); }
        }

        private void loginName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
    }
}
