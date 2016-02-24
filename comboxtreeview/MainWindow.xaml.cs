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

namespace comboxtreeview
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var list = new ObservableCollection<NodeItem>();
            var item = new NodeItem() { DisplayName = "TestItem" };
            var subitem = new NodeItem() { DisplayName = "SubItem222222222" };
            subitem.Children.Add(new NodeItem() { DisplayName = "Sub2Item222222222222222222" });
            subitem.Children.Add(new NodeItem() { DisplayName = "Sub2Item22222222222" });
            item.Children.Add(subitem);
            item.Children.Add(subitem);
            list.Add(item);
            var item2 = new NodeItem() { DisplayName = "2TestItem" };
            var subitem2 = new NodeItem() { DisplayName = "2SubItem222222222" };
            subitem2.Children.Add(new NodeItem() { DisplayName = "2Sub2Item222222222222222222" });
            subitem2.Children.Add(new NodeItem() { DisplayName = "2Sub2Item22222222222" });
            item2.Children.Add(subitem2);
            item2.Children.Add(subitem2);
            list.Add(item2);

            comboBox.Items.Clear();
            comboBox.ItemsSource = list;

            comboBox.DataContext = list;
            comboBox1.DataContext = list;
            comboBox2.DataContext = list;
            (comboBox2.Items[0] as ComboBoxItem).DataContext = subitem2;

            nodeList.DataContext = list;
            cboGroup.DataContext = list;

            comboTree.DataContext = list;
            comboTree.SelectedValue = subitem2;
        }

        private void lftTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var cbItemDisplay = this.comboBox2.Items[0] as ComboBoxItem;
            cbItemDisplay.DataContext = e.NewValue as NodeItem;
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBox2.SelectedItem = comboBox2.Items[0];
        }

        private void comboTree_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = comboTree.SelectedValue as NodeItem;
        }
    }
}
