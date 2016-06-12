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
using FoodSafety.Service.Contract;
using FoodSafetyMonitoring.dao;
using System.Data;
using FoodSafetyMonitoring.Common;
using Toolkit = Microsoft.Windows.Controls;
using FoodSafetyMonitoring.Manager.UserControls;
namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysNewDetectCyQuery.xaml 的交互逻辑
    /// </summary>
    public partial class SysNewDetectAnimalQuery : UserControl
    {
        DataTable ProvinceCityTable;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();

        IOperationContract operationContract = ServiceFactory.GetWcfService<IOperationContract>();
        IDetectContract detectContract = ServiceFactory.GetWcfService<IDetectContract>();
        string userId = PubClass.userInfo.ID;
        public SysNewDetectAnimalQuery()
        {
            InitializeComponent();
            ProvinceCityTable = PubClass.ProvinceCityTable;
            DataRow[] rows = ProvinceCityTable.Select("pid = '0001'");

            //画面初始化-检测单列表画面
            dtpStartDate.SelectedDate = DateTime.Now.AddDays(-1);
            dtpEndDate.SelectedDate = DateTime.Now;
            ComboboxTool.InitComboboxSource(_source_company1,operationContract.ExecuteProUserCompany(userId,""), "cxtj");
            ComboboxTool.InitComboboxSource(_detect_station,operationContract.ExecuteProDetUser(userId), "cxtj");

            ComboboxTool.InitComboboxSource(_detect_item1,operationContract.GetComboDetItemAnimal(), "cxtj");

            ComboboxTool.InitComboboxSource(_detect_object1,operationContract.GetComboDetObjectAnimal(), "cxtj");
            ComboboxTool.InitComboboxSource(_detect_result1,operationContract.GetComboDetResult(), "cxtj");
            ComboboxTool.InitComboboxSource(_detect_person1,operationContract.ExecuteProDetUser(userId), "cxtj");

            ComboboxTool.InitComboboxSource(_detect_method,operationContract.GetComboDetReagentAnimal(), "cxtj");
            ComboboxTool.InitComboboxSource(_detect_type,operationContract.GetComboDetSource(), "cxtj");

            ComboboxTool.InitComboboxSource(_province1, rows, "cxtj");
            _province1.SelectionChanged += new SelectionChangedEventHandler(_province1_SelectionChanged);
            //20150707检测师改为连动（受监测站点影响）
            _detect_station.SelectionChanged += new SelectionChangedEventHandler(_detect_station_SelectionChanged);

            SetColumns();
        }

        private void SetColumns()
        {
            MyColumns.Add("orderid", new MyColumn("orderid", "检测单编号") { BShow = true, Width = 10 });
            MyColumns.Add("detecttype", new MyColumn("detecttype", "数据来源id") { BShow = false });
            MyColumns.Add("detecttypename", new MyColumn("detecttypename", "数据来源") { BShow = true, Width = 8 });
            MyColumns.Add("detectdate", new MyColumn("detectdate", "检测时间") { BShow = true, Width = 18 });
            MyColumns.Add("deptid", new MyColumn("deptid", "检测单位id") { BShow = false });
            MyColumns.Add("deptname", new MyColumn("deptname", "检测单位") { BShow = true, Width = 16 });
            MyColumns.Add("itemid", new MyColumn("itemid", "检测项目id") { BShow = false });
            MyColumns.Add("itemname", new MyColumn("itemname", "检测项目") { BShow = true, Width = 12 });
            MyColumns.Add("objectid", new MyColumn("objectid", "检测对象id") { BShow = false });
            MyColumns.Add("objectname", new MyColumn("objectname", "样品分类") { BShow = true, Width = 8 });
            MyColumns.Add("sampleid", new MyColumn("sampleid", "检测样本id") { BShow = false });
            MyColumns.Add("samplename", new MyColumn("samplename", "样品名称") { BShow = true, Width = 8 });
            MyColumns.Add("sampleno", new MyColumn("sampleno", "样品编号") { BShow = true, Width = 8 });
            MyColumns.Add("sensitivityid", new MyColumn("sensitivityid", "检测灵敏度id") { BShow = false });
            MyColumns.Add("sensitivityname", new MyColumn("sensitivityname", "检测灵敏度") { BShow = true, Width = 10 });
            MyColumns.Add("reagentid", new MyColumn("companyid", "检测方法id") { BShow = false });
            MyColumns.Add("reagentname", new MyColumn("reagentname", "检测方法") { BShow = true, Width = 10 });
            MyColumns.Add("resultid", new MyColumn("resultid", "检测结果id") { BShow = false });
            MyColumns.Add("resultname", new MyColumn("resultname", "检测结果") { BShow = true, Width = 10 });
            MyColumns.Add("detectuserid", new MyColumn("detectuserid", "检测师id") { BShow = false });
            MyColumns.Add("areaname", new MyColumn("areaname", "来源产地") { BShow = true, Width = 18 });
            MyColumns.Add("companyid", new MyColumn("companyid", "被检单位id") { BShow = false });
            MyColumns.Add("companyname", new MyColumn("companyname", "被检单位") { BShow = true, Width = 18 });
            MyColumns.Add("sum_num", new MyColumn("sum_num", "总行数") { BShow = false });

            _tableview.MyColumns = MyColumns;
            _tableview.BShowModify = false;
            _tableview.BShowDetails = true;

            if (PubClass.userInfo.FlagTier == "0")
            {
                _tableview.BShowDelete = true;
            }
            else
            {
                _tableview.BShowDelete = false;
            }

            _tableview.DetailsRowEnvent += new UcTableOperableView_NoTitle.DetailsRowEventHandler(_tableview_DetailsRowEnvent);
            _tableview.DeleteRowEnvent += new UcTableOperableView_NoTitle.DeleteRowEventHandler(_tableview_DeleteRowEnvent);
        }

        void _province1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_province1.SelectedIndex > 0)
            {
                DataRow[] rows = ProvinceCityTable.Select("pid = '" + (_province1.SelectedItem as Label).Tag.ToString() + "'");
                ComboboxTool.InitComboboxSource(_city1, rows, "cxtj");
                //20150707被检单位改为连动（受来源产地影响）
                ComboboxTool.InitComboboxSource(_source_company1, operationContract.ExecuteProUserCompany(userId, (_province1.SelectedItem as Label).Tag.ToString()), "cxtj");
                _city1.SelectionChanged += new SelectionChangedEventHandler(_city1_SelectionChanged);
            }
        }


        void _city1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_city1.SelectedIndex > 0)
            {
                DataRow[] rows = ProvinceCityTable.Select("pid = '" + (_city1.SelectedItem as Label).Tag.ToString() + "'");
                ComboboxTool.InitComboboxSource(_region1, rows, "cxtj");
                //20150707被检单位改为连动（受来源产地影响）
                ComboboxTool.InitComboboxSource(_source_company1, operationContract.ExecuteProUserCompany(userId, (_city1.SelectedItem as Label).Tag.ToString()), "cxtj");

                _region1.SelectionChanged += new SelectionChangedEventHandler(_region1_SelectionChanged);
            }
        }

        //20150707被检单位改为连动（受来源产地影响）
        void _region1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_region1.SelectedIndex > 0)
            {
                ComboboxTool.InitComboboxSource(_source_company1, operationContract.ExecuteProUserCompany(userId, (_region1.SelectedItem as Label).Tag.ToString()), "cxtj");

            }
        }

        //20150707检测师改为连动（受检测单位影响）
        void _detect_station_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_detect_station.SelectedIndex > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append( string.Format("deptId = '{0}'", (_detect_station.SelectedItem as Label).Tag.ToString()));
                DataTable dtUser = operationContract.GetSysClientUser(sb.ToString());
                ComboboxTool.InitComboboxSource(_detect_person1,dtUser, "cxtj");

            }
            else if (_detect_station.SelectedIndex == 0)
            {
                ComboboxTool.InitComboboxSource(_detect_person1, operationContract.ExecuteProDetUser(userId), "cxtj");
            }
        }

        private void _query_Click(object sender, RoutedEventArgs e)
        {
            if (dtpStartDate.SelectedDate.Value.Date > dtpEndDate.SelectedDate.Value.Date)
            {
                Toolkit.MessageBox.Show("开始日期大于结束日期，请重新选择！", "系统提示", MessageBoxButton.OK);
                return;
            }
            _tableview.GetDataByPageNumberEvent += new UcTableOperableView_NoTitle.GetDataByPageNumberEventHandler(_tableview_GetDataByPageNumberEvent);
            GetData();

            _sj.Visibility = Visibility.Visible;
            _hj.Visibility = Visibility.Visible;
            _title.Text = _tableview.RowTotal.ToString();
            _tableview.PageIndex = 1;

            if (_tableview.RowTotal == 0)
            {
                Toolkit.MessageBox.Show("没有查询到数据！", "系统提示", MessageBoxButton.OK);
                return;
            }
        }

        private void GetData()
        {
            DataTable table = operationContract.ExecuteProQueryDetectAnimal(PubClass.userInfo.ID, ((DateTime)dtpStartDate.SelectedDate).ToShortDateString(),
                  ((DateTime)dtpEndDate.SelectedDate).ToShortDateString(),
                  _province1.SelectedIndex < 1 ? "" : (_province1.SelectedItem as Label).Tag.ToString(),
                  _city1.SelectedIndex < 1 ? "" : (_city1.SelectedItem as Label).Tag.ToString(),
                  _region1.SelectedIndex < 1 ? "" : (_region1.SelectedItem as Label).Tag.ToString(),
                  _source_company1.SelectedIndex < 1 ? "" : (_source_company1.SelectedItem as Label).Tag.ToString(),
                   _detect_station.SelectedIndex < 1 ? "" : (_detect_station.SelectedItem as Label).Tag.ToString(),
                  _detect_item1.SelectedIndex < 1 ? "" : (_detect_item1.SelectedItem as Label).Tag.ToString(),
                  _detect_object1.SelectedIndex < 1 ? "" : (_detect_object1.SelectedItem as Label).Tag.ToString(),
                  _detect_result1.SelectedIndex < 1 ? "" : (_detect_result1.SelectedItem as Label).Tag.ToString(),
                  _detect_method.SelectedIndex < 1 ? "" : (_detect_method.SelectedItem as Label).Tag.ToString(),
                  _detect_person1.SelectedIndex < 1 ? "" : (_detect_person1.SelectedItem as Label).Tag.ToString(),
                  _detect_type.SelectedIndex < 1 ? "" : (_detect_type.SelectedItem as Label).Tag.ToString(),
                  (_tableview.PageIndex - 1) * _tableview.RowMax,
                  _tableview.RowMax);

            _tableview.Table = table;
        }

        void _tableview_GetDataByPageNumberEvent()
        {
            GetData();
        }

        void _tableview_DetailsRowEnvent(string id)
        {
            int orderid = int.Parse(id);
            detectdetailsAnimal det = new detectdetailsAnimal(orderid);
            det.ShowDialog();
        }

        void _tableview_DeleteRowEnvent(string id)
        {
            if (Toolkit.MessageBox.Show("确定要删除该条检测单吗？", "系统询问", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    bool reviewflag = detectContract.ExistsDetectReviewAnimal(id);
                    if (reviewflag)
                    {
                        bool result1 = detectContract.DeleteDetectReviewAnimal(id);
                        if (!result1)
                        {
                            Toolkit.MessageBox.Show("删除失败！", "系统提示", MessageBoxButton.OK);
                            return;
                        }
                    }
                    bool result = detectContract.DeleteDetectReportAnimal(id);
                    if (result)
                    {
                        Toolkit.MessageBox.Show("删除成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        GetData();
                    }
                    else
                    {
                        Toolkit.MessageBox.Show("删除失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                catch
                {
                    Toolkit.MessageBox.Show("删除失败2！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
        }

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = operationContract.ExecuteProQueryDetectAnimal(PubClass.userInfo.ID,((DateTime)dtpStartDate.SelectedDate).ToShortDateString(),
                  ((DateTime)dtpEndDate.SelectedDate).ToShortDateString(),
                  _province1.SelectedIndex < 1 ? "" : (_province1.SelectedItem as Label).Tag.ToString(),
                  _city1.SelectedIndex < 1 ? "" : (_city1.SelectedItem as Label).Tag.ToString(),
                  _region1.SelectedIndex < 1 ? "" : (_region1.SelectedItem as Label).Tag.ToString(),
                  _source_company1.SelectedIndex < 1 ? "" : (_source_company1.SelectedItem as Label).Tag.ToString(),
                   _detect_station.SelectedIndex < 1 ? "" : (_detect_station.SelectedItem as Label).Tag.ToString(),
                  _detect_item1.SelectedIndex < 1 ? "" : (_detect_item1.SelectedItem as Label).Tag.ToString(),
                  _detect_object1.SelectedIndex < 1 ? "" : (_detect_object1.SelectedItem as Label).Tag.ToString(),
                  _detect_result1.SelectedIndex < 1 ? "" : (_detect_result1.SelectedItem as Label).Tag.ToString(),
                  _detect_method.SelectedIndex < 1 ? "" : (_detect_method.SelectedItem as Label).Tag.ToString(),
                  _detect_person1.SelectedIndex < 1 ? "" : (_detect_person1.SelectedItem as Label).Tag.ToString(),
                  _detect_type.SelectedIndex < 1 ? "" : (_detect_type.SelectedItem as Label).Tag.ToString(),
                  0,
                  _tableview.RowTotal);
           

            _tableview.ExportExcel(table);
        }

    }
}
