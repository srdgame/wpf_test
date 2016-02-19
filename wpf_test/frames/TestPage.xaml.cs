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
using wpf_test.ctrls;
using wpf_test.data;

namespace wpf_test.frames
{
    /// <summary>
    /// TestPage.xaml 的交互逻辑
    /// </summary>
    public partial class TestPage : Page
    {
        public TestPage(MainWindow main)
        {
            InitializeComponent();
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            
        }
        
        private void treeView_ClickAdd(object sender, PNRoutedEventArgs e)
        {
            PropertyNodeItem item = e.SourceItem as PropertyNodeItem;
            PropertyNodeItem new_item = new PropertyNodeItem(PropertyNodeType.LEAF)
            {
                DisplayName = "New Tag",
                Tips = "",
            };
            new_item.IsSelected = true;
            item.Children.Add(new_item);

        }
        private void treeView_ClickEdit(object sender, PNRoutedEventArgs e)
        {
            MessageBox.Show("Edit");

        }

        private void treeView_ClickDelete(object sender, PNRoutedEventArgs e)
        {
            MessageBox.Show("Delete");

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<PropertyNodeItem> itemList = new ObservableCollection<PropertyNodeItem>();

            PropertyNodeItem node1 = new PropertyNodeItem(PropertyNodeType.BOLE)
            {
                DisplayName = "Node No.1",
                Tips = "This is the discription of Node1. This is a folder.",
            };

            PropertyNodeItem node1tag1 = new PropertyNodeItem(PropertyNodeType.LEAF)
            {
                DisplayName = "Tag No.1",
                Tips = "This is the discription of Tag 1. This is a tag.",
            };
            node1.Children.Add(node1tag1);

            PropertyNodeItem node1tag2 = new PropertyNodeItem(PropertyNodeType.LEAF)
            {
                DisplayName = "Tag No.2",
                Tips = "This is the discription of Tag 2. This is a tag.",
            };
            node1.Children.Add(node1tag2);
            itemList.Add(node1);

            PropertyNodeItem node2 = new PropertyNodeItem(PropertyNodeType.BOLE)
            {
                DisplayName = "Node No.2",
                Tips = "This is the discription of Node 2. This is a folder.",
            };

            PropertyNodeItem node2tag3 = new PropertyNodeItem(PropertyNodeType.LEAF)
            {
                DisplayName = "Tag No.3",
                Tips = "This is the discription of Tag 3. This is a tag.",
            };

            node2.Children.Add(node2tag3);
            PropertyNodeItem node2tag4 = new PropertyNodeItem(PropertyNodeType.LEAF)
            {
                DisplayName = "Tag No.4",
                Tips = "This is the discription of Tag 4. This is a tag.",
            };

            node2.Children.Add(node2tag4);
            itemList.Add(node2);

            this.treeView.ItemsSource = itemList;
        }
    }
}
