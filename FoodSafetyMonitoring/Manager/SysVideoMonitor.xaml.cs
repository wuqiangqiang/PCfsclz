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
using System.IO;
using System.Diagnostics;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysVideoMonitor.xaml 的交互逻辑
    /// </summary>
    public partial class SysVideoMonitor : UserControl
    {
        public SysVideoMonitor()
        {
            InitializeComponent();

            List<devices> device = new List<devices>();

            //读取数据
            List<string> txt = new List<string>();
            StreamReader sr = new StreamReader(System.IO.Path.Combine(Environment.CurrentDirectory, @"1.txt"), true);
            string strline = sr.ReadLine();
            while (strline.Length != 0)
            {
                txt.Add(strline);
                strline = sr.ReadLine();
            }
            //将数据存到device中
            for (int i = 0; i < txt.Count / 4; i++ )
            {
                device.Add(new devices { deviceno = txt[i*4 + 1].ToString(), sname = txt[i*4 + 2].ToString(), sage = txt[i*4 + 3].ToString() });
            }
            //赋值给listbox1
            listbox1.Items.Clear();
            listbox1.ItemsSource = device; 
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string deviceno = (sender as TextBlock).Tag.ToString();
            //ProcessStartInfo info = new ProcessStartInfo("AutoUpdate.exe", deviceno);
            //Process.Start(info);
        }
    }

    class devices
    {
        public string deviceno { get; set; }
        public string sname { get; set; }
        public string sage { get; set; }
    }  

}
