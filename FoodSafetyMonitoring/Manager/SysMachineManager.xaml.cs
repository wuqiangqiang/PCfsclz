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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysMachineManager.xaml 的交互逻辑
    /// </summary>
    public partial class SysMachineManager : UserControl
    {
        private DataTable exporttable;

        private string dept_type;
        IOperationContract operationContract = ServiceFactory.GetWcfService<IOperationContract>();

        public SysMachineManager(string depttype)
        {
            InitializeComponent();
            this.dept_type = depttype;

        }

        private void _query_Click(object sender, RoutedEventArgs e)
        {
            string device_id = _device_id.Text;
            System.Data.DataTable table = operationContract.ExecuteProQueryDevice(PubClass.userInfo.ID, _device_id.Text,
                                                                                  dept_type);

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
                        table.AddCell(new Phrase("设备号", font));
                        table.AddCell(new Phrase("仪器名", font));
                        table.AddCell(new Phrase("型号", font));
                        table.AddCell(new Phrase("经度", font));
                        table.AddCell(new Phrase("纬度", font));
                        table.AddCell(new Phrase("地址", font));
                        table.AddCell(new Phrase("时间", font));
                        for (int i = 0; i < exporttable.Rows.Count; i++)
                        {
                            table.AddCell(new Phrase(exporttable.Rows[i][0].ToString(), font));
                            table.AddCell(new Phrase(exporttable.Rows[i][1].ToString(), font));
                            table.AddCell(new Phrase(exporttable.Rows[i][2].ToString(), font));
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
    }
}