using System;
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

namespace wpf_test.ctrls
{
    /// <summary>
    /// MyTreeView.xaml 的交互逻辑
    /// </summary>
    public partial class MyTreeView : UserControl
    {
        public MyTreeView()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent ClickAddEvent = 
            EventManager.RegisterRoutedEvent(
            "ClickAdd", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(MyTreeView));

        public event RoutedEventHandler ClickAdd
        {
            add { AddHandler(MyTreeView.ClickAddEvent, value, false); }
            remove { RemoveHandler(MyTreeView.ClickAddEvent, value); }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            e.RoutedEvent = ClickAddEvent;
            e.Source = this;
            this.RaiseEvent(e);
        }

        public static readonly RoutedEvent ClickEditEvent =
            EventManager.RegisterRoutedEvent(
            "ClickEdit", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(MyTreeView));

        public event RoutedEventHandler ClickEdit
        {
            add { AddHandler(MyTreeView.ClickEditEvent, value, false); }
            remove { RemoveHandler(MyTreeView.ClickEditEvent, value); }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            e.RoutedEvent = ClickEditEvent;
            e.Source = this;
            this.RaiseEvent(e);
        }

        public static readonly RoutedEvent ClickDeleteEvent = 
            EventManager.RegisterRoutedEvent(
            "ClickDelete", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(MyTreeView));

        public event RoutedEventHandler ClickDelete
        {
            add { AddHandler(MyTreeView.ClickDeleteEvent, value, false); }
            remove { RemoveHandler(MyTreeView.ClickDeleteEvent, value); }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            e.RoutedEvent = ClickDeleteEvent;
            e.Source = this;
            this.RaiseEvent(e);
        }

        public static readonly RoutedEvent SelectedItemChangedEvent =
            EventManager.RegisterRoutedEvent(
            "SelectedItemChanged", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(MyTreeView));

        public event RoutedEventHandler SelectedItemChanged
        {
            add { AddHandler(MyTreeView.SelectedItemChangedEvent, value, false); }
            remove { RemoveHandler(MyTreeView.SelectedItemChangedEvent, value); }
        }
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            e.RoutedEvent = SelectedItemChangedEvent;
            e.Source = this;
            this.RaiseEvent(e);
        }

        public System.Collections.IEnumerable ItemsSource
        {
            get { return treeView.ItemsSource; }
            set { treeView.ItemsSource = value; }
        }
        public object SelectedItem { get { return treeView.SelectedItem; } }
        public object SelectedValue { get { return treeView.SelectedValue; } }
        public string SelectedValuePath { get { return treeView.SelectedValuePath; } set { treeView.SelectedValuePath = value; } }

    }
}
