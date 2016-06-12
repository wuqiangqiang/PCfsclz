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
using FoodSafetyMonitoring.Common;
using FoodSafetyMonitoring.dao;
using System.Data;
using FoodSafetyMonitoring.Manager.UserControls;
using Toolkit = Microsoft.Windows.Controls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysReviewInfoLt.xaml 的交互逻辑
    /// </summary>
    public partial class SysReviewInfoFishery : UserControl
    {
    
        private DataTable current_table;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();
        IOperationContract operationContract = ServiceFactory.GetWcfService<IOperationContract>();
        public SysReviewInfoFishery()
        {
            InitializeComponent();

            //初始化查询条件
            reportDate_kssj.SelectedDate = DateTime.Now.AddDays(-1);
            reportDate_jssj.SelectedDate = DateTime.Now;
            //检测单位
            ComboboxTool.InitComboboxSource(_detect_dept, operationContract.ExecuteProUserDept(PubClass.userInfo.ID), "cxtj");
            //检测项目
            ComboboxTool.InitComboboxSource(_detect_item, operationContract.GetComboDetItemFishery(), "cxtj");

            MyColumns.Add("orderid", new MyColumn("orderid", "检测单编号") { BShow = true, Width = 8 });
            MyColumns.Add("detecttypename", new MyColumn("detecttypename", "信息来源") { BShow = true, Width = 8 });
            MyColumns.Add("detectdate", new MyColumn("detectdate", "检测时间") { BShow = true, Width = 18 });
            MyColumns.Add("partname", new MyColumn("partname", "检测单位") { BShow = true, Width = 16 });
            MyColumns.Add("itemname", new MyColumn("itemname", "检测项目") { BShow = true, Width = 10 });
            MyColumns.Add("objectname", new MyColumn("objectname", "样品分类") { BShow = true, Width = 8 });
            MyColumns.Add("reagentname", new MyColumn("reagentname", "检测方法") { BShow = true, Width = 10 });
            MyColumns.Add("sensitivityname", new MyColumn("sensitivityname", "检测灵敏度") { BShow = true, Width = 10 });
            MyColumns.Add("resultname", new MyColumn("resultname", "检测结果") { BShow = true, Width = 8 });
            MyColumns.Add("detectusername", new MyColumn("detectusername", "检测师") { BShow = true, Width = 10 });
            MyColumns.Add("areaname", new MyColumn("areaname", "来源产地") { BShow = false });
            MyColumns.Add("companyname", new MyColumn("companyname", "被检单位") { BShow = true, Width = 16 });
            MyColumns.Add("reviewflagname", new MyColumn("reviewflagname", "是否复核") { BShow = false });
            MyColumns.Add("sum_num", new MyColumn("sum_num", "总行数") { BShow = false });

            _tableview.MyColumns = MyColumns;
            _tableview.BShowState = true;
            _tableview.StateRowEnvent += new UcTableOperableView_NoTitle.StateRowEventHandler(_tableview_StateRowEnvent);
        }

        private void _query_Click(object sender, RoutedEventArgs e)
        {
            if (reportDate_kssj.SelectedDate.Value.Date > reportDate_jssj.SelectedDate.Value.Date)
            {
                Toolkit.MessageBox.Show("开始时间大于结束时间，请重新选择！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
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
                Toolkit.MessageBox.Show("没有查询到数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

        }

        public void GetData()
        {
            DataTable table = operationContract.ExecuteProReviewInfo("f", PubClass.userInfo.ID, (DateTime)reportDate_kssj.SelectedDate, (DateTime)reportDate_jssj.SelectedDate, _detect_dept.SelectedIndex < 1 ? "" : (_detect_dept.SelectedItem as Label).Tag.ToString(), _detect_item.SelectedIndex < 1 ? "" : (_detect_item.SelectedItem as Label).Tag.ToString(), (_tableview.PageIndex - 1) * _tableview.RowMax, _tableview.RowMax);


            _tableview.Table = table;
            current_table = table;
        }

        void _tableview_GetDataByPageNumberEvent()
        {
            GetData();
        }

        void _tableview_StateRowEnvent(string id)
        {
            int orderid = int.Parse(id);
            AddReviewDetailsFishery det = new AddReviewDetailsFishery(orderid, this);
            det.ShowDialog();

        }

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = operationContract.ExecuteProReviewInfo("f", PubClass.userInfo.ID, (DateTime)reportDate_kssj.SelectedDate, (DateTime)reportDate_jssj.SelectedDate, _detect_dept.SelectedIndex < 1 ? "" : (_detect_dept.SelectedItem as Label).Tag.ToString(), _detect_item.SelectedIndex < 1 ? "" : (_detect_item.SelectedItem as Label).Tag.ToString(), (_tableview.PageIndex - 1) * _tableview.RowMax, _tableview.RowMax);

            _tableview.ExportExcel(table);
        }
    }
}
