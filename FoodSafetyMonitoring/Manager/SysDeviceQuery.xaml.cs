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
using FoodSafety.Model;
using FoodSafetyMonitoring.dao;
using System.Windows.Forms.Integration;
using System.Data;
using FoodSafetyMonitoring.Common;
using FoodSafetyMonitoring.Manager.UserControls;
using Toolkit = Microsoft.Windows.Controls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysDeviceQuery.xaml 的交互逻辑
    /// </summary>
    public partial class SysDeviceQuery : UserControl
    {
        private IDBOperation dbOperation;
        private string user_flag_tier;
        private DataTable exporttable;
        private string dept_type;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();
        public SysDeviceQuery(IDBOperation dbOperation, string depttype)
        {
            InitializeComponent();

            this.dbOperation = dbOperation;
            this.dept_type = depttype;

            user_flag_tier = PubClass.userInfo.FlagTier;
        }

        private void _query_Click(object sender, RoutedEventArgs e)
        {
            string company_name = _device_no.Text;
            System.Data.DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call p_query_company('{0}','{1}','{2}','{3}','{4}','{5}')",
                                          PubClass.userInfo.ID,
                                          company_name)).Tables[0];


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

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            if (exporttable != null)
            {
                if (exporttable.Rows.Count == 0)
                {
                    Toolkit.MessageBox.Show("导出内容为空，请确认！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                //打开对话框
                System.Windows.Forms.SaveFileDialog saveFile = new System.Windows.Forms.SaveFileDialog();
                saveFile.Filter = "Text documents (.pdf)|*.pdf";
                saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                var pdfFilePath = saveFile.FileName;
                if (pdfFilePath != "")
                {
                    if (System.IO.File.Exists(pdfFilePath))
                    {
                        try
                        {
                            System.IO.File.Delete(pdfFilePath);
                        }
                        catch (Exception ex)
                        {
                            Toolkit.MessageBox.Show("导出文件时出错,文件可能正被打开！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                    }

                    try
                    {
                        Document document = new Document();
                        PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create));
                        // 添加文档内容
                        document.Open();

                        //设置中文是字体，否则，中文存不了
                        BaseFont bfHei = BaseFont.CreateFont(@"C:\Windows\Fonts\simfang.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                        iTextSharp.text.Font font = new iTextSharp.text.Font(bfHei, 10);

                        PdfPTable table = new PdfPTable(3);
                        table.AddCell(new Phrase("检测单位", font));
                        table.AddCell(new Phrase("来源单位", font));
                        table.AddCell(new Phrase("来源产地", font));
                        table.AddCell(new Phrase("负责人姓名", font));
                        table.AddCell(new Phrase("手机", font));
                        for (int i = 0; i < exporttable.Rows.Count; i++)
                        {
                            table.AddCell(new Phrase(exporttable.Rows[i][1].ToString(), font));
                            table.AddCell(new Phrase(exporttable.Rows[i][3].ToString(), font));
                            table.AddCell(new Phrase(exporttable.Rows[i][4].ToString(), font));
                            table.AddCell(new Phrase(exporttable.Rows[i][5].ToString(), font));
                            table.AddCell(new Phrase(exporttable.Rows[i][6].ToString(), font));
                        }

                        document.Add(table);
                        document.Close();
                        Toolkit.MessageBox.Show("文件导出成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    catch
                    {
                        Toolkit.MessageBox.Show("无法创建pdf对象！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                }
            }
        }

        private void _btn_modify_Click(object sender, RoutedEventArgs e)
        {
            string company_id = (sender as System.Windows.Controls.Button).Tag.ToString();
            NewOrModifyDevice company = new NewOrModifyDevice(dbOperation, company_id, this);
            company.ShowDialog();
        }

        public void refresh()
        {
            string company_name = _device_no.Text;
            System.Data.DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call p_query_company('{0}','{1}','{2}','{3}','{4}','{5}')",
                                          PubClass.userInfo.ID,
                                          company_name)).Tables[0];

            lvlist.DataContext = table;
            exporttable = table;

        }

        private void _new_Click(object sender, RoutedEventArgs e)
        {
            NewOrModifyDevice company = new NewOrModifyDevice(dbOperation, this);
            company.ShowDialog();
        }

        private void _btn_delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
