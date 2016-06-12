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
using System.IO;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysSjHcQuery.xaml 的交互逻辑
    /// </summary>
    public partial class SysSjHcQuery : UserControl
    {
        private string user_flag_tier;
        private DataTable exporttable;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();
        IOperationContract operationService = ServiceFactory.GetWcfService<IOperationContract>();
        public SysSjHcQuery()
        {
            InitializeComponent();
            dtpStartDate.SelectedDate = DateTime.Now.AddDays(-1);
            dtpEndDate.SelectedDate = DateTime.Now;

            user_flag_tier = PubClass.userInfo.FlagTier;
        }

        private void _query_Click(object sender, RoutedEventArgs e)
        {

            DataTable table = operationService.ExecuteProSjhc(PubClass.userInfo.ID,
                                                              ((DateTime) dtpStartDate.SelectedDate).ToString(
                                                                  "yyyy-MM-dd"),
                                                              ((DateTime) dtpEndDate.SelectedDate).ToString("yyyy-MM-dd"));
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
            DataTable table = operationService.ExecuteProSjhc(PubClass.userInfo.ID,
                                                            ((DateTime)dtpStartDate.SelectedDate).ToString(
                                                                "yyyy-MM-dd"),
                                                            ((DateTime)dtpEndDate.SelectedDate).ToString("yyyy-MM-dd"));
            lvlist.DataContext = table;
            exporttable = table;

            _title.Text = table.Rows.Count.ToString();
        }

        private void _new_Click(object sender, RoutedEventArgs e)
        {
            NewSjHc sjhc = new NewSjHc(this);

            sjhc.ShowDialog();
        }

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            if (exporttable == null)
            {
                return;
            }

            if (exporttable.Rows.Count == 0)
            {
                Toolkit.MessageBox.Show("导出内容为空，请确认！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.Filter = "导出文件 (*.csv)|*.csv";
            sfd.FilterIndex = 0;
            sfd.RestoreDirectory = true;
            sfd.Title = "导出文件保存路径";
            sfd.ShowDialog();
            string strFilePath = sfd.FileName;
            if (strFilePath != "")
            {
                if (File.Exists(strFilePath))
                {
                    File.Delete(strFilePath);
                }
                StreamWriter sw = new StreamWriter(new FileStream(strFilePath, FileMode.CreateNew), Encoding.Default);
                string tableHeader = "试剂代码" + "," + "试剂名称" + "," + "数量" + "," + "采购时间";
                //sw.WriteLine("");
                sw.WriteLine(tableHeader);

                for (int j = 0; j < exporttable.Rows.Count; j++)
                {
                    DataRow row = exporttable.Rows[j];
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < exporttable.Columns.Count; i++)
                    {
                        sb.Append(row[i]);
                        sb.Append(",");
                    }
                    sw.WriteLine(sb);
                }
                sw.Close();
                Toolkit.MessageBox.Show("文件导出成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
