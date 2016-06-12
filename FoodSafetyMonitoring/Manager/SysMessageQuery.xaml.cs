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
    /// SysMessageQuery.xaml 的交互逻辑
    /// </summary>
    public partial class SysMessageQuery : UserControl
    {
      
        private string user_flag_tier;
        private DataTable exporttable;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();
        IOperationContract operationContarct = ServiceFactory.GetWcfService<IOperationContract>();
        public SysMessageQuery()
        {
            InitializeComponent();

          
            dtpStartDate.SelectedDate = DateTime.Now.AddDays(-1);
            dtpEndDate.SelectedDate = DateTime.Now;

            user_flag_tier = PubClass.userInfo.FlagTier;
        }

        private void _query_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DATE(cdate) >= '{0}' and DATE(cdate) <= '{1}'",
                            ((DateTime) dtpStartDate.SelectedDate).ToShortDateString(),
                            ((DateTime)dtpEndDate.SelectedDate).ToShortDateString());
            System.Data.DataTable table = operationContarct.GetMessage(sb.ToString());
            lvlist.DataContext = table;
            exporttable = table;

            _sj.Visibility = Visibility.Visible;
            _hj.Visibility = Visibility.Visible;
            _title.Text = table.Rows.Count.ToString();

            if (table.Rows.Count == 0)
            {
                Toolkit.MessageBox.Show("没有查询到数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }


        public void refresh()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("DATE(cdate) >= '{0}' and DATE(cdate) <= '{1}'",
                                    ((DateTime)dtpStartDate.SelectedDate).ToShortDateString(),
                                    ((DateTime)dtpEndDate.SelectedDate).ToShortDateString()));
            System.Data.DataTable table = operationContarct.GetMessage(sb.ToString());

            lvlist.DataContext = table;
            exporttable = table;

        }

        private void _new_Click(object sender, RoutedEventArgs e)
        {
            NewMessage message = new NewMessage(this);
            message.ShowDialog();
        }
    }
}