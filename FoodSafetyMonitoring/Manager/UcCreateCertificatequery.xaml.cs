﻿using System;
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
using FoodSafety.Model;
using FoodSafetyMonitoring.dao;
using System.Data;
using FoodSafetyMonitoring.Common;
using Toolkit = Microsoft.Windows.Controls;
using FoodSafetyMonitoring.Manager.UserControls;
using System.IO;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// UcCreateCertificatequery.xaml 的交互逻辑
    /// </summary>
    public partial class UcCreateCertificatequery : UserControl
    {
        public IDBOperation dbOperation = null;
        private DataTable current_table;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();

        string userId = PubClass.userInfo.ID;
        string deptId = PubClass.userInfo.DepartmentID;


        public UcCreateCertificatequery(IDBOperation dbOperation)
        {
            InitializeComponent();
            this.dbOperation = dbOperation;

            dtpStartDate.SelectedDate = DateTime.Now.AddDays(-1);
            dtpEndDate.SelectedDate = DateTime.Now;

            //ComboboxTool.InitComboboxSource(_source_company, string.Format("select DISTINCT t_certificate.companyid ,t_shipper.shippername" +
            //                                 " FROM t_certificate left join t_shipper ON t_certificate.companyid = t_shipper.shipperid" +
            //                                 " WHERE t_certificate.createdeptid like '{0}%' ", deptId), "cxtj");
        }


        private void _query_Click(object sender, RoutedEventArgs e)
        {
            //清空列表
            lvlist.DataContext = null;

            DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call p_query_certificate_new_new({0},'{1}','{2}','{3}','{4}','{5}')",
                   PubClass.userInfo.ID,
                   ((DateTime)dtpStartDate.SelectedDate).ToShortDateString(),
                   ((DateTime)dtpEndDate.SelectedDate).ToShortDateString(),
                   _card_no.Text,
                   _source_company.Text,
                   _source_name.Text)).Tables[0];

            current_table = table;
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

        //private void _btn_details_Click(object sender, RoutedEventArgs e)
        //{
        //    string batch_no = (sender as Button).Tag.ToString();

        //    grid_info.Children.Add(new UcCreateCertificatedetails(dbOperation, batch_no));
        //}

        private void _btn_card_Click(object sender, RoutedEventArgs e)
        {
            string card_id = (sender as Button).Tag.ToString();
            CertificatePreview cer = new CertificatePreview(dbOperation, card_id);
            cer.ShowDialog();
        }

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            if (current_table == null)
            {
                return;
            }

            if (current_table.Rows.Count == 0)
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
                string tableHeader = "检疫证号"+ ","+"出证时间"+","+"检疫分站"+","+"检疫员"+","+"货主";
                //sw.WriteLine("");
                sw.WriteLine(tableHeader);

                for (int j = 0; j < current_table.Rows.Count; j++)
                {
                    DataRow row = current_table.Rows[j];
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < current_table.Columns.Count; i++)
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