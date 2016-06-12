using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Data;
using FoodSafety.DI;
using FoodSafety.Model;
using FoodSafety.Service.Contract;
using Toolkit = Microsoft.Windows.Controls;
using DBUtility;
using FoodSafetyMonitoring.Common;
using System.ComponentModel;
using System.Collections.ObjectModel;
using FoodSafetyMonitoring.dao;
using System.Data.Odbc;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;


namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// Test.xaml 的交互逻辑
    /// </summary>
    public partial class SysDeptManager : System.Windows.Controls.UserControl, IClickChildMenuInitUserControlUI
    {
        FamilyTreeViewModel departmentViewModel;
      
        IOperationContract operationService = ServiceFactory.GetWcfService<IOperationContract>();
        ISysSetContract sysSetService = ServiceFactory.GetWcfService<ISysSetContract>();

        readonly Dictionary<string, string> cityLevelDictionary = new Dictionary<string, string>() { { "0", "国家" }, { "1", "省级" }, { "2", "市(州)" }, { "3", "区县" }, { "4", "检测单位" } };
        private Department department;
        private System.Data.DataTable ProvinceCityTable = null;
        private string user_flag_tier;
        private System.Data.DataTable SupplierTable;
        private System.Data.DataTable dt_level = new System.Data.DataTable();
        private System.Data.DataTable dt_dept_type = new System.Data.DataTable();
        private string state = "view";
        private TextBlock text_treeView = null;

        public SysDeptManager()
        {
            InitializeComponent();
         
            ProvinceCityTable = PubClass.ProvinceCityTable;
            user_flag_tier =PubClass.userInfo.FlagTier.ToString();
            SupplierTable = operationService.GetSupplier();
            //赋值所在地下拉选择框
            ComboboxTool.InitComboboxSource(_lower_provice, GetRows("0001"), "lr");
            //DataRow[] rows = ProvinceCityTable.Select("cityId='44'");
            //ComboboxTool.InitComboboxSource(_lower_provice, rows, "lr");
            _lower_provice.SelectionChanged += new SelectionChangedEventHandler(_province_SelectionChanged);
            //赋值供应商下拉选择框
            //ComboboxTool.InitComboboxSource(_Supplier, "select supplierId,supplierName from t_supplier", "lr");
            System.Data.DataTable dt = operationService.GetSupplier();
            ComboboxTool.InitComboboxSource(_Supplier,dt, "lr");
            //赋值部门级别下拉选择框
            dt_level.Columns.Add(new DataColumn("levelid"));
            dt_level.Columns.Add(new DataColumn("levelname"));
            var row2 = dt_level.NewRow();
            row2["levelid"] = "0";
            row2["levelname"] = "省级";
            dt_level.Rows.Add(row2);
            var row3 = dt_level.NewRow();
            row3["levelid"] = "1";
            row3["levelname"] = "市级";
            dt_level.Rows.Add(row3);
            var row4 = dt_level.NewRow();
            row4["levelid"] = "2";
            row4["levelname"] = "区级";
            dt_level.Rows.Add(row4);
            var row5 = dt_level.NewRow();
            row5["levelid"] = "3";
            row5["levelname"] = "镇街级";
            dt_level.Rows.Add(row5);
            var row6 = dt_level.NewRow();
            row6["levelid"] = "4";
            row6["levelname"] = "检测单位";
            dt_level.Rows.Add(row6);
            ComboboxTool.InitComboboxSource(_level, dt_level, "lr");
            //赋值主导产业下拉选择框
            dt_dept_type.Columns.Add(new DataColumn("typeid"));
            dt_dept_type.Columns.Add(new DataColumn("typename"));
            var row_type_1 = dt_dept_type.NewRow();
            row_type_1["typeid"] = "1";
            row_type_1["typename"] = "种植业";
            dt_dept_type.Rows.Add(row_type_1);
            var row_type_2 = dt_dept_type.NewRow();
            row_type_2["typeid"] = "2";
            row_type_2["typename"] = "畜禽业";
            dt_dept_type.Rows.Add(row_type_2);
            var row_type_3 = dt_dept_type.NewRow();
            row_type_3["typeid"] = "3";
            row_type_3["typename"] = "水产业";
            dt_dept_type.Rows.Add(row_type_3);
            ComboboxTool.InitComboboxSource(_dept_type, dt_dept_type, "lr");

            InitUserControlUI();
        }

        #region IClickChildMenuInitUserControlUI 成员

        private int flag_init = 0;//初始化,0未初始化,1已初始化

        public void InitUserControlUI()
        {
            if (flag_init == 1)
            {
                return;
            }
            flag_init = 1;

          System.Data.DataTable table = operationService.GetDepartment(PubClass.userInfo.ID);
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
                    rows = table.Select("fkDeptId='" + row1["deptId"].ToString() + "'");
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

        #endregion


        void _province_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_lower_provice.SelectedIndex > 0)
            {
                ComboboxTool.InitComboboxSource(_lower_city, GetRows((_lower_provice.SelectedItem as System.Windows.Controls.Label).Tag.ToString()), "lr");
                _lower_city.SelectionChanged += new SelectionChangedEventHandler(_city_SelectionChanged);
            }
        }


        void _city_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_lower_city.SelectedIndex > 0)
            {
                ComboboxTool.InitComboboxSource(_lower_country, GetRows((_lower_city.SelectedItem as System.Windows.Controls.Label).Tag.ToString()), "lr");
                _lower_country.SelectionChanged += new SelectionChangedEventHandler(_country_SelectionChanged);
            }
        }

        void _country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_lower_country.SelectedIndex > 0)
            {
                ComboboxTool.InitComboboxSource(_lower_town, GetRows((_lower_country.SelectedItem as System.Windows.Controls.Label).Tag.ToString()), "lr");
            }
        }

        void _level_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_level.SelectedIndex > 0)
            {
                if ((_level.SelectedItem as System.Windows.Controls.Label).Tag.ToString() == "4")
                {
                    _station_name.Text = "检测单位名称:";
                    _detail_info.RowDefinitions[5].Height = new GridLength(0, GridUnitType.Pixel);
                }
                else
                {
                    _station_name.Text = "部门名称:";
                }
            }
        }


        //保存按钮
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Department department = new Department();

            //新增
            if (state == "add" || state == "addsamelevel")
            {
                if (state == "add")
                {
                    department = _add.Tag as Department;
                }
                else if (state == "addsamelevel")
                {
                    department = _addsamelevel.Tag as Department;
                }

                System.Data.DataTable dt = new System.Data.DataTable();
                DataRow row = department.Row.Table.NewRow();
                row.ItemArray = (object[])department.Row.ItemArray.Clone();
                row["fkDeptId"] = row["deptId"];

                int maxID = 0;

                if (department.Children.Count == 0)
                {
                    maxID = Convert.ToInt32(row["deptId"].ToString() + "01");
                    row["deptId"] = maxID;
                }
                else
                {
                    for (int i = 0; i < department.Children.Count; i++)
                    {
                        int v = Convert.ToInt32(department.Children[i].Row["deptId"].ToString());
                        if (maxID < v)
                        {
                            maxID = v;
                        }
                    }
                    row["deptId"] = maxID + 1;
                }

                if (_level.SelectedIndex < 1)
                {
                    Toolkit.MessageBox.Show("请选择部门级别!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (_station.Text == "")
                {
                    if (row["deptLevel"].ToString() == "4")
                    {
                        Toolkit.MessageBox.Show("请输入检测单位名称!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    else
                    {
                        Toolkit.MessageBox.Show("请输入部门名称!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }

                row["deptName"] = _station.Text;
                row["deptLevel"] = (_level.SelectedItem as System.Windows.Controls.Label).Tag.ToString();

                if (user_flag_tier == "0")
                {
                    if (_Supplier.SelectedIndex < 1)
                    {
                        Toolkit.MessageBox.Show("请选择供应商!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }

                row["province"] = _lower_provice.SelectedIndex < 1 ? "" : (_lower_provice.SelectedItem as System.Windows.Controls.Label).Tag.ToString();
                row["city"] = _lower_city.SelectedIndex < 1 ? "" : (_lower_city.SelectedItem as System.Windows.Controls.Label).Tag.ToString();
                if(_lower_country.Items.Count <= 0)
                {
                    row["country"] = "";
                }
                else
                {
                    row["country"] = _lower_country.SelectedIndex < 1 ? "" : (_lower_country.SelectedItem as System.Windows.Controls.Label).Tag.ToString();
                }
                if (_lower_town.Items.Count <= 0)
                {
                    row["town"] = "";
                }
                else
                {
                    row["town"] = _lower_town.SelectedIndex < 1 ? "" : (_lower_town.SelectedItem as System.Windows.Controls.Label).Tag.ToString();
                }

                row["deptName"] = _station.Text;
                row["address"] = _address.Text;
                row["principal"] = _principal_name.Text;
                row["principalphone"] = _principal_phone.Text;
                row["contacter"] = _contact_name.Text;
                row["contacterphone"] = _contact_phone.Text;
                row["supplierId"] = (_Supplier.SelectedItem as System.Windows.Controls.Label).Tag.ToString();
                row["maintypes"] = _maintype.Text;
                row["depttype"] = _dept_type.SelectedIndex < 1 ? "" : (_dept_type.SelectedItem as System.Windows.Controls.Label).Tag.ToString();

                Department newDepartment = new Department();
                newDepartment.Parent = department;
                newDepartment.Name = _station.Text;
                newDepartment.Row = row;

                FoodSafety.Model.Sys_client_sysdept sysClientSysdept = new Sys_client_sysdept();
                sysClientSysdept.deptId = row["deptId"].ToString();
                sysClientSysdept.deptName = row["deptName"].ToString();
                sysClientSysdept.deptLevel = row["deptLevel"].ToString();
                sysClientSysdept.fkDeptId = row["fkDeptId"].ToString();
                sysClientSysdept.depttype = row["depttype"].ToString();
                sysClientSysdept.province = row["province"].ToString();
                sysClientSysdept.city = row["city"].ToString();
                sysClientSysdept.country = row["country"].ToString();
                sysClientSysdept.town = row["town"].ToString();
                sysClientSysdept.address = row["address"].ToString();

                sysClientSysdept.principal = row["principal"].ToString();
                sysClientSysdept.principalphone = row["principalphone"].ToString();
                sysClientSysdept.contacter = row["contacter"].ToString();
                sysClientSysdept.contacterphone = row["contacterphone"].ToString();
                sysClientSysdept.supplierId = row["supplierId"].ToString();
                sysClientSysdept.maintypes = row["maintypes"].ToString();
          
                try
                {
                    bool flag = sysSetService.Add(sysClientSysdept);
                    if (flag)
                    {
                        Toolkit.MessageBox.Show("保存成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        Common.SysLogEntry.WriteLog("部门管理",PubClass.userInfo.ShowName, Common.OperationType.Add, "新增部门信息");
                    }
                    else
                    {
                        Toolkit.MessageBox.Show("保存失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                catch (Exception ee)
                {
                    System.Diagnostics.Debug.WriteLine("SysDeptManager.btnSave_Click" + ee.Message);
                    Toolkit.MessageBox.Show("数据更新失败!稍后尝试!");
                    return;
                }
                state = "view";

                department.Children.Add(newDepartment);
                departmentViewModel = new FamilyTreeViewModel(this.department);

                departmentViewModel.SearchText = _station.Text;
                departmentViewModel.SearchCommand.Execute(null);
                _addsamelevel.Tag = newDepartment.Parent;
                _add.Tag = newDepartment;
                _edit.Tag = newDepartment;
                _delete.Tag = newDepartment;

                //设置按钮是否有效
                _addsamelevel.IsEnabled = true;
                _add.IsEnabled = true;
                _edit.IsEnabled = true;
                _delete.IsEnabled = true;

                //如果用户所处部门与选中部门为同一等级，添加同级按钮无效
                if (user_flag_tier == row["deptLevel"].ToString())
                {
                    _addsamelevel.IsEnabled = false;
                }

                //如果选中部门等级是检测单位，添加下级按钮无效;主要品种显示
                if (row["deptLevel"].ToString() == "4")
                {
                    _add.IsEnabled = false;
                }

            }//修改
            else if (state == "edit")
            {
                department = _edit.Tag as Department;

                string Province = _lower_provice.SelectedIndex < 1 ? "" : (_lower_provice.SelectedItem as System.Windows.Controls.Label).Tag.ToString();
                string City = _lower_city.SelectedIndex < 1 ? "" : (_lower_city.SelectedItem as System.Windows.Controls.Label).Tag.ToString();
                string Country = "";
                if (_lower_country.Items.Count <= 0)
                {
                    Country = "";
                }
                else
                {
                    Country = _lower_country.SelectedIndex < 1 ? "" : (_lower_country.SelectedItem as System.Windows.Controls.Label).Tag.ToString();
                }
                string Town = "";
                if (_lower_town.Items.Count <= 0)
                {
                    Town = "";
                }
                else
                {
                    Town = _lower_town.SelectedIndex < 1 ? "" : (_lower_town.SelectedItem as System.Windows.Controls.Label).Tag.ToString();
                }

              
                FoodSafety.Model.Sys_client_sysdept sysClientSysdept = new Sys_client_sysdept();
                sysClientSysdept.deptName = _station.Text;
                sysClientSysdept.address = _address.Text;
                sysClientSysdept.depttype = _dept_type.SelectedIndex < 1 ? "": (_dept_type.SelectedItem as System.Windows.Controls.Label).Tag.ToString();
                sysClientSysdept.contacter = _contact_name.Text;
                sysClientSysdept.contacterphone = _contact_phone.Text;
                sysClientSysdept.supplierId = (_Supplier.SelectedItem as System.Windows.Controls.Label).Tag.ToString();
                sysClientSysdept.province = Province;
                sysClientSysdept.city = City;
                sysClientSysdept.country = Country;
                sysClientSysdept.town = Town;
                sysClientSysdept.maintypes = _maintype.Text;
                sysClientSysdept.principal = _principal_name.Text;
                sysClientSysdept.principalphone = _principal_phone.Text;
                sysClientSysdept.deptId = department.Row["deptId"].ToString();
                try
                {
                    bool flag = sysSetService.Update(sysClientSysdept);
                    if (flag)
                    {

                        Toolkit.MessageBox.Show("保存成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        Common.SysLogEntry.WriteLog("部门管理", PubClass.userInfo.ShowName, Common.OperationType.Modify, "修改部门信息");
                    }
                    else
                    {
                        Toolkit.MessageBox.Show("保存失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                catch (Exception ee)
                {
                    System.Diagnostics.Debug.WriteLine("SysDeptManager.btnSave_Click" + ee.Message);
                      Toolkit.MessageBox.Show("数据更新失败!稍后尝试!");
                    return;
                }
                state = "view";

                //更新树形表
                department.Row["deptName"] = _station.Text;
                department.Name = _station.Text;
                department.Row["address"] = _address.Text;
                department.Row["contacter"] = _principal_name.Text;
                department.Row["contacterphone"] = _contact_phone.Text;
                department.Row["principal"] = _principal_name.Text;
                department.Row["principalphone"] = _principal_phone.Text;
                department.Row["supplierId"] = (_Supplier.SelectedItem as System.Windows.Controls.Label).Tag.ToString();
                department.Row["depttype"] = _dept_type.SelectedIndex < 1 ? "" : (_dept_type.SelectedItem as System.Windows.Controls.Label).Tag.ToString();
                department.Row["province"] = Province;
                department.Row["city"] = City;
                department.Row["country"] = Country;
                department.Row["town"] = Town;
                department.Row["maintypes"] = _maintype.Text;
                _edit.IsEnabled = true;
            }
            else
            {
                return;
            }

            _treeView.DataContext = null;
            _treeView.DataContext = departmentViewModel;
            _detail_info.IsEnabled = false;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            //刷新部门详细信息
            Department department = _add.Tag as Department;
            DataRow row = department.Row;
            load_DeptDetails(row);
        }

        //根据代码获取当前城市名
        private string GetName(string code)
        {
            DataRow[] rows = ProvinceCityTable.Select("cityId='" + code + "'");
            if (rows.Length > 0)
            {
                return rows[0]["cityName"].ToString();
            }
            else
            {
                return "";
            }
        }

        //根据代码获取下级所有城市
        private DataRow[] GetRows(string code)
        {
            DataRow[] rows = ProvinceCityTable.Select("pid='" + code + "'");
            return rows;
        }

        

        //省市上双击事件
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 0)
            {
                text_treeView = sender as TextBlock;
                Department department = (sender as TextBlock).Tag as Department;

                _add.Tag = department;
                _addsamelevel.Tag = department.Parent;
                _edit.Tag = department;
                _delete.Tag = department; 

                DataRow row = department.Row;
                load_DeptDetails(row);
            }
        }

        private void load_DeptDetails(DataRow row)
        {
            //详情部分显示
            _detail_info_all.Visibility = Visibility.Visible;
            _detail_info.IsEnabled = false;
            _detail_info.RowDefinitions[5].Height = new GridLength(0, GridUnitType.Pixel);

            //将必填标识清空
            _level_flag.Text = "";
            _station_flag.Text = "";

            //设置按钮是否有效
            _addsamelevel.IsEnabled = true;
            _add.IsEnabled = true;
            _edit.IsEnabled = true;
            _delete.IsEnabled = true;

            //如果用户所处部门与选中部门为同一等级，添加同级按钮无效,删除按钮也无效
            if (user_flag_tier == row["deptLevel"].ToString())
            {
                _addsamelevel.IsEnabled = false;
                _delete.IsEnabled = false;
            }

            //如果选中部门已存在下级部门，删除按钮无效
            bool result3 = sysSetService.ExistsDept(row["deptId"].ToString());
            if (result3)
            {
                _delete.IsEnabled = false;
            }

            //如果选中部门等级是检测单位，添加下级按钮无效;主要品种显示
            if (row["deptLevel"].ToString() == "4")
            {
                _add.IsEnabled = false;
                _station_name.Text = "检测单位名称:";
                _detail_info.RowDefinitions[5].Height = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                _station_name.Text = "部门名称:";
            }

            //所在地设置
            _belong_to.Width = 200;
            _belong_to.Visibility = Visibility.Visible;
            _area.Visibility = Visibility.Hidden;

            //赋值部门级别
            DataRow[] level_rows = dt_level.Select("levelid = '" + row["deptLevel"].ToString() + "'");
            _regional_level.Text = level_rows[0]["levelname"].ToString();
            _regional_level.Width = 100;
            _regional_level.Visibility = Visibility.Visible;
            _level.Visibility = Visibility.Hidden;

            //供应商显示与否的判断：全权用户登陆显示，其余均不显示
            if (user_flag_tier == "0" && row["deptLevel"].ToString() != "0")
            {
                //_Supplier_name.Visibility = Visibility.Visible;
                //_Supplier.Visibility = Visibility.Visible;
                _detail_info.RowDefinitions[11].Height = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                //_Supplier_name.Visibility = Visibility.Hidden;
                //_Supplier.Visibility = Visibility.Hidden;
                _detail_info.RowDefinitions[11].Height = new GridLength(0, GridUnitType.Pixel);
            }

            //赋值供应商
            DataRow[] rows = SupplierTable.Select("supplierId = '" + row["supplierId"] + "'");
            if (rows.Length > 0)
            {
                _Supplier.Text = rows[0]["supplierName"].ToString();
            }

            //赋值所在地文本框
            _belong_to.Text = GetName(row["province"].ToString()) + GetName(row["city"].ToString()) + GetName(row["country"].ToString()) + GetName(row["town"].ToString());

            _principal_name.Text = row["principal"].ToString();
            _principal_phone.Text = row["principalphone"].ToString();
            _contact_name.Text = row["contacter"].ToString();
            _contact_phone.Text = row["contacterphone"].ToString();
            _address.Text = row["address"].ToString();
            _station.Text = row["deptName"].ToString();
            _maintype.Text = row["maintypes"].ToString();

            for (int i = 0; i < _dept_type.Items.Count; i++)
            {
                if (_dept_type.Items[i].ToString() == row["depttype"].ToString())
                {
                    _dept_type.SelectedItem = _dept_type.Items[i];
                    break;
                }
            }

            if (department.Parent != null)
            {
                _superior_department.Text = department.Parent.Row["deptName"].ToString();
            }
            else
            {
                if (row["deptLevel"].ToString() == "0")
                {
                    _superior_department.Text = "无";
                }
                else
                {
                    //_superior_department.Text = dbOperation.GetDbHelper().GetSingle(string.Format("select deptName FROM sys_client_sysdept WHERE deptId = '{0}'", row["fkDeptId"].ToString())).ToString();
                    _superior_department.Text = sysSetService.GetDeptName(row["fkDeptId"].ToString());
                }
            }

        }

        private void _edit_Click(object sender, RoutedEventArgs e)
        {
            _detail_info.IsEnabled = true;
            state = "edit";
            _edit.IsEnabled = false;
            _station_flag.Text = "(必填)";
            //_station_property_flag.Text = "(必填)";

            Department department = _edit.Tag as Department;
            //bool result = dbOperation.GetDbHelper().Exists(string.Format("select count(deptId) from sys_client_sysdept where fkDeptId ='{0}'", department.Row["deptId"].ToString()));
            if (user_flag_tier == "0")
            {
                bool result = sysSetService.ExistsDept(department.Row["deptId"].ToString());

                if (result)
                {
                    _Supplier.IsEnabled = false;
                }
                else
                {
                    _Supplier.IsEnabled = true;
                }
            }

            //所在地
            _belong_to.Width = 0;
            _belong_to.Visibility = Visibility.Hidden;
            _area.Visibility = Visibility.Visible;
            _lower_provice.SelectedIndex = 0;
            _lower_city.Items.Clear();
            _lower_country.Items.Clear();
            _lower_town.Items.Clear();
            string provice = GetName(department.Row["province"].ToString());
            if (provice == "")
            {
                _lower_provice.SelectedIndex = 0;
            }
            else
            {
                _lower_provice.Text = provice;
            }
            string city = GetName(department.Row["city"].ToString());
            if (city == "")
            {
                _lower_city.SelectedIndex = 0;
            }
            else
            {
                _lower_city.Text = city;
            }
            string country = GetName(department.Row["country"].ToString());
            if (country == "")
            {
                _lower_country.SelectedIndex = 0;
            }
            else
            {
                _lower_country.Text = country;
            }
            string town = GetName(department.Row["town"].ToString());
            if (town == "")
            {
                _lower_town.SelectedIndex = 0;
            }
            else
            {
                _lower_town.Text = town;
            }
        }

        private void _addsamelevel_Click(object sender, RoutedEventArgs e)
        {
            if (_station.Text.Length == 0)
            {
                Toolkit.MessageBox.Show("检测点名称不能为空!");
                return;
            }
            state = "addsamelevel";

            //设置按钮有效性
            _addsamelevel.IsEnabled = false;
            _add.IsEnabled = false;
            _edit.IsEnabled = false;
            _delete.IsEnabled = false;

            _detail_info.IsEnabled = true;
            _regional_level.Width = 0;
            _regional_level.Visibility = Visibility.Hidden;
            _level.Visibility = Visibility.Visible;
            _belong_to.Width = 0;
            _belong_to.Visibility = Visibility.Hidden;
            _area.Visibility = Visibility.Visible;
            _lower_provice.SelectedIndex = 0;
            _lower_city.Items.Clear();
            _lower_country.Items.Clear();
            _lower_town.Items.Clear();

            if (user_flag_tier == "0")
            {
                //_Supplier_name.Visibility = Visibility.Visible;
                //_Supplier.Visibility = Visibility.Visible;
                _detail_info.RowDefinitions[11].Height = new GridLength(1, GridUnitType.Star);
                _Supplier.IsEnabled = true;
            }

            Department department = _addsamelevel.Tag as Department;

            //下级的所在地默认为上级的，但可以修改
            string provice = GetName(department.Row["province"].ToString());
            if (provice == "")
            {
                _lower_provice.SelectedIndex = 0;
            }
            else
            {
                _lower_provice.Text = provice;
            }
            string city = GetName(department.Row["city"].ToString());
            if (city == "")
            {
                _lower_city.SelectedIndex = 0;
            }
            else
            {
                _lower_city.Text = city;
            }
            string country = GetName(department.Row["country"].ToString());
            if (country == "")
            {
                _lower_country.SelectedIndex = 0;
            }
            else
            {
                _lower_country.Text = country;
            }
            string town = GetName(department.Row["town"].ToString());
            if (town == "")
            {
                _lower_town.SelectedIndex = 0;
            }
            else
            {
                _lower_town.Text = town;
            }

            DataRow[] levels;
            switch (department.Row["deptLevel"].ToString())
            {
                case "0": levels = dt_level.Select("levelid = '1'");
                    ComboboxTool.InitComboboxSource(_level, levels, "lr");
                    _level.SelectedIndex = 1;
                    break;
                case "1": levels = dt_level.Select("levelid > '1'");
                    ComboboxTool.InitComboboxSource(_level, levels, "lr");
                    break;
                case "2": levels = dt_level.Select("levelid > '2'");
                    ComboboxTool.InitComboboxSource(_level, levels, "lr");
                    break;
                case "3": levels = dt_level.Select("levelid = '4'");
                    ComboboxTool.InitComboboxSource(_level, levels, "lr");
                    _level.SelectedIndex = 1;
                    break;
                default: break;
            }
            _station_flag.Text = "(必填)";
            _level_flag.Text = "(必填)";

            //如果当前添加的是检测单位，则显示检测点性质信息
            if (_level.Text == "检测单位")
            {
                _detail_info.RowDefinitions[5].Height = new GridLength(1, GridUnitType.Star);
                //_station_property_flag.Text = "(必填)";
                _station_name.Text = "检测单位名称:";
            }
            else
            {
                _station_name.Text = "部门名称:";
            }

            //新增部门，画面上字段进行初始化
            _dept_type.SelectedIndex = 0;
            _station.Text = "";
            _principal_name.Text = "";
            _principal_phone.Text = "";
            _contact_name.Text = "";
            _contact_phone.Text = "";
            _address.Text = "";
            _maintype.Text = "";
        }

        private void _add_Click(object sender, RoutedEventArgs e)
        {
            if (_station.Text.Length == 0)
            {
                Toolkit.MessageBox.Show("检测点名称不能为空!");
                return;
            }
            state = "add";

            //设置按钮有效性
            _addsamelevel.IsEnabled = false;
            _add.IsEnabled = false;
            _edit.IsEnabled = false;
            _delete.IsEnabled = false;

            _detail_info.IsEnabled = true;
            _regional_level.Width = 0;
            _regional_level.Visibility = Visibility.Hidden;
            _level.Visibility = Visibility.Visible;
            _belong_to.Width = 0;
            _belong_to.Visibility = Visibility.Hidden;
            _area.Visibility = Visibility.Visible;
            _lower_provice.SelectedIndex = 0;
            _lower_city.Items.Clear();
            _lower_country.Items.Clear();
            _lower_town.Items.Clear();

            if (user_flag_tier == "0")
            {
                //_Supplier_name.Visibility = Visibility.Visible;
                //_Supplier.Visibility = Visibility.Visible;
                _detail_info.RowDefinitions[11].Height = new GridLength(1, GridUnitType.Star);
                _Supplier.IsEnabled = true;

                //_dept_type.IsEnabled = true;
            }

            Department department = _add.Tag as Department;

            //下级的所在地默认为上级的，但可以修改
            string provice = GetName(department.Row["province"].ToString());
            if (provice == "")
            {
                _lower_provice.SelectedIndex = 0;
            }
            else
            {
                _lower_provice.Text = provice;
            }
            string city = GetName(department.Row["city"].ToString());
            if (city == "")
            {
                _lower_city.SelectedIndex = 0;
            }
            else
            {
                _lower_city.Text = city;
            }
            string country = GetName(department.Row["country"].ToString());
            if (country == "")
            {
                _lower_country.SelectedIndex = 0;
            }
            else
            {
                _lower_country.Text = country;
            }
            string town = GetName(department.Row["town"].ToString());
            if (town == "")
            {
                _lower_town.SelectedIndex = 0;
            }
            else
            {
                _lower_town.Text = town;
            }

            DataRow[] levels;
            switch (department.Row["deptLevel"].ToString())
            {
                case "0":levels = dt_level.Select("levelid = '1'");
                    ComboboxTool.InitComboboxSource(_level, levels, "lr");
                    _level.SelectedIndex = 1;
                    break;
                case "1":levels = dt_level.Select("levelid > '1'");
                    ComboboxTool.InitComboboxSource(_level, levels, "lr");
                    break;
                case "2": levels = dt_level.Select("levelid > '2'");
                    ComboboxTool.InitComboboxSource(_level, levels, "lr");
                    break;
                case "3": levels = dt_level.Select("levelid = '4'");
                    ComboboxTool.InitComboboxSource(_level, levels, "lr");
                    _level.SelectedIndex = 1;
                    break;
                default: break;
            }
            _station_flag.Text = "(必填)";
            _level_flag.Text = "(必填)";
            
            //如果当前添加的是检测单位，则显示检测点性质信息
            if (_level.Text == "检测单位")
            {
                _detail_info.RowDefinitions[5].Height = new GridLength(1, GridUnitType.Star);
                //_station_property_flag.Text = "(必填)";
                _station_name.Text = "检测单位名称:";
            }
            else
            {
                _station_name.Text = "部门名称:";
            }

            //新增部门，画面上字段进行初始化
            _dept_type.SelectedIndex = 0;
            _superior_department.Text = _station.Text;
            _station.Text = "";
            _principal_name.Text = "";
            _principal_phone.Text = "";
            _contact_name.Text = "";
            _contact_phone.Text = "";
            _address.Text = "";
            _maintype.Text = "";
        }

        private void _delete_Click(object sender, RoutedEventArgs e)
        {
            Department department = _delete.Tag as Department;
            bool result = sysSetService.ExistsDeleteDept(department.Row["deptId"].ToString());
            if (result)
            {
                Toolkit.MessageBox.Show("该检测单位已有对应的检测单数据，不能删除！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

       
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(" deptId ='{0}'", department.Row["deptId"]));
            bool result2 = sysSetService.ExistsUserId(sb.ToString());
            if (result2)
            {
                Toolkit.MessageBox.Show("该部门已被用户占用，不能删除！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            bool result3 = sysSetService.ExistsDept(department.Row["deptId"].ToString());

            if (result3)
            {
                Toolkit.MessageBox.Show("该部门已有下级部门，不能删除！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Toolkit.MessageBox.Show("确定要删除该部门吗？", "系统询问", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    bool flag = sysSetService.Delete(department.Row["deptId"].ToString());
                    if (flag)
                    {
                        Toolkit.MessageBox.Show("删除成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        Common.SysLogEntry.WriteLog("部门管理", (System.Windows.Application.Current.Resources["User"] as UserInfo).ShowName, Common.OperationType.Delete, "删除部门信息");
                    }
                    else
                    {
                        Toolkit.MessageBox.Show("删除失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                catch (Exception ee)
                {
                    System.Diagnostics.Debug.WriteLine("SysDeptManager._delete_Click" + ee.Message);
                    Toolkit.MessageBox.Show("数据删除失败!稍后尝试!");
                    return;
                }
            }
            else
            {
                return;
            }
            department.Parent.Children.Remove(department);
            _treeView.DataContext = null;
            departmentViewModel = new FamilyTreeViewModel(this.department);
            _treeView.DataContext = departmentViewModel;

            string searchTxt = "";
            if (department.Parent.Children.Count == 0)
            {
                searchTxt = department.Parent.Name;
                
                //详细信息刷新
                DataRow row = department.Parent.Row;
                load_DeptDetails(row);
                _add.Tag = department.Parent;
                //判断当前部门的上一级是否和登陆者是同一级
                if (user_flag_tier != row["deptLevel"].ToString())
                {
                    _addsamelevel.Tag = department.Parent.Parent;
                }
                _edit.Tag = department.Parent;
                _delete.Tag = department.Parent;
            }
            else
            {
                searchTxt = department.Parent.Children[0].Name;

                //详细信息刷新
                DataRow row = department.Parent.Children[0].Row;
                load_DeptDetails(row);
                _add.Tag = department.Parent.Children[0];
                _addsamelevel.Tag = department.Parent;
                _edit.Tag = department.Parent.Children[0];
                _delete.Tag = department.Parent.Children[0];
            }
            departmentViewModel.SearchText = searchTxt;
            departmentViewModel.SearchCommand.Execute(null);   
        }


        private void Phone_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!isNumberic(text))
                { e.CancelCommand(); }
            }
            else { e.CancelCommand(); }
        }

        private void Phone_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void Phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!isNumberic(e.Text))
            {
                e.Handled = true;
            }
            else
               e.Handled = false;
        }

        //isDigit是否是数字
        public static bool isNumberic(string _string)
        {
            if (string.IsNullOrEmpty(_string))

                return false;
            foreach (char c in _string)
            {
                if (!char.IsDigit(c))
                    //if(c<'0' c="">'9')//最好的方法,在下面测试数据中再加一个0，然后这种方法效率会搞10毫秒左右
                    return false;
            }
            return true;
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            //隐藏toolbar右侧的小箭头
            System.Windows.Controls.ToolBar toolBar = sender as System.Windows.Controls.ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if(overflowGrid != null)
            {
               overflowGrid.Visibility = Visibility.Collapsed;
             }

             var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if(mainPanelBorder != null)
            {
              mainPanelBorder.Margin = new Thickness(0);
            }
        }
    }


    public class FamilyTreeViewModel
    {
        #region Data

        readonly ReadOnlyCollection<DepartmentViewModel> _firstGeneration;
        readonly DepartmentViewModel _rootPerson;
        readonly ICommand _searchCommand;

        IEnumerator<DepartmentViewModel> _matchingPeopleEnumerator;
        string _searchText = String.Empty;

        #endregion // Data

        #region Constructor

        public FamilyTreeViewModel(Department rootPerson)
        {
            _rootPerson = new DepartmentViewModel(rootPerson);

            _firstGeneration = new ReadOnlyCollection<DepartmentViewModel>(
                new DepartmentViewModel[] 
                { 
                    _rootPerson 
                });

            _searchCommand = new SearchFamilyTreeCommand(this);
        }

        #endregion // Constructor

        #region Properties

        #region FirstGeneration

        /// <summary>
        /// Returns a read-only collection containing the first person 
        /// in the family tree, to which the TreeView can bind.
        /// </summary>
        public ReadOnlyCollection<DepartmentViewModel> FirstGeneration
        {
            get { return _firstGeneration; }
        }



        #endregion // FirstGeneration

        #region SearchCommand

        /// <summary>
        /// Returns the command used to execute a search in the family tree.
        /// </summary>
        public ICommand SearchCommand
        {
            get { return _searchCommand; }
        }

        private class SearchFamilyTreeCommand : ICommand
        {
            readonly FamilyTreeViewModel _familyTree;

            public SearchFamilyTreeCommand(FamilyTreeViewModel familyTree)
            {
                _familyTree = familyTree;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            event EventHandler ICommand.CanExecuteChanged
            {
                // I intentionally left these empty because
                // this command never raises the event, and
                // not using the WeakEvent pattern here can
                // cause memory leaks.  WeakEvent pattern is
                // not simple to implement, so why bother.
                add { }
                remove { }
            }

            public void Execute(object parameter)
            {
                _familyTree.PerformSearch();
            }
        }

        #endregion // SearchCommand

        #region SearchText

        /// <summary>
        /// Gets/sets a fragment of the name to search for.
        /// </summary>
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (value == _searchText)
                    return;

                _searchText = value;

                _matchingPeopleEnumerator = null;
            }
        }

        #endregion // SearchText

        #endregion // Properties

        #region Search Logic

        void PerformSearch()
        {
            if (_matchingPeopleEnumerator == null || !_matchingPeopleEnumerator.MoveNext())
                this.VerifyMatchingPeopleEnumerator();

            var person = _matchingPeopleEnumerator.Current;

            if (person == null)
                return;

            // Ensure that this person is in view.
            if (person.Parent != null)
                person.Parent.IsExpanded = true;

            person.IsSelected = true;
        }

        void VerifyMatchingPeopleEnumerator()
        {
            var matches = this.FindMatches(_searchText, _rootPerson);
            _matchingPeopleEnumerator = matches.GetEnumerator();

            if (!_matchingPeopleEnumerator.MoveNext())
            {
                System.Windows.MessageBox.Show(
                    "No matching names were found.",
                    "Try Again",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                    );
            }
        }

        IEnumerable<DepartmentViewModel> FindMatches(string searchText, DepartmentViewModel person)
        {
            if (person.NameContainsText(searchText))
                yield return person;

            foreach (DepartmentViewModel child in person.Children)
                foreach (DepartmentViewModel match in this.FindMatches(searchText, child))
                    yield return match;
        }

        #endregion // Search Logic
    }

    public class DepartmentViewModel : INotifyPropertyChanged
    {
        #region Data

        readonly ReadOnlyCollection<DepartmentViewModel> _children;
        readonly DepartmentViewModel _parent;
        readonly Department _department;

        bool _isExpanded;
        bool _isSelected;

        #endregion // Data

        #region Constructors

        public DepartmentViewModel(Department department)
            : this(department, null)
        {
        }

        private DepartmentViewModel(Department department, DepartmentViewModel parent)
        {
            _department = department;
            _parent = parent;

            _children = new ReadOnlyCollection<DepartmentViewModel>(
                    (from child in _department.Children
                     select new DepartmentViewModel(child, this))
                     .ToList<DepartmentViewModel>());
        }

        #endregion // Constructors

        #region Person Properties

        public ReadOnlyCollection<DepartmentViewModel> Children
        {
            get { return _children; }
        }

        public string Name
        {
            get { return _department.Name; }
        }

        public DataRow Row
        {
            get { return _department.Row; }
        }

        public Department Own
        {
            get { return _department.Own; }
        }

        public BitmapImage Img
        {
            get { return _department.Img; }
        }

        #endregion // Person Properties

        #region Presentation Members

        #region IsExpanded

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }

                // Expand all the way up to the root.
                if (_isExpanded && _parent != null)
                    _parent.IsExpanded = true;
            }
        }

        #endregion // IsExpanded

        #region IsSelected

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        #endregion // IsSelected

        #region NameContainsText

        public bool NameContainsText(string text)
        {
            if (String.IsNullOrEmpty(text) || String.IsNullOrEmpty(this.Name))
                return false;

            return this.Name.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1;
        }

        #endregion // NameContainsText

        #region Parent

        public DepartmentViewModel Parent
        {
            get { return _parent; }
        }

        #endregion // Parent

        #endregion // Presentation Members

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion // INotifyPropertyChanged Members
    }

    public class Department
    {
        readonly List<Department> _children = new List<Department>();
        public IList<Department> Children
        {
            get { return _children; }
        }

        public string Name { get; set; }
        public Department Parent { get; set; }

        public BitmapImage Img
        {
            get
            {
                string uri = @"/Manager/Images/all.png";
                switch (Row["deptLevel"].ToString())
                {
                    case "0": uri = @"/Manager/Images/all.png"; break;
                    case "1": uri = @"/Manager/Images/provice.png"; break;
                    case "2": uri = @"/Manager/Images/city.png"; break;
                    case "3": uri = @"/Manager/Images/country.png"; break;
                    case "4": uri = @"/Manager/Images/dept.png"; break;
                    default: break;
                }

                return new BitmapImage(new Uri("pack://application:,," + uri));
            }
        }
        public DataRow Row { get; set; }

        public Department Own { get { return this; } }
    }

}
