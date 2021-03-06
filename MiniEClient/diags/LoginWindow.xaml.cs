﻿using System;
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

namespace MiniEClient.diags
{
    /// <summary>
    /// login.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public string _user { get; set; }
        public string _password { get; set; }
        public string _ip { get; set; }
        public int _port { get; set; }
        public LoginWindow()
        {
            _user = "002";
            _password = "123456";
            _ip = "172.16.1.2";
            _port = 55001;
            InitializeComponent();
        }

        private void button_login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = new minie.irpc.minie_backend_client(_ip, _port);
                if (client.login(_user, _password))
                {
                    this.DialogResult = true;
                    (Application.Current as App).Client = client;
                }
            }
            catch (Exception e1)
            {
                // FIXME: Show user friendly message instead of exception message. e.g. 无法连接服务器，请检查网络连接是否正常。
                Console.WriteLine("{0} Exception caught", e1);
                var msg = e1.Message;
                if (msg.Length == 0)
                    msg = e1.InnerException.Message;
                MessageBox.Show(msg, e1.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }


            //if (user=="admin" && passwd == "password")
            //{
            //    this.DialogResult = true;
            //}
        }
    }
}
