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
using System.Windows.Shapes;
using FoodSafety.Model;
using FoodSafetyMonitoring.Common;
using FoodSafetyMonitoring.dao;
using System.Data;
using Toolkit = Microsoft.Windows.Controls;
using FoodSafetyMonitoring.Manager.UserControls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// NewOrModifyCompany.xaml 的交互逻辑
    /// </summary>
    public partial class NewOrModifyDevice : Window
    {
        DataTable ProvinceCityTable;
        private IDBOperation dbOperation;
        private string companyId;
        SysDeviceQuery company;
        private int flag = 0;
        private string userId = PubClass.userInfo.ID;
        private string deptId = PubClass.userInfo.DepartmentID;

        public NewOrModifyDevice(IDBOperation dbOperation, string company_id, SysDeviceQuery company_query)
        {
            InitializeComponent();
            flag = 0;
            this.dbOperation = dbOperation;
            this.companyId = company_id;
            this.company = company_query;
            ProvinceCityTable = Application.Current.Resources["省市表"] as DataTable;
            DataRow[] rows = ProvinceCityTable.Select("pid = '0001'");

            //来源产地
            ComboboxTool.InitComboboxSource(_province, rows, "cxtj");
            _province.SelectionChanged += new SelectionChangedEventHandler(_province_SelectionChanged);

            DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("select companyName,t_company.deptId,sys_client_sysdept.deptName,areaId,t_company.contacter,t_company.phone " +
                               "from t_company left join sys_client_sysdept  " +
                               "on t_company.deptId = sys_client_sysdept.deptId where companyId = '{0}' ", companyId)).Tables[0];

            if (table.Rows.Count != 0)
            {
                this._companyname.Text = table.Rows[0][0].ToString();
                this._deptname.Text = table.Rows[0][2].ToString();
                this._phone.Text = table.Rows[0][5].ToString();
                this._contacter.Text = table.Rows[0][4].ToString();
                //赋值来源产地
                for (int i = 0; i < _province.Items.Count; i++)
                {
                    if ((_province.Items[i] as Label).Tag.ToString() == table.Rows[0][3].ToString().Substring(0, 2))
                    {
                        _province.SelectedItem = _province.Items[i];
                        break;
                    }
                }

                for (int i = 0; i < _city.Items.Count; i++)
                {
                    if ((_city.Items[i] as Label).Tag.ToString() == table.Rows[0][3].ToString().Substring(0, 4))
                    {
                        _city.SelectedItem = _city.Items[i];
                        break;
                    }
                }

                for (int i = 0; i < _region.Items.Count; i++)
                {
                    if ((_region.Items[i] as Label).Tag.ToString() == table.Rows[0][3].ToString())
                    {
                        _region.SelectedItem = _region.Items[i];
                        break;
                    }
                }
            }

        }

        public NewOrModifyDevice(IDBOperation dbOperation, SysDeviceQuery company_query)
        {
            InitializeComponent();
            flag = 1;
            this.dbOperation = dbOperation;
            this.company = company_query;
            ProvinceCityTable = Application.Current.Resources["省市表"] as DataTable;
            DataRow[] rows = ProvinceCityTable.Select("pid = '0001'");

            //来源产地
            ComboboxTool.InitComboboxSource(_province, rows, "cxtj");
            _province.SelectionChanged += new SelectionChangedEventHandler(_province_SelectionChanged);

            string dept_name = dbOperation.GetDbHelper().GetSingle(string.Format("select deptName from sys_client_sysdept where deptId = '{0}'", deptId)).ToString();

            this._deptname.Text = dept_name;

        }

        void _province_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_province.SelectedIndex > 0)
            {
                DataRow[] rows = ProvinceCityTable.Select("pid = '" + (_province.SelectedItem as Label).Tag.ToString() + "'");
                ComboboxTool.InitComboboxSource(_city, rows, "cxtj");
                _city.SelectionChanged += new SelectionChangedEventHandler(_city_SelectionChanged);
            }
        }


        void _city_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_city.SelectedIndex > 0)
            {
                DataRow[] rows = ProvinceCityTable.Select("pid = '" + (_city.SelectedItem as Label).Tag.ToString() + "'");
                ComboboxTool.InitComboboxSource(_region, rows, "cxtj");
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_companyname.Text.Trim().Length == 0)
            {
                Toolkit.MessageBox.Show("请输入来源单位！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_province.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择来源产地(省)！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_city.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择来源产地(市)！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_region.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择来源产地(区)！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_contacter.Text.Trim().Length == 0)
            {
                Toolkit.MessageBox.Show("请输入负责人姓名！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_phone.Text.Trim().Length == 0)
            {
                Toolkit.MessageBox.Show("请输入手机！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (flag == 0)
            {
                string sql = string.Format("update t_company set companyName = '{0}',phone = '{1}',areaId = '{2}',contacter = '{3}'" +
                                         " where companyId = '{4}'"
                            , _companyname.Text, _phone.Text, (_region.SelectedItem as Label).Tag.ToString(), _contacter.Text, companyId);

                int i = dbOperation.GetDbHelper().ExecuteSql(sql);
                if (i > 0)
                {
                    Toolkit.MessageBox.Show("档案信息更新成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    company.refresh();
                    this.Close();
                    return;
                }
                else
                {
                    Toolkit.MessageBox.Show("档案信息更新失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            else
            {
                string sql = string.Format("insert into t_company(companyName,areaId,openFlag,deptId,cuserId,cdate,phone,contacter) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                                           _companyname.Text, (_region.SelectedItem as Label).Tag.ToString(), '1',
                                           deptId, PubClass.userInfo.ID, DateTime.Now, _phone.Text, _contacter.Text);

                int i = dbOperation.GetDbHelper().ExecuteSql(sql);
                if (i > 0)
                {
                    Toolkit.MessageBox.Show("档案信息新增成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    company.refresh();
                    this.Close();
                    return;
                }
                else
                {
                    Toolkit.MessageBox.Show("档案信息新增失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }


        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            this.Left += e.HorizontalChange;
            this.Top += e.VerticalChange;
        }

        private void exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Close();
            }
        }

        private void exit_MouseEnter(object sender, MouseEventArgs e)
        {
            exit.Source = new BitmapImage(new Uri("pack://application:,," + "/res/close_on.png"));
        }

        private void exit_MouseLeave(object sender, MouseEventArgs e)
        {
            exit.Source = new BitmapImage(new Uri("pack://application:,," + "/res/close.png"));
        }

        private void _phone_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
        private void _phone_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!isNumberic(text))
                { e.CancelCommand(); }
            }
            else { e.CancelCommand(); }
        }

        private void _phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
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


    }
}
