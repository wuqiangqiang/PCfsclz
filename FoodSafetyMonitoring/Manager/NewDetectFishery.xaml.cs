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
using FoodSafety.DI;
using FoodSafety.Service.Contract;
using FoodSafetyMonitoring.dao;
using System.Data;
using FoodSafetyMonitoring.Common;
using Toolkit = Microsoft.Windows.Controls;
using DBUtility;
using FoodSafetyMonitoring.Manager.UserControls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// NewDetectFishery.xaml 的交互逻辑
    /// </summary>
    public partial class NewDetectFishery : Window
    {
        private string sampleNo;
        private string sampleId;
        private string objectId;
        string userId = PubClass.userInfo.ID;
        string supplierId = PubClass.userInfo.SupplierId;
        string deptid = PubClass.userInfo.DepartmentID;
        IOperationContract operationContract = ServiceFactory.GetWcfService<IOperationContract>();
        ISysSetContract sysSetContract = ServiceFactory.GetWcfService<ISysSetContract>();
        IDetectContract detectContract = ServiceFactory.GetWcfService<IDetectContract>();
        DataTable ProvinceCityTable;
        public NewDetectFishery(string sample_no , string object_id,string sample_id)
        {
            InitializeComponent();

            this.sampleNo = sample_no;
            this.sampleId = sample_id;
            this.objectId = object_id;

            ProvinceCityTable =PubClass.ProvinceCityTable;
            DataTable table = operationContract.GetDeptProvinceCity(deptid);
            DataRow[] rows;
            if (table.Rows.Count == 0)
            {
                rows = ProvinceCityTable.Select("pid = '0001'");
            }
            else
            {
                rows = table.Select();
            }

            //画面初始化-新增检测单画面
            ComboboxTool.InitComboboxSource(_province, rows, "lr");
            _province.SelectionChanged += new SelectionChangedEventHandler(_province_SelectionChanged);

            //查找登录者部门所属的省份
            string proviceid = operationContract.GetProvince(deptid);
            int i = 1;
            foreach (DataRow row in rows)
            {
                if (row["cityId"].ToString() == proviceid)
                {
                    _province.SelectedIndex = i;
                }
                i = i + 1;
            }

            ComboboxTool.InitComboboxSource(_source_company, operationContract.GetComboUserCompany(userId), "lr");
            ComboboxTool.InitComboboxSource(_sample_no,operationContract.GetComboSampleNo("f",userId), "lr");
            ComboboxTool.InitComboboxSource(_detect_sample, operationContract.GetComboDetSample("f"), "lr");
            ComboboxTool.InitComboboxSource(_detect_object,operationContract.GetComboDetObjectFishery(), "lr");
            ComboboxTool.InitComboboxSource(_detect_item,operationContract.GetComboDetItemFishery(), "lr");
            _detect_item.SelectionChanged += new SelectionChangedEventHandler(_detect_item_SelectionChanged);
            ComboboxTool.InitComboboxSource(_detect_method,operationContract.GetComboDetReagentFishery(), "lr");
            ComboboxTool.InitComboboxSource(_detect_result,operationContract.GetComboDetResult(), "lr");
            _entering_datetime.Text = string.Format("{0:g}", System.DateTime.Now);
            _source_company.SelectionChanged += new SelectionChangedEventHandler(_source_company_SelectionChanged);
            _detect_person.Text = PubClass.userInfo.ShowName;
            _detect_site.Text = sysSetContract.GetDeptName(PubClass.userInfo.DepartmentID);

            //赋值样品编号、样品名称、样品分类
            for (int j = 0; j < _sample_no.Items.Count; j++)
            {
                if ((_sample_no.Items[j] as Label).Tag.ToString() == sampleNo)
                {
                    _sample_no.SelectedItem = _sample_no.Items[j];
                    break;
                }
            }

            for (int j = 0; j < _detect_sample.Items.Count; j++)
            {
                if ((_detect_sample.Items[j] as Label).Tag.ToString() == sampleId)
                {
                    _detect_sample.SelectedItem = _detect_sample.Items[j];
                    break;
                }
            }

            for (int j = 0; j < _detect_object.Items.Count; j++)
            {
                if ((_detect_object.Items[j] as Label).Tag.ToString() == objectId)
                {
                    _detect_object.SelectedItem = _detect_object.Items[j];
                    break;
                }
            }
        }

        private void clear()
        {
            this._detect_item.SelectedIndex = 0;
            this._detect_method.SelectedIndex = 0;
            this._detect_sensitivity.SelectedIndex = 0;
            this._detect_result.SelectedIndex = 0;
            this._entering_datetime.Text = string.Format("{0:g}", System.DateTime.Now);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_province.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择省！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_city.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择市！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_region.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择区！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_source_company.SelectedIndex == 0 || _source_company.Text == "")
            {
                Toolkit.MessageBox.Show("请选择被检单位！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_detect_item.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择检查项目！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_detect_method.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择检测方法！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_sample_no.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("样品编号不能为空！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_detect_sample.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("样品名称不能为空！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_detect_object.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("样品分类不能为空！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_detect_sensitivity.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择检测灵敏度！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_detect_result.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择检测结果！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_detect_person.Text.Trim().Length == 0)
            {
                Toolkit.MessageBox.Show("请选择检测师！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //判断被检单位是否存在，若不存在则插入数据库
            string company_id = operationContract.GetCompanyId(_source_company.Text.Trim(), PubClass.userInfo.DepartmentID);
            if (string.IsNullOrEmpty(company_id))
            {
                bool flag = operationContract.AddCompany(_source_company.Text, (_region.SelectedItem as Label).Tag.ToString(), "1", PubClass.userInfo.DepartmentID, PubClass.userInfo.ID, DateTime.Now);
                if (!flag)
                {
                    Toolkit.MessageBox.Show("被检单位添加失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

            bool flag1 = detectContract.ExecuteProAddDetectFishery(company_id, (_detect_item.SelectedItem as Label).Tag.ToString(), (_detect_method.SelectedItem as Label).Tag.ToString(), (_sample_no.SelectedItem as Label).Tag.ToString(), (_detect_object.SelectedItem as Label).Tag.ToString(), (_detect_sample.SelectedItem as Label).Tag.ToString(), (_detect_sensitivity.SelectedItem as Label).Tag.ToString(),(_detect_result.SelectedItem as Label).Tag.ToString(), PubClass.userInfo.DepartmentID, PubClass.userInfo.ID,DateTime.Now);
            if (flag1)
            {
                Toolkit.MessageBox.Show("添加成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                Common.SysLogEntry.WriteLog("水产品检测单录入", PubClass.userInfo.ShowName, OperationType.Add, "新增检测单");
                clear();
                ComboboxTool.InitComboboxSource(_source_company, operationContract.GetComboUserCompany(userId), "lr");
            }
            else
            {
                Toolkit.MessageBox.Show("添加失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            this._province.SelectedIndex = 0;
            this._city.SelectedIndex = 0;
            this._region.SelectedIndex = 0;
            this._source_company.SelectedIndex = 0;
            this._detect_item.SelectedIndex = 0;
            this._detect_method.SelectedIndex = 0;
            this._detect_sensitivity.SelectedIndex = 0;
            this._detect_result.SelectedIndex = 0;
            this._entering_datetime.Text = string.Format("{0:g}", System.DateTime.Now);
        }

        void _source_company_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //获取变更前的状态
            bool flag = _province.IsEnabled;

            //被检单位下拉选择的是有效内容，则将省市区的下拉灰显并且自动赋值
            if (_source_company.SelectedIndex >= 1)
            {
                _province.IsEnabled = false;
                _city.IsEnabled = false;
                _region.IsEnabled = false;

                string areaid = operationContract.GetCompanyArea((_source_company.SelectedItem as Label).Tag.ToString());

                _source_company.Tag = areaid;
                if (areaid.Length > 0)
                {
                    string _areaid = areaid.Substring(0, 2);
                    _province.Text = ProvinceCityTable.Select("cityId = '" + _areaid + "'")[0]["cityName"].ToString();
                }
                if (areaid.Length > 2)
                {
                    string _areaid = areaid.Substring(0, 4);
                    _city.Text = ProvinceCityTable.Select("cityId = '" + _areaid + "'")[0]["cityName"].ToString();
                }
                if (areaid.Length > 4)
                {
                    _region.Text = ProvinceCityTable.Select("cityId = '" + areaid + "'")[0]["cityName"].ToString();
                }
            }
            //被检单位下拉选择的是“-请选择-”或是手动输入被检单位，则将省市区的下拉激活并且内容清空
            else if (_source_company.SelectedIndex < 1)
            {
                if (flag == false)
                {
                    _province.IsEnabled = true;
                    _city.IsEnabled = true;
                    _region.IsEnabled = true;

                    _province.SelectedIndex = 0;
                    _city.SelectedIndex = 0;
                    _region.SelectedIndex = 0;
                }
            }
        }

        void _detect_item_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_detect_item.SelectedIndex > 0)
            {
                ComboboxTool.InitComboboxSource(_detect_sensitivity, operationContract.GetComboExecuteProDetect("f", (_detect_item.SelectedItem as Label).Tag, (_detect_object.SelectedItem as Label).Tag), "lr");
            }
        }

        void _province_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_province.SelectedIndex > 0)
            {
                DataRow[] rows = ProvinceCityTable.Select("pid = '" + (_province.SelectedItem as Label).Tag.ToString() + "'");
                ComboboxTool.InitComboboxSource(_city, rows, "lr");
                _city.SelectionChanged += new SelectionChangedEventHandler(_city_SelectionChanged);
            }
        }

        void _city_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_city.SelectedIndex > 0)
            {
                DataRow[] rows = ProvinceCityTable.Select("pid = '" + (_city.SelectedItem as Label).Tag.ToString() + "'");
                ComboboxTool.InitComboboxSource(_region, rows, "lr");
            }
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

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            this.Left += e.HorizontalChange;
            this.Top += e.VerticalChange;
        }
    }
}
