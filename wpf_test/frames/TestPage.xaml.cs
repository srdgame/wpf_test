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
    public class TestData : NodeData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
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
            var v = treeView.SelectedValue as ICloneable;
            var page = new frames.EditorPage()
            {
                Editor = new TestEditor() { DataContext = v.Clone() },
            };
            page.Save += OnSave;
            frame.Navigate(page);
            if ((treeView.SelectedItem as PropertyNodeItemBase).Type.HasFlag(PropertyNodeType.NOLOCK))
            {
                page.IsEditable = true;
            }
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            var page = frame.Content as EditorPage;
            var item = treeView.SelectedItem as PropertyNodeItem<TestData>;
            item.Data = page.EditorData as TestData;
            if (item.Type.HasFlag(PropertyNodeType.NOLOCK))
                item.Type ^= PropertyNodeType.NOLOCK;
        }

        private void treeView_ClickAdd(object sender, PNRoutedEventArgs e)
        {
            var item = e.SourceItem as PropertyNodeItem<TestData>;
            var new_item = new PropertyNodeItem<TestData>(PropertyNodeType.LEAF, new TestData()
            {
                id = Guid.NewGuid().ToString(),
                name = "New Tag",
                desc = "",
            });
            new_item.Type |= PropertyNodeType.NOLOCK;
            item.Children.Add(new_item);
            new_item.IsSelected = true;
        }
        private void treeView_ClickEdit(object sender, PNRoutedEventArgs e)
        {
            var page = frame.Content as EditorPage;
            page.IsEditable = true;
        }

        private void treeView_ClickDelete(object sender, PNRoutedEventArgs e)
        {
            MessageBox.Show("Delete");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var itemList = new ObservableCollection<PropertyNodeItem<TestData>>();

            var node1 = new PropertyNodeItem<TestData>(PropertyNodeType.BOLE, new TestData()
            {
                id = Guid.NewGuid().ToString(),
                name = "Node No.1",
                desc = "This is the discription of Node1. This is a folder.",
            });

            var node1tag1 = new PropertyNodeItem<TestData>(PropertyNodeType.LEAF, new TestData()
            {
                id = Guid.NewGuid().ToString(),
                name = "Tag No.1",
                desc = "This is the discription of Tag 1. This is a tag.",
            });
            node1.Children.Add(node1tag1);

            var node1tag2 = new PropertyNodeItem<TestData>(PropertyNodeType.LEAF, new TestData()
            {
                id = Guid.NewGuid().ToString(),
                name = "Tag No.2",
                desc = "This is the discription of Tag 2. This is a tag.",
            });
            node1.Children.Add(node1tag2);
            itemList.Add(node1);

            var node2 = new PropertyNodeItem<TestData>(PropertyNodeType.BOLE, new TestData()
            {
                id = Guid.NewGuid().ToString(),
                name = "Node No.2",
                desc = "This is the discription of Node 2. This is a folder.",
            });

            var node2tag3 = new PropertyNodeItem<TestData>(PropertyNodeType.LEAF, new TestData()
            {
                id = Guid.NewGuid().ToString(),
                name = "Tag No.3",
                desc = "This is the discription of Tag 3. This is a tag.",
            });

            node2.Children.Add(node2tag3);
            var node2tag4 = new PropertyNodeItem<TestData>(PropertyNodeType.LEAF, new TestData()
            {
                id = Guid.NewGuid().ToString(),
                name = "Tag No.4",
                desc = "This is the discription of Tag 4. This is a tag.",
            });

            node2.Children.Add(node2tag4);
            itemList.Add(node2);

            this.treeView.ItemsSource = itemList;
        }
    }
}
