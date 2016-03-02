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

namespace MiniEClient.ctrls
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
        private void UpdateDisplayItem()
        {
            if (ItemsSource == null)
                return;
            if (SelectedValue == null && SelectedItem == null)
                return;

            var cbItemDisplay = comboBox.Items[0] as ComboBoxItem;
            if (SelectedValuePath != null && SelectedValuePath != string.Empty)
            {
                PropertyInfo pi = ItemsSource.FirstOrDefault().GetType().GetProperty(SelectedValuePath);
                PropertyInfo cpi = ItemsSource.FirstOrDefault().GetType().GetProperty(ChildrenPath);
                SelectedItem = FindItem(ItemsSource, pi, cpi, SelectedValue);
                //cbItemDisplay.DataContext = FindItem(ItemsSource, pi, cpi, SelectedValue);
            }
            else
            {
                PropertyInfo cpi = ItemsSource.FirstOrDefault().GetType().GetProperty(ChildrenPath);
                SelectedItem = FindItem(ItemsSource, null, cpi, SelectedValue);
                //cbItemDisplay.DataContext = FindItem(ItemsSource, null, cpi, SelectedValue);
            }
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
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnItemsSourceChanged)));

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = d as ComboTree;
            thisControl.UpdateDisplayItem();
        }

        public string ChildrenPath
        {
            get { return GetValue(ChildrenPathProperty) as string; }
            //set { SetValue(ChildrenPathProperty, value); }
        }
        public static readonly DependencyProperty ChildrenPathProperty =
            DependencyProperty.Register(
                "ChildrenPath",
                typeof(string),
                typeof(ComboTree),
                new FrameworkPropertyMetadata("Children", 
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnChildrenPathChanged)));

        private static void OnChildrenPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = d as ComboTree;
            thisControl.UpdateDisplayItem();
        }


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
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnSelectedValuePathChanged)));

        private static void OnSelectedValuePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = d as ComboTree;
            thisControl.UpdateDisplayItem();
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
                new FrameworkPropertyMetadata(null, 
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
                    new PropertyChangedCallback(OnSelectedValueChanged)));

        private static void OnSelectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = d as ComboTree;
            thisControl.UpdateDisplayItem();
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                "SelectedItem",
                typeof(object),
                typeof(ComboTree),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnSelectedItemChanged)));

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = d as ComboTree;
            var cbItemDisplay = thisControl.comboBox.Items[0] as ComboBoxItem;
            cbItemDisplay.DataContext = e.NewValue;
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
            cbItemDisplay.Tag = e.NewValue;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update the selected item in combobox, or you will not see the content for item0
            var cbItemDisplay = comboBox.Items[0] as ComboBoxItem;
            comboBox.SelectedItem = cbItemDisplay;

            // Update the Selected Item and Value
            SelectedItem = cbItemDisplay.Tag;
            if (SelectedItem != null)
            {
                SelectedValue = SelectedItem.GetType().GetProperty(this.SelectedValuePath).GetValue(SelectedItem);
            }

            e.RoutedEvent = SelectionChangedEvent;
            e.Source = this;
            RaiseEvent(e);
        }

    }
}
