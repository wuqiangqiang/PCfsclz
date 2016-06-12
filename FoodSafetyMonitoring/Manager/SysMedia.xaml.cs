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

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysMedia.xaml 的交互逻辑
    /// </summary>
    public partial class SysMedia : UserControl
    {
        public SysMedia()
        {
            InitializeComponent();

        }

        private void _Img1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mediaElement1.Source = new Uri(@"D:\video\a.avi");
            mediaElement1.Play();
        }

        private void _Img2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mediaElement1.Source = new Uri(@"D:\video\b.avi");
            mediaElement1.Play();
        }
        private void _Img3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mediaElement1.Source = new Uri(@"D:\video\c.avi");
            mediaElement1.Play();
        }
        private void _Img4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mediaElement1.Source = new Uri(@"D:\video\d.avi");
            mediaElement1.Play();
        }
        private void _Img5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mediaElement1.Source = new Uri(@"D:\video\e.avi");
            mediaElement1.Play();
        }
        private void _Img6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mediaElement1.Source = new Uri(@"D:\video\f.avi");
            mediaElement1.Play();
        }
        private void _Img7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mediaElement1.Source = new Uri(@"D:\video\g.avi");
            mediaElement1.Play();
        }
        private void _Img8_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mediaElement1.Source = new Uri(@"D:\video\h.avi");
            mediaElement1.Play();
        }
    }
}
