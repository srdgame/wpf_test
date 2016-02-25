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
        public NodeType type { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }

        public override NodeType Type { get { return type; } }

        public override string Id { get { return id; } }
        public override string DisplayName { get { return name; } }
        public override string Tips { get { return desc; } }
        public TestData(TestData parent = null) : base(parent)
        {

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
            var v = treeView.SelectedItem as PropertyNodeItem;
            var page = new frames.EditorPage()
            {
                Editor = new TestEditor() { DataContext = v.Data.Clone() },
            };
            page.Save += OnSave;
            frame.Navigate(page);
            if (v.IsNew)
            {
                page.IsEditable = true;
            }
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            var page = frame.Content as EditorPage;
            var item = treeView.SelectedItem as PropertyNodeItem;
            var data = page.EditorData as TestData;
            item.Data = data;
            item.IsNew = false;
        }

        private void treeView_ClickAdd(object sender, PNRoutedEventArgs e)
        {
            var item = e.SourceItem as PropertyNodeItem;
            var data = e.SourceData as TestData;
            var new_item = new TestData()
            {
                type = NodeType.LEAF,
                id = Guid.NewGuid().ToString(),
                name = "New Tag",
                desc = "",
            };
            new_item.Item.IsNew = true;
            data.Children.Add(new_item);
            new_item.Item.IsSelected = true;
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
            var itemList = new NodeDataList();

            var node1 = new TestData()
            {
                type = NodeType.BOLE,
                id = Guid.NewGuid().ToString(),
                name = "Node No.1",
                desc = "This is the discription of Node1. This is a folder.",
            };

            var node1tag1 = new TestData(node1)
            {
                type = NodeType.LEAF,
                id = Guid.NewGuid().ToString(),
                name = "Tag No.1",
                desc = "This is the discription of Tag 1. This is a tag.",
            };

            var node1tag2 = new TestData(node1)
            {
                type = NodeType.LEAF,
                id = Guid.NewGuid().ToString(),
                name = "Tag No.2",
                desc = "This is the discription of Tag 2. This is a tag.",
            };

            itemList.Add(node1);

            var node2 =  new TestData()
                {
                    type = NodeType.BOLE,
                    id = Guid.NewGuid().ToString(),
                    name = "Node No.2",
                    desc = "This is the discription of Node 2. This is a folder.",
            };

            var node2tag3 = new TestData(node2)
                {
                    type = NodeType.LEAF,
                    id = Guid.NewGuid().ToString(),
                    name = "Tag No.3",
                    desc = "This is the discription of Tag 3. This is a tag.",
            };

            itemList.Add(node2);

            this.treeView.ItemsSource = itemList;
        }
    }
}
