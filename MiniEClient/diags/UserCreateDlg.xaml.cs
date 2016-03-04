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
using System.Windows.Shapes;
using minie.irpc;

namespace MiniEClient.diags
{
    /// <summary>
    /// UserCreateDlg.xaml 的交互逻辑
    /// </summary>
    public partial class UserCreateDlg : Window
    {
        public string _cellphone { get; set; }
        public string _password { get; set; }
        public int _code { get; set; }
        public cm_user_rpc _user { get; set; }
        public UserCreateDlg()
        {
            InitializeComponent();
        }

        private void btn_get_code_Click(object sender, RoutedEventArgs e)
        {
            // valid phone number
            try
            {
                if ((App.Current as App).Client.Proxy.register_app_user_step1(_cellphone))
                {
                    MessageBox.Show(this, "Valid Code has been sent to your cellphone via SMS", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(this, "Phone number provided is invalid", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btn_reg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_code == 0)
                {
                    MessageBox.Show(this, "Please input Varify Code!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                _user = (App.Current as App).Client.Proxy.register_app_user_step2(_cellphone, _password, _code);
                if (_user != null)
                {
                    editor.DataContext = _user;
                    editor.Visibility = Visibility.Visible;
                    step1.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show(this, "Register User failed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            step1.Visibility = Visibility.Visible;
            editor.Visibility = Visibility.Hidden;
        }
    }
}
