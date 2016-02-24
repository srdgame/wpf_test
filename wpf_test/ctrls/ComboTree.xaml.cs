using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
    /// ComboTree.xaml 的交互逻辑
    /// </summary>
    public partial class ComboTree : UserControl
    {
        public ComboTree()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }
        public IEnumerable<object> ItemsSource
        {
            get { return GetValue(ItemsSourceProperty) as IEnumerable<object>; }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IEnumerable<object>),
                typeof(ComboTree),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnItemsSourceChanged)));

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = d as ComboTree;
            thisControl.comboBox.DataContext = e.NewValue;
        }
        private string _children_path;
        public string ChildrenPath
        {
            get { return GetValue(ChildrenPathProperty) as string; }
            set { SetValue(ChildrenPathProperty, value); }
        }
        public static readonly DependencyProperty ChildrenPathProperty =
            DependencyProperty.Register(
                "ChildrenPath",
                typeof(string),
                typeof(ComboTree),
                new FrameworkPropertyMetadata("Children", new PropertyChangedCallback(OnChildrenPathChanged)));

        private static void OnChildrenPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = d as ComboTree;
            thisControl._children_path = e.NewValue as string;
        }

        private string _select_value_path;
        public string SelectedValuePath
        {
            get { return GetValue(SelectedValuePathProperty) as string; }
            set { SetValue(SelectedValuePathProperty, value); }
        }
        public static readonly DependencyProperty SelectedValuePathProperty =
            DependencyProperty.Register(
                "SelectedValuePath",
                typeof(string),
                typeof(ComboTree),
                new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnSelectedValuePathChanged)));

        private static void OnSelectedValuePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = d as ComboTree;
            thisControl._select_value_path = e.NewValue as string;
        }

        public object SelectedValue
        {
            get { return GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register(
                "SelectedValue",
                typeof(object),
                typeof(ComboTree),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnSelectedValueChanged)));

        private static object FindItem(IEnumerable items, PropertyInfo pi, PropertyInfo cpi, object value)
        {
            object item = null;
            foreach (var i in items)
            {
                item = pi != null ? pi.GetValue(i) : i;
                if (item.Equals(value))
                    return i;

                item = FindItem(cpi.GetValue(i) as IEnumerable, pi, cpi, value);
                if (item != null)
                    return item;
            }
            return null;
        }

        private static void OnSelectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = d as ComboTree;
            var item = thisControl.comboBox.Items[0] as ComboBoxItem;
            if (thisControl.SelectedValuePath != null && thisControl.SelectedValuePath != string.Empty)
            {
                PropertyInfo pi = thisControl.ItemsSource.FirstOrDefault().GetType().GetProperty(thisControl.SelectedValuePath);
                PropertyInfo cpi = thisControl.ItemsSource.FirstOrDefault().GetType().GetProperty(thisControl.ChildrenPath);
                item.DataContext = FindItem(thisControl.ItemsSource, pi, cpi, e.NewValue);
            }
            else
            {
                PropertyInfo cpi = thisControl.ItemsSource.FirstOrDefault().GetType().GetProperty(thisControl.ChildrenPath);
                item.DataContext = FindItem(thisControl.ItemsSource, null, cpi, e.NewValue);
            }
        }

        public static readonly RoutedEvent SelectionChangedEvent =
            EventManager.RegisterRoutedEvent(
            "SelectionChanged", RoutingStrategy.Bubble,
            typeof(SelectionChangedEventHandler),
            typeof(ComboTree));

        public event SelectionChangedEventHandler SelectionChanged
        {
            add { AddHandler(ComboTree.SelectionChangedEvent, value, false); }
            remove { RemoveHandler(ComboTree.SelectionChangedEvent, value); }
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var cbItemDisplay = comboBox.Items[0] as ComboBoxItem;
            cbItemDisplay.DataContext = e.NewValue;
            SelectedValue = cbItemDisplay.DataContext;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBox.SelectedItem = comboBox.Items[0];
            e.RoutedEvent = SelectionChangedEvent;
            e.Source = this;
            RaiseEvent(e);
        }

    }
}
