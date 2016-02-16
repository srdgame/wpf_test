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

namespace WpfTest
{
    internal class PropertyNodeItem
    {
        public string Icon { get; set; }
        public string AddIcon { get; set; }
        public string EditIcon { get; set; }
        public string DeleteIcon { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public List<PropertyNodeItem> Children { get; set; }
        public PropertyNodeItem()
        {
            Children = new List<PropertyNodeItem>();
        }
    }
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string ADD_ICON = "add.png";
        public string EDITABLE_ICON = "edit.png";
        public string DELETE_ICON = "delete.png";
        public string FOLDER_ICON = "folder.png";
        public string TAG_ICON = "tag.png";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<PropertyNodeItem> itemList = new List<PropertyNodeItem>();
            PropertyNodeItem node1 = new PropertyNodeItem()
            {
                DisplayName = "Node No.1",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = FOLDER_ICON,
            };

            PropertyNodeItem node1tag1 = new PropertyNodeItem()
            {
                DisplayName = "Tag No.1",
                Name = "This is the discription of Tag 1. This is a tag.",
                Icon = TAG_ICON,
                EditIcon = EDITABLE_ICON,
                DeleteIcon = DELETE_ICON
            };
            node1.Children.Add(node1tag1);

            PropertyNodeItem node1tag2 = new PropertyNodeItem()
            {
                DisplayName = "Tag No.2",
                Name = "This is the discription of Tag 2. This is a tag.",
                Icon = TAG_ICON,
                EditIcon = EDITABLE_ICON,
                AddIcon = ADD_ICON
            };
            node1.Children.Add(node1tag2);

            itemList.Add(node1);
            PropertyNodeItem node2 = new PropertyNodeItem()
            {
                DisplayName = "Node No.2",
                Name = "This is the discription of Node 2. This is a folder.",
                Icon = FOLDER_ICON,
            };

            PropertyNodeItem node2tag3 = new PropertyNodeItem()
            {
                DisplayName = "Tag No.3",
                Name = "This is the discription of Tag 3. This is a tag.",
                Icon = TAG_ICON,
                EditIcon = EDITABLE_ICON
            };

            node2.Children.Add(node2tag3);
            PropertyNodeItem node2tag4 = new PropertyNodeItem()
            {
                DisplayName = "Tag No.4",
                Name = "This is the discription of Tag 4. This is a tag.",
                Icon = TAG_ICON,
                EditIcon = EDITABLE_ICON
            };

            node2.Children.Add(node2tag4);
            itemList.Add(node2);

            this.treeView.ItemsSource = itemList;
        }
    }
}
