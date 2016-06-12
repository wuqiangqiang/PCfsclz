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
using System.Data;
using FoodSafetyMonitoring.Common;
using Toolkit = Microsoft.Windows.Controls;
using FoodSafetyMonitoring.Manager.UserControls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysUserSignIn.xaml 的交互逻辑
    /// </summary>
    public partial class SysUserSignIn : UserControl
    {
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();
        IOperationContract operationContract = ServiceFactory.GetWcfService<IOperationContract>();
        string userId = PubClass.userInfo.ID;
        string user_flag_tier = PubClass.userInfo.FlagTier;
        private string dept_name;

        public SysUserSignIn()
        {
            InitializeComponent();

            //画面初始化-检测单列表画面
            dtpStartDate.SelectedDate = DateTime.Now.AddDays(-1);
            dtpEndDate.SelectedDate = DateTime.Now;



            //检测站点
            DataTable dtCxtj = operationContract.ExecuteProCxtj(userId);
            ComboboxTool.InitComboboxSource(_detect_station, dtCxtj, "cxtj");
            
            _detect_station.SelectionChanged += new SelectionChangedEventHandler(_detect_station_SelectionChanged);
            DataTable dtDetuser = operationContract.ExecuteProDetUser(userId);
            ComboboxTool.InitComboboxSource(_detect_person1, dtDetuser, "cxtj");

            SetColumns();
        }

        private void SetColumns()
        {
            MyColumns.Add("userid", new MyColumn("userid", "用户id") { BShow = false });
            MyColumns.Add("part_name", new MyColumn("part_name", "部门名称") { BShow = true, Width = 15 });
            MyColumns.Add("username", new MyColumn("username", "检测师名称") { BShow = true, Width = 10 });
            MyColumns.Add("check_count", new MyColumn("check_count", "签到次数") { BShow = true, Width = 10 });
            MyColumns.Add("sum_num", new MyColumn("sum_num", "总行数") { BShow = false });

            _tableview.MyColumns = MyColumns;
            _tableview.BShowDetails = true;

            _tableview.DetailsRowEnvent += new UcTableOperableView_NoTitle.DetailsRowEventHandler(_tableview_DetailsRowEnvent);
        }

        //20150707检测师改为连动（受检测站点影响）
        void _detect_station_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_detect_station.SelectedIndex > 0)
            {
                StringBuilder sb  = new StringBuilder();
                sb.Append(string.Format("deptId like '{0}%'", (_detect_station.SelectedItem as Label).Tag.ToString()));
                DataTable dtClientUser = operationContract.GetSysClientUser(sb.ToString());
                ComboboxTool.InitComboboxSource(_detect_person1, dtClientUser, "cxtj");
            }
            else if (_detect_station.SelectedIndex == 0)
            {
                DataTable dtDetUser = operationContract.ExecuteProDetUser(userId);
                ComboboxTool.InitComboboxSource(_detect_person1, dtDetUser, "cxtj");
            }
        }

        private void _query_Click(object sender, RoutedEventArgs e)
        {
            if (dtpStartDate.SelectedDate.Value.Date > dtpEndDate.SelectedDate.Value.Date)
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

        private void GetData()
        {
            DataTable table = operationContract.ExecuteProSignIn(PubClass.userInfo.ID,((DateTime)dtpStartDate.SelectedDate).ToString("yyyy-MM-dd"),
                  ((DateTime)dtpEndDate.SelectedDate).ToString("yyyy-MM-dd"),
                   _detect_station.SelectedIndex < 1 ? "" : (_detect_station.SelectedItem as Label).Tag.ToString(),
                  _detect_person1.SelectedIndex < 1 ? "" : (_detect_person1.SelectedItem as Label).Tag.ToString(),
                  ((_tableview.PageIndex - 1) * _tableview.RowMax).ToString(), _tableview.RowMax.ToString()); 
 
            _tableview.Table = table;
        }

        void _tableview_GetDataByPageNumberEvent()
        {
            GetData();
        }

        void _tableview_DetailsRowEnvent(string id)
        {
            grid_table.Children.Add(new UcUserSignDetails(id, ((DateTime)dtpStartDate.SelectedDate).ToString("yyyy-MM-dd"), ((DateTime)dtpEndDate.SelectedDate).ToString("yyyy-MM-dd")));
        }

    }
}
