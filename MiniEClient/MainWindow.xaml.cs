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
using minie.irpc;

namespace MiniEClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public BackendServicePrx Client { get; set; }
        public sys_user_rpc UserInfo { get; set; }
        public MainWindow()
        {
            Client = (Application.Current as App).Client.Proxy;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                UserInfo = Client.get_current_user_info();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void CreateUser()
        {
            var dlg = new diags.UserCreateDlg();
            dlg.Owner = this;
            dlg.ShowDialog();
        }

        internal void ActiveDist()
        {
            this.frame_main.Navigate(new frames.UnitMgr());
        }

        internal void ActiveUsers()
        {
            this.frame_main.Navigate(new frames.UsersPage());
        }

        internal void ActiveHelp()
        {
            this.frame_main.Navigate(new frames.TestPage());
        }

        internal void ActiveAds()
        {
            this.frame_main.Navigate(new frames.AdMgr());
        }

    }
}
