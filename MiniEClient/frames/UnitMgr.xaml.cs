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
using minie.irpc;

namespace MiniEClient.frames
{
    /// <summary>
    /// DistMgr.xaml 的交互逻辑
    /// </summary>
    public partial class UnitMgr : Page
    {
        private ObservableCollection<object> _categories;
        private PNTreeViewItemList _item_list;
        private MainWindow m_Main;

        public UnitMgr()
        {
            m_Main = App.Current.MainWindow as MainWindow;
            InitializeComponent();
            _categories = new ObservableCollection<object>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var itemList = new PNTreeViewItemList();
            try
            {
                var list = m_Main.Client.get_nodes();
                foreach(cm_node_rpc node in list)
                {
                    itemList.Add(new CMNode(node));

                }
                _item_list = itemList;

                this.treeView.ItemsSource = itemList;

                var clist = m_Main.Client.get_node_categories();
                foreach (cm_node_category_rpc c in clist)
                {
                    _categories.Add( c );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void treeView_ClickAdd(object sender, PNRoutedEventArgs e)
        {
            var item = e.SourceItem as CMNode;
            var new_item = new CMNode(new cm_node_rpc()
            {
                //id =  Do not set the id, as the server will generate id for us.
                name = "New Node",
                desc = "",
                parent = item.Data as cm_node_rpc,
                children = new List<cm_node_rpc>(),
                category = new cm_node_category_rpc()
                {
                    id = 100,
                    name = "room",
                    desc = "住户"
                },
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

            if (true)
            {
                page.Editor.DataContext = item.CloneData();
                return;
            }

            item.UpdateData(page.EditorData); // Update data object
            item.UpdateGUI(); // Update GUI
            // Trigger parent update
            item.Parent = (page.Editor as CMNodeEditor).nodeList.SelectedItem as CMNode;
            item.IsNew = false;
        }
    }
}
