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
            var subitem = new NodeItem() { DisplayName = "SubItem" };
            subitem.Children.Add(new NodeItem() { DisplayName = "Sub2Item" });
            subitem.Children.Add(new NodeItem() { DisplayName = "Sub2Item" });
            item.Children.Add(subitem);
            list.Add(item);

            comboBox.Items.Clear();
            comboBox.ItemsSource = list;
        }
    }
}
