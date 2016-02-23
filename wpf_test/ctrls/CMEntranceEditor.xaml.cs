﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using wpf_test.data;

namespace wpf_test.ctrls
{
    /// <summary>
    /// CMEntranceEditor.xaml 的交互逻辑
    /// </summary>
    public partial class CMEntranceEditor : UserControl
    {
        public ObservableCollection<CMNodeBase> NodeList { get; set; }
        public CMEntranceEditor()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            nodeList.ItemsSource = NodeList;
        }
    }
}
