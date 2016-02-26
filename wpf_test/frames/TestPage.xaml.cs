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
    public class rpc_test_data : ICloneable
    {
        public PNItemType type { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public rpc_test_data parent { get; set; }
        public List<rpc_test_data> children;
        public rpc_test_data(rpc_test_data parent = null)
        {
            this.parent = parent;
            children = new List<rpc_test_data>();
            if (this.parent != null)
                parent.children.Add(this);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class TestData : PNDataBase
    {
        private rpc_test_data _data;
        public rpc_test_data Data { get { return _data; } }
        public override PNItemType Type { get { return _data.type; } }

        public override string Id { get { return _data.id; } }
        public override string DisplayName { get { return _data.name; } }
        public override string Tips { get { return _data.desc; } }
        public TestData(rpc_test_data data, TestData parent = null) : base(parent)
        {
            _data = data;
            TestData tmp;
            foreach(var element in data.children)
            {
                tmp = new TestData(element, this);
            }
        }

        public override void UpdateData(object data)
        {
            var d = data as rpc_test_data;
            _data.name = d.name;
            _data.desc = d.desc;
            _data.parent = d.parent;
            _data.type = d.type;
        }

        public override object CloneData()
        {
            return _data.Clone();
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
        NodeDataList _item_list;
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var v = treeView.SelectedItem as PNTreeViewItem;
            var data = treeView.SelectedData as TestData;

            var page = new frames.EditorPage()
            {
                Editor = new TestEditor()
                {
                    DataContext = data.CloneData(),
                    NodeList = _item_list,
                },
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
            var item = treeView.SelectedItem as PNTreeViewItem;
            var data = treeView.SelectedData as TestData;
            data = page.EditorData as TestData;
            item.IsNew = false;
        }

        private void treeView_ClickAdd(object sender, PNRoutedEventArgs e)
        {
            var item = e.SourceItem as PNTreeViewItem;
            var data = e.SourceData as TestData;
            var new_item = new TestData(new rpc_test_data()
            {
                type = PNItemType.LEAF,
                id = Guid.NewGuid().ToString(),
                name = "New Tag",
                desc = "",
            }, data);
            new_item.Item.IsNew = true;
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

            var node1 = new rpc_test_data()
            {
                type = PNItemType.BOLE,
                id = Guid.NewGuid().ToString(),
                name = "Node No.1",
                desc = "This is the discription of Node1. This is a folder.",
            };

            var node1tag1 = new rpc_test_data(node1)
            {
                type = PNItemType.LEAF,
                id = Guid.NewGuid().ToString(),
                name = "Tag No.1",
                desc = "This is the discription of Tag 1. This is a tag.",
            };

            var node1tag2 = new rpc_test_data(node1)
            {
                type = PNItemType.LEAF,
                id = Guid.NewGuid().ToString(),
                name = "Tag No.2",
                desc = "This is the discription of Tag 2. This is a tag.",
            };

            new rpc_test_data(node1tag2)
            {
                type = PNItemType.BOLE,
                id = Guid.NewGuid().ToString(),
                name = "DDD",
                desc = "",
            };

            itemList.Add(new TestData(node1));

            var node2 =  new rpc_test_data()
                {
                    type = PNItemType.BOLE,
                    id = Guid.NewGuid().ToString(),
                    name = "Node No.2",
                    desc = "This is the discription of Node 2. This is a folder.",
            };

            var node2tag3 = new rpc_test_data(node2)
                {
                    type = PNItemType.LEAF,
                    id = Guid.NewGuid().ToString(),
                    name = "Tag No.3",
                    desc = "This is the discription of Tag 3. This is a tag.",
            };

            itemList.Add(new TestData(node2));

            this.treeView.ItemsSource = itemList;
            _item_list = itemList;
        }
    }
}
