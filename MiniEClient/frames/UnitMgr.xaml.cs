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
using MiniEClient.ctrls;
using MiniEClient.data;

namespace MiniEClient.frames
{
    /// <summary>
    /// DistMgr.xaml 的交互逻辑
    /// </summary>
    public partial class UnitMgr : Page
    {
        private ObservableCollection<CMNodeCategory> _categories;
        private PNTreeViewItemList _item_list;
        private MainWindow m_Main;

        public UnitMgr(MainWindow main)
        {
            m_Main = main;
            InitializeComponent();
            _categories = new ObservableCollection<CMNodeCategory>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var itemList = new PNTreeViewItemList();
            var bj = new cm_node_rpc()
            {
                id = Guid.NewGuid().ToString(),
                name = "北京市",
                desc = "北京市行政区.",
                category = 900,
                address = "Address....."
            };

            var haidian = new cm_node_rpc(bj)
            {
                id = Guid.NewGuid().ToString(),
                name = "海淀区",
                desc = "海淀行政区.",
                category = 900,
                address = "Address....."
            };

            var zgceast = new cm_node_rpc(haidian)
            {
                id = Guid.NewGuid().ToString(),
                name = "中关村东路",
                desc = "中关村东路下辖小区.",
                category = 400,
                address = "Address....."
            };
            var zdhbuilding = new cm_node_rpc(zgceast)
            {
                id = Guid.NewGuid().ToString(),
                name = "自动化大厦",
                desc = "中关村东路95号.",
                category = 300,
                address = "Address....."
            };
            var f12 = new cm_node_rpc(zdhbuilding)
            {
                id = Guid.NewGuid().ToString(),
                name = "12F",
                desc = "12 floor",
                category = 200,
            };
            var u1214 = new cm_node_rpc(f12)
            {
                id = Guid.NewGuid().ToString(),
                name = "1214",
                desc = "1214 Romm",
                category = 100,
            };
            var u1216 = new cm_node_rpc(f12)
            {
                id = Guid.NewGuid().ToString(),
                name = "1216",
                desc = "1216 Romm",
                category = 100,
            };

            var cg = new cm_node_rpc(haidian)
            {
                id = Guid.NewGuid().ToString(),
                name = "翠宫",
                desc = "翠宫饭店.",
                category = 300,
                address = "cuigong....."
            };

            var chaoyang = new cm_node_rpc(bj)
            {
                id = Guid.NewGuid().ToString(),
                name = "朝阳区",
                desc = "北京市朝阳行政区.",
                category = 900,
                address = "Address....."
            };

            itemList.Add(new CMNode(bj));
            _item_list = itemList;

            this.treeView.ItemsSource = itemList;
            _categories.Add(new CMNodeCategory() { name = "住户", value = 100 });
            _categories.Add(new CMNodeCategory() { name = "单元", value = 200 });
            _categories.Add(new CMNodeCategory() { name = "楼房", value = 300 });
            _categories.Add(new CMNodeCategory() { name = "小区", value = 400 });
            _categories.Add(new CMNodeCategory() { name = "行政区域", value = 900 });
        }

        private void treeView_ClickAdd(object sender, PNRoutedEventArgs e)
        {
            var item = e.SourceItem as CMNode;
            var new_item = new CMNode(new cm_node_rpc()
            {
                id = Guid.NewGuid().ToString(),
                name = "New Node",
                desc = "",
                parent = item.Data as cm_node_rpc,
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

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var v = treeView.SelectedItem as CMNode;
            var page = new frames.EditorPage()
            {
                Editor = new CMNodeEditor()
                {
                    DataContext = v.CloneData(),
                    Categories = _categories,
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
            item.UpdateData(page.EditorData);
            item.UpdateGUI();
            item.Parent = (page.Editor as CMNodeEditor).nodeList.SelectedItem as CMNode;
            item.IsNew = false;
        }
    }
}
