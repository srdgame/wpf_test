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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_test.ctrls
{
    /// <summary>
    /// TestEditor.xaml 的交互逻辑
    /// </summary>
    public partial class TestEditor : UserControl
    {
        //public IEnumerable<object> NodeList { set { nodeList.ItemsSource = value; }  }
        public static readonly DependencyProperty NodeListProperty =
            DependencyProperty.Register("NodeList", typeof(IEnumerable<object>), typeof(TestEditor), new FrameworkPropertyMetadata(null));

        public IEnumerable<object> NodeList
        {
            get { return (IEnumerable<object>)GetValue(NodeListProperty); }
            set { SetValue(NodeListProperty, value); }
        }
        public TestEditor()
        {
            InitializeComponent();
        }
    }
}
