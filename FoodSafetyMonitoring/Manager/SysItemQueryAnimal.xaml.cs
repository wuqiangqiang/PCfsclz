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
    /// SysItemQuery.xaml 的交互逻辑
    /// </summary>
    public partial class SysItemQueryAnimal: UserControl
    {
        private string user_flag_tier;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();
        IDetectContract detectContract = ServiceFactory.GetWcfService<IDetectContract>();
        public SysItemQueryAnimal()
        {
            InitializeComponent();
            user_flag_tier = PubClass.userInfo.FlagTier;
        }

        private void _query_Click(object sender, RoutedEventArgs e)
        {
            System.Data.DataTable table = detectContract.GetDetItem("a", _item_name.Text.Trim());

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


        public void refresh()
        {
            System.Data.DataTable table = detectContract.GetDetItem("a", _item_name.Text.Trim());

            lvlist.DataContext = table;
            _title.Text = table.Rows.Count.ToString();
        }

        private void _new_Click(object sender, RoutedEventArgs e)
        {
            NewOrModifyItemAnimal message = new NewOrModifyItemAnimal(this);
            message.ShowDialog();
        }

        private void _btn_modify_Click(object sender, RoutedEventArgs e)
        {
            string item_id = (sender as System.Windows.Controls.Button).Tag.ToString();
            NewOrModifyItemAnimal message = new NewOrModifyItemAnimal(this, item_id);
            message.ShowDialog();
        }

        private void _btn_delete_Click(object sender, RoutedEventArgs e)
        {
            string item_id = (sender as System.Windows.Controls.Button).Tag.ToString();
            if (Toolkit.MessageBox.Show("确定要删除该检测项目吗？", "系统询问", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    bool result = detectContract.DeleteDetect("a", item_id);
                    if (result)
                    {
                        Toolkit.MessageBox.Show("删除成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        refresh();
                    }
                    else
                    {
                        Toolkit.MessageBox.Show("删除失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                catch
                {
                    Toolkit.MessageBox.Show("删除失败2！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
        }
    }
}
