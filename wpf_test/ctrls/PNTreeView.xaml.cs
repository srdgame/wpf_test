using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    public class PNRoutedEventArgs : RoutedEventArgs
    {
        public PNRoutedEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source) { }

        public object SourceItem { get; set; }
    }
    /// <summary>
    /// MyTreeView.xaml 的交互逻辑
    /// </summary>
    public partial class PNTreeView : UserControl
    {
        public PNTreeView()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent ClickAddEvent = 
            EventManager.RegisterRoutedEvent(
            "ClickAdd", RoutingStrategy.Bubble,
            typeof(EventHandler<PNRoutedEventArgs>),
            typeof(PNTreeView));

        public event RoutedEventHandler ClickAdd
        {
            add { AddHandler(PNTreeView.ClickAddEvent, value, false); }
            remove { RemoveHandler(PNTreeView.ClickAddEvent, value); }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            PNRoutedEventArgs pe = new PNRoutedEventArgs(ClickAddEvent, e.Source);
            Button btn = sender as Button;
            pe.SourceItem = btn.Tag != null ? btn.Tag : treeView.SelectedItem;

            // Expand the click item
            (pe.SourceItem as data.PropertyNodeItem).IsExpanded = true;

            RaiseEvent(pe);
        }

        public static readonly RoutedEvent ClickEditEvent =
            EventManager.RegisterRoutedEvent(
            "ClickEdit", RoutingStrategy.Bubble,
            typeof(EventHandler<PNRoutedEventArgs>),
            typeof(PNTreeView));

        public event RoutedEventHandler ClickEdit
        {
            add { AddHandler(PNTreeView.ClickEditEvent, value, false); }
            remove { RemoveHandler(PNTreeView.ClickEditEvent, value); }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            PNRoutedEventArgs pe = new PNRoutedEventArgs(ClickEditEvent, e.Source);
            Button btn = sender as Button;
            pe.SourceItem = btn.Tag != null ? btn.Tag : treeView.SelectedItem;
            RaiseEvent(pe);
        }

        public static readonly RoutedEvent ClickDeleteEvent = 
            EventManager.RegisterRoutedEvent(
            "ClickDelete", RoutingStrategy.Bubble,
            typeof(EventHandler<PNRoutedEventArgs>),
            typeof(PNTreeView));

        public event RoutedEventHandler ClickDelete
        {
            add { AddHandler(PNTreeView.ClickDeleteEvent, value, false); }
            remove { RemoveHandler(PNTreeView.ClickDeleteEvent, value); }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            PNRoutedEventArgs pe = new PNRoutedEventArgs(ClickDeleteEvent, e.Source);
            Button btn = sender as Button;
            pe.SourceItem = btn.Tag != null ? btn.Tag : treeView.SelectedItem;
            RaiseEvent(pe);
        }

        public static readonly RoutedEvent SelectedItemChangedEvent =
            EventManager.RegisterRoutedEvent(
            "SelectedItemChanged", RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<object>),
            typeof(PNTreeView));

        public event RoutedEventHandler SelectedItemChanged
        {
            add { AddHandler(PNTreeView.SelectedItemChangedEvent, value, false); }
            remove { RemoveHandler(PNTreeView.SelectedItemChangedEvent, value); }
        }
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PropertyNodeItem item = treeView.SelectedItem as PropertyNodeItem;
            if (item != null)
            {
                btn_Add.IsEnabled = item.Type != PropertyNodeType.LEAF;
                btn_Delete.IsEnabled = true;
                btn_Edit.IsEnabled = true;
            }
            else
            {
                btn_Add.IsEnabled = false;
                btn_Delete.IsEnabled = false;
                btn_Edit.IsEnabled = false;
            }

            e.RoutedEvent = SelectedItemChangedEvent;
            RaiseEvent(e);
        }

        public System.Collections.IEnumerable ItemsSource
        {
            get { return treeView.ItemsSource; }
            set { treeView.ItemsSource = value; }
        }
        public object SelectedItem { get { return treeView.SelectedItem; } }
        public object SelectedValue { get { return treeView.SelectedValue; } }
        public string SelectedValuePath { get { return treeView.SelectedValuePath; } set { treeView.SelectedValuePath = value; } }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            btn_Add.IsEnabled = false;
            btn_Delete.IsEnabled = false;
            btn_Edit.IsEnabled = false;
        }
    }
}
