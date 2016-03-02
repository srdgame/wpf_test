using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace treeview
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<PropertyNodeData> _itemList;
        public MainWindow()
        {
            InitializeComponent();
        }

        internal void UpdateItems()
        {
            
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<PropertyNodeData> itemList = new ObservableCollection<PropertyNodeData>();

            PropertyNodeData node1 = new PropertyNodeData()
            {
                id = Guid.NewGuid().ToString(),
                name = "Node No.1",
                desc = "This is the discription of Node1. This is a folder.",
            };

            PropertyNodeData node1tag1 = new PropertyNodeData()
            {
                id = Guid.NewGuid().ToString(),
                name = "Tag No.1",
                desc = "This is the discription of Tag 1. This is a tag.",
            };
            node1.Children.Add(node1tag1);

            PropertyNodeData node1tag2 = new PropertyNodeData()
            {
                id = Guid.NewGuid().ToString(),
                name = "Tag No.2",
                desc = "This is the discription of Tag 2. This is a tag.",
            };
            node1.Children.Add(node1tag2);
            itemList.Add(node1);

            PropertyNodeData node2 = new PropertyNodeData()
            {
                id = Guid.NewGuid().ToString(),
                name = "Node No.2",
                desc = "This is the discription of Node 2. This is a folder.",
            };

            PropertyNodeData node2tag3 = new PropertyNodeData()
            {
                id = Guid.NewGuid().ToString(),
                name = "Tag No.3",
                desc = "This is the discription of Tag 3. This is a tag.",
            };

            node2.Children.Add(node2tag3);
            PropertyNodeData node2tag4 = new PropertyNodeData()
            {
                id = Guid.NewGuid().ToString(),
                name = "Tag No.4",
                desc = "This is the discription of Tag 4. This is a tag.",
            };

            node2.Children.Add(node2tag4);
            itemList.Add(node2);

            this.treeView.ItemsSource = itemList;
            _itemList = itemList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            PropertyNodeData item = btn.Tag as PropertyNodeData;
            //if (item.Icon.Contains("folder.png"))
            {
                PropertyNodeData new_item = new PropertyNodeData()
                {
                    id = Guid.NewGuid().ToString(),
                    name = "New Tag",
                    desc = "",
                };
                item.Children.Add(new_item);
                itemshow.Bind(new_item.Item);
                TreeViewItem treeitem = new TreeViewItem();
                treeitem.DataContext = new_item;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            PropertyNodeItem item = btn.Tag as PropertyNodeItem;
            MessageBox.Show(item.DisplayName);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            PropertyNodeData item = btn.Tag as PropertyNodeData;
            //if (item.Icon.Contains("folder.png"))
            {
                this._itemList.Remove(item);
            }
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PropertyNodeData item = treeView.SelectedItem as PropertyNodeData;
            itemshow.Bind(item.Item);
        }
    }
}
