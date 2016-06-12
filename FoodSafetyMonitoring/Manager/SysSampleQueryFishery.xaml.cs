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
    /// SysSampleQuery.xaml 的交互逻辑
    /// </summary>
    public partial class SysSampleQueryFishery: UserControl
    {
       
        //private string user_flag_tier;
        private DataTable currenttable;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();
        private string userId = PubClass.userInfo.ID;
        IOperationContract operationService = ServiceFactory.GetWcfService<IOperationContract>();

        public SysSampleQueryFishery()
        {
            InitializeComponent();
            dtpStartDate.SelectedDate = DateTime.Now.AddDays(-1);
            dtpEndDate.SelectedDate = DateTime.Now;
            //user_flag_tier = PubClass.userInfo.FlagTier;
        }

        private void _query_Click(object sender, RoutedEventArgs e)
        {

            string kssj = DateTime.Parse(dtpStartDate.SelectedDate.ToString()).ToString("yyyy-MM-dd");
            string jssj = DateTime.Parse(dtpEndDate.SelectedDate.ToString()).ToString("yyyy-MM-dd");
            string sampleNo = txt_sampleNo.Text.Trim();
            DataTable table = operationService.GetUserSampleFishery(userId, kssj, jssj,sampleNo);

            currenttable = table;
            lvlist.DataContext = table;

            _sj.Visibility = Visibility.Visible;
            _hj.Visibility = Visibility.Visible;
            _title.Text = table.Rows.Count.ToString();

            if (table.Rows.Count == 0)
            {
                Toolkit.MessageBox.Show("没有查询到数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        private void _btn_detect_Click(object sender, RoutedEventArgs e)
        {
            string object_id;
            string sample_id;
            string sampleno_id = (sender as System.Windows.Controls.Button).Tag.ToString();

            DataRow[] rows = currenttable.Select("id = '" + sampleno_id + "'");
            object_id = rows[0]["objectId"].ToString();
            sample_id = rows[0]["sampleId"].ToString();

            NewDetectFishery message = new NewDetectFishery(sampleno_id, object_id, sample_id);
            message.ShowDialog();
        }

        private void _btn_show_Click(object sender, RoutedEventArgs e)
        {
            string sampleurl = (sender as System.Windows.Controls.Button).Tag.ToString();
            SampleImageShow showimage = new SampleImageShow(sampleurl);
            showimage.ShowDialog();
        }

       
    }
}
