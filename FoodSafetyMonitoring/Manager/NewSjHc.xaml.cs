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
using FoodSafety.Model;
using FoodSafety.Service.Contract;
using FoodSafetyMonitoring.Common;
using FoodSafetyMonitoring.dao;
using System.Data;
using Toolkit = Microsoft.Windows.Controls;
using FoodSafetyMonitoring.Manager.UserControls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// NewSjHc.xaml 的交互逻辑
    /// </summary>
    public partial class NewSjHc : Window
    {
        SysSjHcQuery sjhc;
        private string userId = PubClass.userInfo.ID;
        private string deptId = PubClass.userInfo.DepartmentID;
        ISysSetContract sysSetContract = ServiceFactory.GetWcfService<ISysSetContract>();

        public NewSjHc(SysSjHcQuery sjhc_query)
        {
            InitializeComponent();
            this.sjhc = sjhc_query;

        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_sjname.Text.Trim().Length == 0)
            {
                Toolkit.MessageBox.Show("请输入试剂名称！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_number.Text.Trim().Length == 0)
            {
                Toolkit.MessageBox.Show("请输入数量！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string sql = string.Format("insert into t_sjhc(sjname,number,cuserId,buydate) values ('{0}','{1}','{2}','{3}')",
                                        _sjname.Text, _number.Text,
                                       PubClass.userInfo.ID, DateTime.Now);
            bool flag = sysSetContract.AddSjhc(sql);
            if (flag)
            {
                Toolkit.MessageBox.Show("试剂耗材入库成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                sjhc.refresh();
                this.Close();
                return;
            }
            else
            {
                Toolkit.MessageBox.Show("试剂耗材入库失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
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

        private void Number_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
        private void Number_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!isNumbericOrDot(text))
                { e.CancelCommand(); }
            }
            else { e.CancelCommand(); }
        }

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!isNumbericOrDot(e.Text))
            {
                e.Handled = true;
            }
            else
                e.Handled = false;
        }

        //isDigit是否是数字
        public static bool isNumbericOrDot(string _string)
        {
            if (string.IsNullOrEmpty(_string))

                return false;
            foreach (char c in _string)
            {
                if (!(char.IsDigit(c) || c == '.'))
                    //if(c<'0' c="">'9')//最好的方法,在下面测试数据中再加一个0，然后这种方法效率会搞10毫秒左右
                    return false;
            }
            return true;
        }
    }
}
