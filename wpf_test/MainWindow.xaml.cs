using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.frame_top.Navigate(new frames.TopMenu(this));
            this.frame_left.Navigate(new frames.LeftMenu(this));
            this.frame_main.Navigate(new frames.Welcome(this));
        }

        internal void ActiveDist()
        {
            this.frame_main.Navigate(new frames.UnitMgr(this));
        }

        internal void ActiveUsers()
        {
            this.frame_main.Navigate(new frames.UsersPage(this));
        }

        internal void ActiveHelp()
        {
            this.frame_main.Navigate(new frames.TestPage(this));
        }

        internal void ActiveAds()
        {
            this.frame_main.Navigate(new frames.AdMgr(this));
        }

    }
}
