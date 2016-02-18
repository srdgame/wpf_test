using System;
using System.Collections.Generic;
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
    public class PropertyNodeItem : ICloneable, INotifyPropertyChanged
    {
        public string Icon { get; set; }
        public string AddIcon { get; set; }
        public string EditIcon { get; set; }
        public string DeleteIcon { get; set; }
        public string DisplayName
        {
            get { return DisplayName; }
            set
            {
                DisplayName = value;
                if (PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("DisplayName"));
                }
            }
        }
        public string Tips { get; set; }
        public List<PropertyNodeItem> Children { get; set; }
        public PropertyNodeItem()
        {
            Children = new List<PropertyNodeItem>();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public event PropertyChangedEventHandler PropertyChanged;  
    }
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        List<PropertyNodeItem> _itemList;
        public MainWindow()
        {
            InitializeComponent();
        }

        internal void UpdateItems()
        {
            
        }

        public string ADD_ICON = "images/icon/add.png";
        public string EDITABLE_ICON = "images/icon/edit.png";
        public string DELETE_ICON = "images/icon/delete.png";
        public string FOLDER_ICON = "images/icon/folder.png";
        public string TAG_ICON = "images/icon/tag.png";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<PropertyNodeItem> itemList = new List<PropertyNodeItem>();

            PropertyNodeItem node1 = new PropertyNodeItem()
            {
                DisplayName = "Node No.1",
                Tips = "This is the discription of Node1. This is a folder.",
                Icon = FOLDER_ICON,
                AddIcon = ADD_ICON,
                EditIcon = EDITABLE_ICON,
                DeleteIcon = DELETE_ICON,
            };

            PropertyNodeItem node1tag1 = new PropertyNodeItem()
            {
                DisplayName = "Tag No.1",
                Tips = "This is the discription of Tag 1. This is a tag.",
                Icon = TAG_ICON,
                EditIcon = EDITABLE_ICON,
                DeleteIcon = DELETE_ICON,
            };
            node1.Children.Add(node1tag1);

            PropertyNodeItem node1tag2 = new PropertyNodeItem()
            {
                DisplayName = "Tag No.2",
                Tips = "This is the discription of Tag 2. This is a tag.",
                Icon = TAG_ICON,
                EditIcon = EDITABLE_ICON,
                DeleteIcon = DELETE_ICON,
            };
            node1.Children.Add(node1tag2);
            itemList.Add(node1);

            PropertyNodeItem node2 = new PropertyNodeItem()
            {
                DisplayName = "Node No.2",
                Tips = "This is the discription of Node 2. This is a folder.",
                Icon = FOLDER_ICON,
                AddIcon = ADD_ICON,
                EditIcon = EDITABLE_ICON,
                DeleteIcon = DELETE_ICON,
            };

            PropertyNodeItem node2tag3 = new PropertyNodeItem()
            {
                DisplayName = "Tag No.3",
                Tips = "This is the discription of Tag 3. This is a tag.",
                Icon = TAG_ICON,
                EditIcon = EDITABLE_ICON,
                DeleteIcon = DELETE_ICON,
            };

            node2.Children.Add(node2tag3);
            PropertyNodeItem node2tag4 = new PropertyNodeItem()
            {
                DisplayName = "Tag No.4",
                Tips = "This is the discription of Tag 4. This is a tag.",
                Icon = TAG_ICON,
                EditIcon = EDITABLE_ICON,
                DeleteIcon = DELETE_ICON,
            };

            node2.Children.Add(node2tag4);
            itemList.Add(node2);

            this.treeView.ItemsSource = itemList;
            _itemList = itemList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            PropertyNodeItem item = btn.Tag as PropertyNodeItem;
            if (item.Icon.Contains("folder.png"))
            {
                PropertyNodeItem new_item = new PropertyNodeItem()
                {
                    Icon = TAG_ICON,
                    DisplayName = "New Tag",
                    Tips = "",
                    EditIcon = EDITABLE_ICON,
                    DeleteIcon = DELETE_ICON,
                };
                item.Children.Add(new_item);
                itemshow.Bind(new_item);
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
            PropertyNodeItem item = btn.Tag as PropertyNodeItem;
            if (item.Icon.Contains("folder.png"))
            {
                this._itemList.Remove(item);
            }
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PropertyNodeItem item = treeView.SelectedValue as PropertyNodeItem;
            itemshow.Bind(item);
        }
    }
}
