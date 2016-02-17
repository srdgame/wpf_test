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

namespace wpf_test.frames
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class LeftMenu : Page
    {
        private MainWindow m_Main;
        public LeftMenu(MainWindow main)
        {
            m_Main = main;
            InitializeComponent();
        }

        private void btn_dist_Click(object sender, RoutedEventArgs e)
        {
            m_Main.ActiveDist();
        }

        private void btn_users_Click(object sender, RoutedEventArgs e)
        {
            m_Main.ActiveUsers();
        }

        private void btn_ads_Click(object sender, RoutedEventArgs e)
        {
            m_Main.ActiveAds();
        }
    }
}
