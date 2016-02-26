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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace comboxtreeview
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
            comboBox.DataContext = DataContext;
        }

        public object SelectedValue
        {
            get { return (comboBox.Items[0] as ComboBoxItem).DataContext; }
            set { (comboBox.Items[0] as ComboBoxItem).DataContext = value; }
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
            var cbItemDisplay = this.comboBox.Items[0] as ComboBoxItem;
            cbItemDisplay.DataContext = e.NewValue as NodeItem;
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
