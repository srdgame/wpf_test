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
    public class test_data_rpc : ICloneable
    {
        public PNItemType type { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public test_data_rpc parent { get; set; }
        public List<test_data_rpc> children;
        public test_data_rpc(test_data_rpc parent = null)
        {
            this.parent = parent;
            children = new List<test_data_rpc>();
            if (this.parent != null)
                parent.children.Add(this);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class TestData : PNTreeViewItem
    {
        private test_data_rpc _data;
        public override object Data { get { return _data; } }
        public override PNItemType Type { get { return _data.type; } }

        public override string Id { get { return _data.id; } }
        public override string DisplayName { get { return _data.name; } }
        public override string Tips { get { return _data.desc; } }

        public override string Editor { get { return "TestEditor"; } }

        public TestData(test_data_rpc data, TestData parent = null) : base(parent)
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
            var d = data as test_data_rpc;
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
        PNTreeViewItemList _item_list;
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = treeView.SelectedItem as TestData;
            var editor = item.CreateEditor(item.CloneData());
            (editor as TestEditor).NodeList = _item_list;

            var page = new frames.EditorPage()
            {
                Editor = editor,
                /*
                Editor = new TestEditor()
                {
                    DataContext = item.CloneData(),
                    NodeList = _item_list,
                    //SelectedNode = item.Parent,
                },
                */
            };
            page.Save += OnSave;
            frame.Navigate(page);
            if (item.IsNew)
            {
                page.IsEditable = true;
            }
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            var page = frame.Content as EditorPage;
            var item = treeView.SelectedItem as PNTreeViewItem;
            item.UpdateData(page.EditorData);
            item.UpdateGUI();
            item.Parent = (page.Editor as TestEditor).SelectedNode as TestData;
            item.IsNew = false;

            var item_p = item.Parent;
            while (item_p != null)
            {
                item_p.IsExpanded = true;
                item_p = item_p.Parent;
            }
        }

        private void treeView_ClickAdd(object sender, PNRoutedEventArgs e)
        {
            var item = e.SourceItem as TestData;
            var new_item = new TestData(new test_data_rpc()
            {
                type = PNItemType.LEAF,
                id = Guid.NewGuid().ToString(),
                name = "New Tag",
                desc = "",
                parent = item.Data as test_data_rpc,
            }, item);
            new_item.IsNew = true;
            new_item.IsSelected = true;
        }
        private void treeView_ClickEdit(object sender, PNRoutedEventArgs e)
        {
            var page = frame.Content as EditorPage;
            page.IsEditable = true;
        }

        private void treeView_ClickDelete(object sender, PNRoutedEventArgs e)
        {
            var item = e.SourceItem as PNTreeViewItem;
            var parent = item.Parent;
            if (parent != null)
            {
                parent.Children.Remove(item);
            }
            else
            {
                _item_list.Remove(item);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var itemList = new PNTreeViewItemList();

            var node1 = new test_data_rpc()
            {
                type = PNItemType.BOLE,
                id = Guid.NewGuid().ToString(),
                name = "Node No.1",
                desc = "This is the discription of Node1. This is a folder.",
            };

            var node1tag1 = new test_data_rpc(node1)
            {
                type = PNItemType.LEAF,
                id = Guid.NewGuid().ToString(),
                name = "Tag No.1",
                desc = "This is the discription of Tag 1. This is a tag.",
            };

            var node1tag2 = new test_data_rpc(node1)
            {
                type = PNItemType.LEAF,
                id = Guid.NewGuid().ToString(),
                name = "Tag No.2",
                desc = "This is the discription of Tag 2. This is a tag.",
            };

            new test_data_rpc(node1tag2)
            {
                type = PNItemType.BOLE,
                id = Guid.NewGuid().ToString(),
                name = "DDD",
                desc = "",
            };

            itemList.Add(new TestData(node1));

            var node2 =  new test_data_rpc()
                {
                    type = PNItemType.BOLE,
                    id = Guid.NewGuid().ToString(),
                    name = "Node No.2",
                    desc = "This is the discription of Node 2. This is a folder.",
            };

            var node2tag3 = new test_data_rpc(node2)
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
