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
using FoodSafetyMonitoring.dao;
using FoodSafetyMonitoring.Common;
using System.Data;
using FoodSafetyMonitoring.Manager.UserControls;
using Toolkit = Microsoft.Windows.Controls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysTaskReport.xaml 的交互逻辑
    /// </summary>
    public partial class SysTaskReport : UserControl
    {
        private string user_flag_tier;
        private string item_id;
        private DataTable currenttable;
        private string dept_type;
        private List<TaskInfo> list = new List<TaskInfo>();
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();
        IOperationContract operationService = ServiceFactory.GetWcfService<IOperationContract>();
        private readonly List<string> year = new List<string>() { "2015", 
            "2016",
            "2017",
            "2018",
            "2019",
            "2020"};//初始化变量

        public SysTaskReport(string depttype)
        {
            InitializeComponent();
            this.dept_type = depttype;
            user_flag_tier = PubClass.userInfo.FlagTier;

            _year.ItemsSource = year;
            for (int i = 0; i < _year.Items.Count; i++)
            {
                if (_year.Items[i].ToString() == DateTime.Now.Year.ToString())
                {
                    _year.SelectedItem = _year.Items[i];
                    break;
                }
            }

            ComboboxTool.InitComboboxSource(_detect_dept, operationService.ExecuteProCxtj(PubClass.userInfo.ID), "cxtj");
            //检测项目
            switch (dept_type)
            {
                case "0": ComboboxTool.InitComboboxSource(_detect_item, operationService.GetComboDetItemProduce(), "cxtj");
                    break;
                case "1": ComboboxTool.InitComboboxSource(_detect_item, operationService.GetComboDetItemProduce(), "cxtj");
                    break;
                case "2": ComboboxTool.InitComboboxSource(_detect_item, operationService.GetComboDetItemProduce(), "cxtj");
                    break;
                default: break;
            }

            //如果登录用户的部门是站点级别，则将查询条件检测单位赋上默认值
            if (user_flag_tier == "4")
            {
                _detect_dept.SelectedIndex = 1;
            }

            _tableview.DetailsRowEnvent += new UcTableOperableView_NoPages.DetailsRowEventHandler(_tableview_DetailsRowEnvent);
        }

        private void _query_Click(object sender, RoutedEventArgs e)
        {
            item_id = _detect_item.SelectedIndex < 1 ? "" : (_detect_item.SelectedItem as Label).Tag.ToString();

            string function = "";
            switch (dept_type)
            {
                case "0": function = "p_task_report_produce";
                    break;
                case "1": function = "p_task_report_fishery";
                    break;
                case "2": function = "p_task_report_animal";
                    break;
                default: break;
            }

            grid_info.Children.Clear();
            grid_info.Children.Add(_tableview);
            MyColumns.Clear();
            DataTable table = operationService.ExecuteProTaskReport(function, PubClass.userInfo.ID, _year.Text,_detect_dept.SelectedIndex < 1? "": (_detect_dept.SelectedItem as Label).Tag.ToString(),item_id);
            currenttable = table;
            list.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                TaskInfo info = new TaskInfo();
                //info.DeptId = table.Rows[i][0].ToString();
                info.DeptName = table.Rows[i][1].ToString();
                //info.ItemId = table.Rows[i][2].ToString();
                info.ItemName = table.Rows[i][3].ToString();
                info.TaskTheory = table.Rows[i][4].ToString();
                info.TaskActual = table.Rows[i][5].ToString();
                info.TaskPercent = table.Rows[i][6].ToString();
                info.SumTheory = table.Rows[i][7].ToString();
                info.SumActual = table.Rows[i][8].ToString();
                info.SumPercent = table.Rows[i][9].ToString();
                list.Add(info);
            }

            //得到行和列标题 及数量            
            string[] DeptNames = list.Select(t => t.DeptName).Distinct().ToArray();
            string[] ItemNames = list.Select(t => t.ItemName).Distinct().ToArray();

            //创建DataTable
            DataTable tabledisplay = new DataTable();

            //表中第一行第一列交叉处一般显示为第1列标题
            tabledisplay.Columns.Add(new DataColumn("序号"));
            MyColumns.Add("序号", new MyColumn("序号", "序号") { BShow = true, Width = 5 });
            tabledisplay.Columns.Add(new DataColumn("部门名称"));
            MyColumns.Add("部门名称", new MyColumn("部门名称", "部门名称") { BShow = true, Width = 16 });

            //表中后面每列的标题其实是列分组的关键字
            for (int i = 0; i < ItemNames.Length; i++)
            {
                DataColumn column = new DataColumn(ItemNames[i]);
                tabledisplay.Columns.Add(column);
                MyColumns.Add(ItemNames[i].ToString().ToLower(), new MyColumn(ItemNames[i].ToString().ToLower(), ItemNames[i].ToString()) { BShow = true, Width = 10 });
                tabledisplay.Columns.Add(new DataColumn("任务完成率"+ i));
                MyColumns.Add("任务完成率" + i, new MyColumn("任务完成率" + i, "任务完成率") { BShow = true, Width = 10 });
            }

            //当选择了检测项目作为查询条件时，不显示任务完成总量和任务总完成率
            bool flag;
            if(item_id == "")
            {
                flag = true;
            }
            else
            {
                flag = false;
            }

            //表格后面为合计列
            tabledisplay.Columns.Add(new DataColumn("任务完成总量"));
            MyColumns.Add("任务完成总量", new MyColumn("任务完成总量", "任务完成总量") { BShow = flag, Width = 10 });
            tabledisplay.Columns.Add(new DataColumn("任务总完成率"));
            MyColumns.Add("任务总完成率", new MyColumn("任务总完成率", "任务总完成率") { BShow = flag, Width = 10 });

            //为表中各行生成数据
            for (int i = 0; i < DeptNames.Length; i++)
            {
                var row = tabledisplay.NewRow();
                //每行第0列为行分组关键字
                row[0] = i + 1;
                row[1] = DeptNames[i];
                //每行的其余列为行列交叉对应的汇总数据
                for (int j = 0; j < ItemNames.Length; j++)
                {
                    string num = list.Where(t => t.DeptName == DeptNames[i] && t.ItemName == ItemNames[j]).Select(t => t.TaskActual).FirstOrDefault();

                    if (num == null || num == "")
                    {
                        num = '0'.ToString();
                    }
                    row[ItemNames[j]] = num;

                    string percent = list.Where(t => t.DeptName == DeptNames[i] && t.ItemName == ItemNames[j]).Select(t => t.TaskPercent).FirstOrDefault();

                    if (percent == null || percent == "")
                    {
                        percent = '0'.ToString();
                    }
                    else
                    {
                        percent = percent + "%";
                    }
                    row[3+2*j] = percent;
                }
                row[ItemNames.Length * 2 + 2] = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.SumActual).FirstOrDefault();

                string sumpercent = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.SumPercent).FirstOrDefault();

                if (sumpercent == null || sumpercent == "")
                {
                    sumpercent = '0'.ToString();
                }
                else
                {
                    sumpercent = sumpercent + "%";
                }
                row[ItemNames.Length * 2 + 3] = sumpercent;


                tabledisplay.Rows.Add(row);
            }

            _tableview.MyColumns = MyColumns;
            _tableview.BShowDetails = true;
            _tableview.Table = tabledisplay;
            //_sj.Visibility = Visibility.Visible;
            //_hj.Visibility = Visibility.Visible;
            //_title.Text = tabledisplay.Rows.Count.ToString();

            if (tabledisplay.Rows.Count == 0)
            {
                Toolkit.MessageBox.Show("没有查询到数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        void _tableview_DetailsRowEnvent(string id)
        {
            string dept_id;
            string flag_tier;

            DataRow[] rows = currenttable.Select("PART_NAME = '" + id + "'");
            dept_id = rows[0]["PART_ID"].ToString();
            flag_tier = rows[0]["flagtier"].ToString();

            if (flag_tier == "4")
            {
                switch (dept_type)
                {
                    case "0": grid_info.Children.Add(new UcTaskReportDetailsProduce(_year.Text, dept_id, item_id));
                        break;
                    case "1": grid_info.Children.Add(new UcTaskReportDetailsFishery(_year.Text, dept_id, item_id));
                        break;
                    case "2": grid_info.Children.Add(new UcTaskReportDetailsAnimal(_year.Text, dept_id, item_id));
                        break;
                    default: break;
                }
            }
            else
            {
                grid_info.Children.Add(new UcTaskReportCountry(_year.Text, dept_id, item_id,dept_type));
            }
        }

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            _tableview.ExportExcel();
        }

        public class TaskInfo
        {
            //public string DeptId { get; set; }

            public string DeptName { get; set; }

            //public string ItemId { get; set; }

            public string ItemName { get; set; }

            public string TaskTheory { get; set; }

            public string TaskActual { get; set; }

            public string TaskPercent { get; set; }

            public string SumTheory { get; set; }

            public string SumActual { get; set; }

            public string SumPercent { get; set; }
        }
    }
}
