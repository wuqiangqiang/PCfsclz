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
using FoodSafetyMonitoring.Common;
using FoodSafetyMonitoring.dao;
using System.Data;
using FoodSafetyMonitoring.Manager.UserControls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysTaskCheck.xaml 的交互逻辑
    /// </summary>
    public partial class SysTaskCheck : UserControl
    {
        private DataTable currenttable;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();
        private string user_flag_tier;
        private string dept_name;
        private string dept_type;
        ISysTaskContract sysTaskContract = ServiceFactory.GetWcfService<ISysTaskContract>();
        private readonly List<string> year = new List<string>() { 
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020"};//初始化变量


        public SysTaskCheck(string depttype)
        {

            InitializeComponent();

            user_flag_tier = PubClass.userInfo.FlagTier;
            this.dept_type = depttype;

            _year.ItemsSource = year;
            for (int i = 0; i < _year.Items.Count; i++)
            {
                if (_year.Items[i].ToString() == DateTime.Now.Year.ToString())
                {
                    _year.SelectedItem = _year.Items[i];
                    break;
                }
            }

            loadTaskGrade();

            MyColumns.Add("part_id", new MyColumn("part_id", "检测单位id") { BShow = false });
            MyColumns.Add("part_name", new MyColumn("part_name", "检测单位") { BShow = true, Width = 16 });
            MyColumns.Add("task_theory", new MyColumn("task_theory", "年度任务量") { BShow = true, Width = 14 });
            MyColumns.Add("task_actual", new MyColumn("task_actual", "年度实际完成量") { BShow = true, Width = 14 });
            MyColumns.Add("task_percent", new MyColumn("task_percent", "年度任务完成率") { BShow = true, Width = 14 });
            MyColumns.Add("task_gradename", new MyColumn("task_gradename", "评级") { BShow = true, Width = 10 });
            MyColumns.Add("sum_num", new MyColumn("sum_num", "总行数") { BShow = false });

            _tableview.MyColumns = MyColumns;
            _tableview.DetailsRowEnvent += new UcTableOperableView_NoTitle.DetailsRowEventHandler(_tableview_DetailsRowEnvent);

        }

        private void _query_Click(object sender, RoutedEventArgs e)
        {
            _tableview.GetDataByPageNumberEvent += new UcTableOperableView_NoTitle.GetDataByPageNumberEventHandler(_tableview_GetDataByPageNumberEvent);
            
            GetData();
            _tableview.Title = _year.Text +  "年 检测任务执行绩效考评结果" + "  合计" + _tableview.RowTotal + "条数据";
        }

        private void GetData()
        {
            string function = "";
            switch (dept_type)
            {
                case "0": function = "p_task_check_produce";
                    break;
                case "1": function = "p_task_check_fishery";
                    break;
                case "2": function = "p_task_check_animal";
                    break;
                default: break;
            }

            DataTable table = sysTaskContract.ExecuteProTaskCheck(function, PubClass.userInfo.ID, _year.Text,
                                                                  (_tableview.PageIndex - 1)*_tableview.RowMax,
                                                                  _tableview.RowMax);

            _tableview.Table = table;
            currenttable = table;
        }

        void _tableview_GetDataByPageNumberEvent()
        {
            GetData();
        }

        void _tableview_DetailsRowEnvent(string id)
        {
        
        }

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            string function = "";
            switch (dept_type)
            {
                case "0": function = "p_task_check_produce";
                    break;
                case "1": function = "p_task_check_fishery";
                    break;
                case "2": function = "p_task_check_animal";
                    break;
                default: break;
            }

            DataTable table = sysTaskContract.ExecuteProTaskCheck(function, PubClass.userInfo.ID, _year.Text,0,_tableview.RowMax);

            _tableview.ExportExcel(table);
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            string grade_id = (sender as Button).Tag.ToString();
            SetTaskGrade setgrade = new SetTaskGrade(grade_id, PubClass.userInfo.DepartmentID, currenttable,this);
            setgrade.ShowDialog();
        }

        //加载考评参数设置值
        public void loadTaskGrade()
        {
            DataTable table = sysTaskContract.ExecuteProTaskGrade(PubClass.userInfo.ID);

            lvlist2.DataContext = null;
            lvlist2.DataContext = table;

            currenttable = table;
        }

        private void _tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
