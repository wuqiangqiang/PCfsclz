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
using Toolkit = Microsoft.Windows.Controls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class SysWarningInfo : UserControl
    {
        private DataTable current_table;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();
        private string user_flag_tier;
        private string dept_type;
        IOperationContract operationService = ServiceFactory.GetWcfService<IOperationContract>();
        public SysWarningInfo(string depttype)
        {
            InitializeComponent();
            this.dept_type = depttype;
            user_flag_tier = PubClass.userInfo.FlagTier;

            MyColumns.Add("zj", new MyColumn("zj", "主键") { BShow = false });
            MyColumns.Add("partid", new MyColumn("partid", "检测单位id") { BShow = false });
            MyColumns.Add("partname", new MyColumn("partname", "部门名称") { BShow = true, Width = 18 });
            MyColumns.Add("itemid", new MyColumn("itemid", "检测项目id") { BShow = false });
            MyColumns.Add("itemname", new MyColumn("itemname", "检测项目") { BShow = true, Width = 14 });
            MyColumns.Add("objectid", new MyColumn("objectid", "检测对象id") { BShow = false });
            MyColumns.Add("objectname", new MyColumn("objectname", "检测对象") { BShow = true, Width = 12 });
            MyColumns.Add("yang_like", new MyColumn("yang_like", "疑似阳性") { BShow = true, Width = 12 });
            MyColumns.Add("yang", new MyColumn("yang", "阳性") { BShow = true, Width = 12 });
            MyColumns.Add("count", new MyColumn("count", "合计数量") { BShow = true, Width = 12 });
            MyColumns.Add("yang_like_sum", new MyColumn("yang_like_sum", "疑似阳性合计") { BShow = false });
            MyColumns.Add("sum_num", new MyColumn("sum_num", "总行数") { BShow = false });
            MyColumns.Add("flagtier", new MyColumn("flagtier", "部门级别") { BShow = false });

            _tableview.MyColumns = MyColumns;
            _tableview.BShowDetails = true;
            
            _tableview.DetailsRowEnvent += new UcTableOperableView_NoTitle.DetailsRowEventHandler(_tableview_DetailsRowEnvent);
            _tableview.GetDataByPageNumberEvent += new UcTableOperableView_NoTitle.GetDataByPageNumberEventHandler(_tableview_GetDataByPageNumberEvent);
            GetData();
        }

        private void GetData()
        {
            string function = "";
            switch (dept_type)
            {
                case "0": function = "p_warning_info_produce";
                    break;
                case "1": function = "p_warning_info_fishery";
                    break;
                case "2": function = "p_warning_info_animal";
                    break;
                default: break;
            }

            DataTable table = operationService.ExecuteProWarningInfo(function, PubClass.userInfo.ID,
                                                                      (_tableview.PageIndex - 1)*_tableview.RowMax,
                                                                      _tableview.RowMax);

            _tableview.Table = table;
            current_table = table;

            string sum = "";
            if(table.Rows.Count != 0)
            {
                sum = table.Rows[0][10].ToString();
                _sj.Visibility = Visibility.Visible;
                _hj.Visibility = Visibility.Visible;
                _title.Text = sum;
            }
            
            _tableview.PageIndex = 1;
        }

        void _tableview_GetDataByPageNumberEvent()
        {
            GetData();
        }

        void _tableview_DetailsRowEnvent(string id)
        {
            string dept_id;
            string item_id;
            string object_id;
            string flag_tier;

            int selectrow = int.Parse(id);

            dept_id = current_table.Rows[selectrow - 1][1].ToString();
            item_id = current_table.Rows[selectrow - 1][3].ToString();
            object_id = current_table.Rows[selectrow - 1][5].ToString();
            flag_tier = current_table.Rows[selectrow - 1][12].ToString();

            if (flag_tier == "4")
            {
                switch (dept_type)
                {
                    case "0": grid_info.Children.Add(new UcWarningdetailsProduce(dept_id, item_id, object_id));
                        break;
                    case "1": grid_info.Children.Add(new UcWarningdetailsFishery(dept_id, item_id, object_id));
                        break;
                    case "2": grid_info.Children.Add(new UcWarningdetailsAnimal(dept_id, item_id, object_id));
                        break;
                    default: break;
                }
            }
            else
            {
                grid_info.Children.Add(new UcWarningCountry(dept_id, item_id, object_id,dept_type));
            }
        }

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            string function = "";
            switch (dept_type)
            {
                case "0": function = "p_warning_info_produce";
                    break;
                case "1": function = "p_warning_info_fishery";
                    break;
                case "2": function = "p_warning_info_animal";
                    break;
                default: break;
            }
           
            DataTable table = operationService.ExecuteProWarningInfo(function, PubClass.userInfo.ID,
                                                                      0,
                                                                      _tableview.RowMax);
            _tableview.ExportExcel(table);
        }

    }
}
