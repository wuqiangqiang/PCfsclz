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
using System.Windows.Shapes;
using FoodSafety.DI;
using FoodSafety.Service.Contract;
using FoodSafetyMonitoring.dao;
using System.Data;
using DBUtility;
using FoodSafetyMonitoring.Common;
using Toolkit = Microsoft.Windows.Controls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// detectDetailsReviewLt.xaml 的交互逻辑
    /// </summary>
    public partial class detectDetailsReviewFishery : Window
    {
  
        int orderid;
        IOperationContract operationContract = ServiceFactory.GetWcfService<IOperationContract>();

        public detectDetailsReviewFishery(int id)
        {
            InitializeComponent();
            orderid = id;

            DataTable table = operationContract.ExecuteProDetectDetailsFishery(id);
            //给画面上的控件赋值
            //图片地址改为从数据库中获取
            string picture_url = operationContract.GetFisheryUrl();
            if (picture_url == "")
            {
                picture_url = "http://www.zrodo.com:8080/xmjc/";
            }
            _img.Source = new BitmapImage(new Uri(picture_url + table.Rows[0][20].ToString()));
            _orderid.Text = table.Rows[0][15].ToString();
            _areaName.Text = table.Rows[0][8].ToString();
            _companyName.Text = table.Rows[0][9].ToString();
            _itemName.Text = table.Rows[0][3].ToString();
            _objectName.Text = table.Rows[0][4].ToString();
            _sampleName.Text = table.Rows[0][10].ToString();
            _reangetName.Text = table.Rows[0][5].ToString();
            _resultName.Text = table.Rows[0][6].ToString();
            _deptName.Text = table.Rows[0][2].ToString();
            _detectDate.Text = table.Rows[0][1].ToString();
            _detectUserName.Text = table.Rows[0][7].ToString();
            _detectTypeName.Text = table.Rows[0][0].ToString();
            _sensitivityName.Text = table.Rows[0][18].ToString();
            _reviewBz.Text = table.Rows[0][16].ToString();
            _reviewUserid.Text = table.Rows[0][11].ToString();
            _reviewReagent_text.Text = table.Rows[0][12].ToString();
            _reviewResult_text.Text = table.Rows[0][13].ToString();
            _reviewDate.Text = table.Rows[0][14].ToString();
            _sampleNo.Text = table.Rows[0][19].ToString();

            //检测结果为疑似阳性变红
            if (_resultName.Text == "疑似阳性" || _resultName.Text == "确证阳性")
            {
                _resultName.Foreground = Brushes.Red;
            }
            else
            {
                _resultName.Foreground = Brushes.Black;
            }

            if (table.Rows[0][17].ToString() == "0")
            {
                _result_id.Text = "检测卡假阳性";
            }
            else if (table.Rows[0][19].ToString() == "1")
            {
                _result_id.Text = "确证阳性";
            }
            else
            {
                _result_id.Text = "";
            }
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            this.Left += e.HorizontalChange;
            this.Top += e.VerticalChange;
        }

        private void exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Close();
            }
        }

        private void exit_MouseEnter(object sender, MouseEventArgs e)
        {
            exit.Source = new BitmapImage(new Uri("pack://application:,," + "/res/close_on.png"));
        }

        private void exit_MouseLeave(object sender, MouseEventArgs e)
        {
            exit.Source = new BitmapImage(new Uri("pack://application:,," + "/res/close.png"));
        }
    }
}
