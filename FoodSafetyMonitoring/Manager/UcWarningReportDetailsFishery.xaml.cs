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
using System.Data;
using FoodSafety.DI;
using FoodSafety.Service.Contract;
using FoodSafetyMonitoring.dao;
using FoodSafetyMonitoring.Manager.UserControls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// UcWarningReportDetailsLt.xaml 的交互逻辑
    /// </summary>
    public partial class UcWarningReportDetailsFishery : UserControl
    {

        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();
        public string Kssj { get; set; }
        public string Jssj { get; set; }
        public string DeptId { get; set; }
        public string ItemId { get; set; }
        public string ReviewFlag { get; set; }
        IOperationContract operationContract = ServiceFactory.GetWcfService<IOperationContract>();

        public UcWarningReportDetailsFishery(string kssj, string jssj, string dept_id, string item_id, string review_id)
        {
            InitializeComponent();

            this.Kssj = kssj;
            this.Jssj = jssj;
            this.DeptId = dept_id;
            this.ItemId = item_id;
            this.ReviewFlag = review_id;

            MyColumns.Add("orderid", new MyColumn("orderid", "检测单编号") { BShow = true, Width = 8 });
            MyColumns.Add("reviewflagname", new MyColumn("reviewflagname", "复核标志") { BShow = false, Width = 8 });
            MyColumns.Add("detecttypename", new MyColumn("detecttypename", "信息来源") { BShow = true, Width = 8 });
            MyColumns.Add("detectdate", new MyColumn("detectdate", "检测时间") { BShow = true, Width = 16 });
            MyColumns.Add("partname", new MyColumn("partname", "检测单位") { BShow = true, Width = 16 });
            MyColumns.Add("itemname", new MyColumn("itemname", "检测项目") { BShow = true, Width = 12 });
            MyColumns.Add("objectname", new MyColumn("objectname", "样品分类") { BShow = true, Width = 8 });
            MyColumns.Add("samplename", new MyColumn("samplename", "样品名称") { BShow = true, Width = 8 });
            MyColumns.Add("sampleno", new MyColumn("sampleno", "样品编号") { BShow = true, Width = 8 });
            MyColumns.Add("sensitivityname", new MyColumn("sensitivityname", "检测灵敏度") { BShow = true, Width = 10 });
            MyColumns.Add("reagentname", new MyColumn("reagentname", "检测方法") { BShow = true, Width = 10 });
            MyColumns.Add("resultname", new MyColumn("resultname", "检测结果") { BShow = true, Width = 8 });
            MyColumns.Add("detectusername", new MyColumn("detectusername", "检测师") { BShow = true, Width = 10 });
            MyColumns.Add("areaname", new MyColumn("areaname", "来源产地") { BShow = false });
            MyColumns.Add("companyname", new MyColumn("companyname", "被检单位") { BShow = true, Width = 16 });
            MyColumns.Add("sum_num", new MyColumn("sum_num", "总行数") { BShow = false });

            _tableview.MyColumns = MyColumns;
            _tableview.BShowState = true;
            _tableview.GetDataByPageNumberEvent += new UcTableOperableView_NoTitle.GetDataByPageNumberEventHandler(_tableview_GetDataByPageNumberEvent);
            _tableview.StateRowEnvent += new UcTableOperableView_NoTitle.StateRowEventHandler(_tableview_StateRowEnvent);
            _tableview.PageIndex = 1;
            GetData();
        }

        private void GetData()
        {
            DataTable table = operationContract.ExecuteProWarningReportDetails("f", Kssj, Jssj, DeptId, ItemId,
                                                                                 ReviewFlag,
                                                                                 (_tableview.PageIndex - 1) *
                                                                                 _tableview.RowMax, _tableview.RowMax);

            _tableview.Table = table;
            _sj.Visibility = Visibility.Visible;
            _hj.Visibility = Visibility.Visible;
            _title.Text = _tableview.RowTotal.ToString();
        }

        void _tableview_GetDataByPageNumberEvent()
        {
            GetData();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        void _tableview_StateRowEnvent(string id)
        {
            int orderid = int.Parse(id);
            string reviewflag = operationContract.GetReviewFlag("f", orderid);
            if (reviewflag == "1")
            {
                detectDetailsReviewFishery det = new detectDetailsReviewFishery(orderid);
                det.ShowDialog();
            }
            else
            {
                detectdetailsFishery det = new detectdetailsFishery(orderid);
                det.ShowDialog();
            }

        }
    }
}
