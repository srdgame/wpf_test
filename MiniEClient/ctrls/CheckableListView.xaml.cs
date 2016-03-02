﻿using System;
using System.Collections;
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
using wpf_test.data;

namespace wpf_test.ctrls
{
    /// <summary>
    /// CheckableListView.xaml 的交互逻辑
    /// </summary>
    public partial class CheckableListView : UserControl
    {
        public CheckableListView()
        {
            InitializeComponent();
        }
        private void UpdateListView()
        {
            if (ItemsSource == null || ItemsSelected == null)
                return;

            CheckableListViewItem item;
            foreach (object sitem in ItemsSelected)
            {
                foreach (object s in ItemsSource)
                {
                    item = s as CheckableListViewItem;
                    if (item.Equals(sitem))
                    {
                        item.IsSelected = true;
                    }
                }
            }
        }
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(CheckableListView), 
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnItemsSourceChanged)));

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = d as CheckableListView;
            thisControl.UpdateListView();
        }

        public IEnumerable ItemsSource
        {
            get { return GetValue(ItemsSourceProperty) as IEnumerable; }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSelectedProperty =
            DependencyProperty.Register("ItemsSelected", typeof(IList), typeof(CheckableListView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnSelectedItemsChanged)));

        public IList ItemsSelected
        {
            get { return GetValue(ItemsSelectedProperty) as IList; }
            set { SetValue(ItemsSelectedProperty, value); }
        }

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = d as CheckableListView;
            thisControl.UpdateListView();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemsSelected == null)
                return;

            foreach (object item in e.RemovedItems)
            {
                foreach (object obj in ItemsSelected)
                {
                    if ((item as CheckableListViewItem).Equals(obj))
                    {
                        ItemsSelected.Remove(obj);
                    }
                }
                //ItemsSelected.Remove(item);
            }

            foreach (object item in e.AddedItems)
            {
                ItemsSelected.Add((item as CheckableListViewItem).Value);
            }
        }
    }
}