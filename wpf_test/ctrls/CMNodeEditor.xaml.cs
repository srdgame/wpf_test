﻿using System;
using System.Collections;
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
    /// CMNodeEditor.xaml 的交互逻辑
    /// </summary>
    public partial class CMNodeEditor : UserControl
    {
        public ObservableCollection<CMNodeCategory> Categories
        {
            get
            {
                return categoryList.ItemsSource as ObservableCollection<CMNodeCategory>;
            }
            set
            {
                categoryList.ItemsSource = value;
            }
        }
        public IEnumerable<object> NodeList { set { nodeList.ItemsSource = value; }  }
        public CMNodeEditor()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           // categoryList.ItemsSource = Categories;
        }
    }
}
