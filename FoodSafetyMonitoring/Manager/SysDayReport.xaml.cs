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
using System.Windows.Forms.Integration;
using System.Data;
using FoodSafetyMonitoring.Common;
using FoodSafetyMonitoring.Manager.UserControls;
using Toolkit = Microsoft.Windows.Controls;

namespace FoodSafetyMonitoring.Manager
{ 
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class SysDayReport : UserControl
    {
    
        private string user_flag_tier;
        private DataTable currenttable;
        private string item_id;
        private string result_id;
        private string dept_type;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();

        private List<DeptItemInfo> list = new List<DeptItemInfo>();
        IOperationContract operationService = ServiceFactory.GetWcfService<IOperationContract>();
        public SysDayReport(string depttype)
        {
            InitializeComponent();
            this.dept_type = depttype;
            user_flag_tier = PubClass.userInfo.FlagTier;

            reportDate.SelectedDate = DateTime.Now;
            //检测单位
            ComboboxTool.InitComboboxSource(_detect_dept,operationService.ExecuteProCxtj(PubClass.userInfo.ID), "cxtj");
            //检测结果
            ComboboxTool.InitComboboxSource(_detect_result, operationService.GetComboDetResult(), "cxtj");

            //检测项目
            switch (dept_type)
            {
                case "0": ComboboxTool.InitComboboxSource(_detect_item,operationService.GetComboDetItemProduce(), "cxtj");
                    break;
                case "1": ComboboxTool.InitComboboxSource(_detect_item,operationService.GetComboDetItemFishery(), "cxtj");
                    break;
                case "2": ComboboxTool.InitComboboxSource(_detect_item,operationService.GetComboDetItemAnimal(), "cxtj");
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
            result_id = _detect_result.SelectedIndex < 1 ? "" : (_detect_result.SelectedItem as Label).Tag.ToString();

            string function = "";
            switch (dept_type)
            {
                case "0": function = "p_report_day_produce";
                    break;
                case "1": function = "p_report_day_fishery";
                    break;
                case "2": function = "p_report_day_animal";
                    break;
                default: break;
            }

            grid_info.Children.Clear();
            grid_info.Children.Add(_tableview);
            MyColumns.Clear();

            DataTable table = operationService.ExecuteProDayReport(function, PubClass.userInfo.ID,(DateTime)reportDate.SelectedDate, _detect_dept.SelectedIndex < 1 ? "" : (_detect_dept.SelectedItem as Label).Tag.ToString(), item_id, result_id);

            currenttable = table;
            list.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DeptItemInfo info = new DeptItemInfo();
                //info.DeptId = table.Rows[i][0].ToString();
                info.DeptName = table.Rows[i][1].ToString();
                //info.ItemId = table.Rows[i][2].ToString();
                info.ItemName = table.Rows[i][3].ToString();
                info.Count = table.Rows[i][4].ToString();
                info.Sum = table.Rows[i][5].ToString();
                info.Yin = table.Rows[i][6].ToString();
                info.Yang = table.Rows[i][7].ToString();
                info.Yisi = table.Rows[i][8].ToString();

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
            }
            //表格合计列
            tabledisplay.Columns.Add(new DataColumn("合计"));
            MyColumns.Add("合计", new MyColumn("合计", "合计") { BShow = true, Width = 10 });
            
            switch(result_id)
            {
                case "": tabledisplay.Columns.Add(new DataColumn("阴性样本"));
                    tabledisplay.Columns.Add(new DataColumn("疑似阳性样本"));
                    tabledisplay.Columns.Add(new DataColumn("阳性样本"));
                    MyColumns.Add("阴性样本", new MyColumn("阴性样本", "阴性样本") { BShow = true, Width = 10 });
                    MyColumns.Add("疑似阳性样本", new MyColumn("疑似阳性样本", "疑似阳性样本") { BShow = true, Width = 10 });
                    MyColumns.Add("阳性样本", new MyColumn("阳性样本", "阳性样本") { BShow = true, Width = 10 });
                    break;
                case "1": tabledisplay.Columns.Add(new DataColumn("阴性样本"));
                    MyColumns.Add("阴性样本", new MyColumn("阴性样本", "阴性样本") { BShow = true, Width = 10 });
                    break;
                case "0":tabledisplay.Columns.Add(new DataColumn("疑似阳性样本"));
                    MyColumns.Add("疑似阳性样本", new MyColumn("疑似阳性样本", "疑似阳性样本") { BShow = true, Width = 10 });
                    break;
                case "2":tabledisplay.Columns.Add(new DataColumn("阳性样本"));
                    MyColumns.Add("阳性样本", new MyColumn("阳性样本", "阳性样本") { BShow = true, Width = 10 });
                    break;
                default: break;
            }

            //为表中各行生成数据
            for (int i = 0; i < DeptNames.Length; i++)
            {
                var row = tabledisplay.NewRow();
                //每行第0列为行分组关键字
                row[0] = i + 1 ;
                row[1] = DeptNames[i];

                //每行的其余列为行列交叉对应的汇总数据
                for (int j = 0; j < ItemNames.Length; j++)
                {
                    string num = list.Where(t => t.DeptName == DeptNames[i] && t.ItemName == ItemNames[j]).Select(t => t.Count).FirstOrDefault();
                    
                    if (num == null ||  num == "")
                    {
                        num = '0'.ToString();
                    }
                    row[ItemNames[j]] = num;
                }
                row[ItemNames.Length + 2] = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.Sum).FirstOrDefault();

                switch (result_id)
                {
                    case "": row[ItemNames.Length + 3] = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.Yin).FirstOrDefault();
                        row[ItemNames.Length + 4] = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.Yisi).FirstOrDefault();
                        row[ItemNames.Length + 5] = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.Yang).FirstOrDefault();
                        break;
                    case "1": row[ItemNames.Length + 3] = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.Yin).FirstOrDefault();
                        break;
                    case "0": row[ItemNames.Length + 3] = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.Yisi).FirstOrDefault();
                        break;
                    case "2": row[ItemNames.Length + 3] = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.Yang).FirstOrDefault();
                        break;
                    default: break;
                }
                
                tabledisplay.Rows.Add(row);
            }

            //计算报表总条数
            int row_count = 0 ;

            if (table.Rows.Count != 0)
            {
                //表格最后添加合计行
                tabledisplay.Rows.Add(tabledisplay.NewRow()[1] = "合计");
                for (int j = 2; j < tabledisplay.Columns.Count; j++)
                {
                    int sum = 0;
                    for (int i = 0; i < tabledisplay.Rows.Count - 1 ; i++)
                    {
                        sum += Convert.ToInt32(tabledisplay.Rows[i][j].ToString());
                    }
                    //sum_column += sum;
                    tabledisplay.Rows[tabledisplay.Rows.Count - 1][j] = sum;
                }

                row_count =  tabledisplay.Rows.Count - 1;
            }
            else
            {
                row_count = 0 ;
            }

            _tableview.MyColumns = MyColumns;
            _tableview.BShowDetails = true;
            _tableview.Table = tabledisplay;

            if (row_count == 0)
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
                    case "0": grid_info.Children.Add(new UcDayReportDetailsProduce(reportDate.SelectedDate.ToString(), dept_id, item_id, result_id));
                              break;
                    case "1": grid_info.Children.Add(new UcDayReportDetailsFishery(reportDate.SelectedDate.ToString(), dept_id, item_id, result_id));
                              break;
                    case "2": grid_info.Children.Add(new UcDayReportDetailsAnimal(reportDate.SelectedDate.ToString(), dept_id, item_id, result_id));
                              break;
                    default: break;
                }
            }
            else
            {
                grid_info.Children.Add(new UcDayReportCountry(reportDate.SelectedDate.ToString(), dept_id, item_id, result_id, dept_type));
            }
            
      
        }

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            _tableview.ExportExcel();
        }

        [Serializable]
        public class DeptItemInfo
        {
            //public string DeptId { get; set; }
            
            public string DeptName { get; set; }

            //public string ItemId { get; set; }

            public string ItemName { get; set; }

            public string Count { get; set; }

            public string Sum { get; set; }

            public string Yin { get; set; }

            public string Yang { get; set; }

            public string Yisi { get; set; }


        }
    }
}
