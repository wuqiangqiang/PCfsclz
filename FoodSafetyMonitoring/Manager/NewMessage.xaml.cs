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
    /// NewMessage.xaml 的交互逻辑
    /// </summary>
    public partial class NewMessage : Window
    {
        SysMessageQuery message;
        private string userId = PubClass.userInfo.ID;
        private string deptId = PubClass.userInfo.DepartmentID;
        IOperationContract operationService = ServiceFactory.GetWcfService<IOperationContract>();
        public NewMessage(SysMessageQuery message_query)
        {
            InitializeComponent();
            this.message = message_query;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_messagetitle.Text.Trim().Length == 0)
            {
                Toolkit.MessageBox.Show("请输入信息标题！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_messagecontent.Text.Trim().Length == 0)
            {
                Toolkit.MessageBox.Show("请输入信息内容！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            bool i = operationService.AddMessage(_messagetitle.Text, _messagecontent.Text, PubClass.userInfo.ID,
                                                 DateTime.Now);
            if (i)
            {
                Toolkit.MessageBox.Show("信息发布成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                message.refresh();
                this.Close();
                return;
            }
            else
            {
                Toolkit.MessageBox.Show("信息发布失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
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
    }
}
