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
using System.Windows.Shapes;
using MiniEClient.ctrls;
using MiniEClient.data;

namespace MiniEClient.frames
{
    class CateData : ICloneable
    {
        public PNItemType type { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    class CateDataItem : PNTreeViewItem
    {
        private CateData _data;
        public override object Data
        {
            get
            {
                return _data;
            }
        }

        public override string DisplayName
        {
            get
            {
                return _data.name;
            }
        }

        public override string Id
        {
            get
            {
                return _data.id;
            }
        }

        public override string Tips
        {
            get
            {
                return _data.desc;
            }
        }

        public override PNItemType Type
        {
            get
            {
                return _data.type;
            }
        }

        public override string Editor
        {
            get
            {
                return string.Empty;
            }
        }

        public override object CloneData()
        {
            return _data.Clone();
        }

        public override void UpdateData(object data)
        {
            throw new NotImplementedException();
        }
        public CateDataItem(CateData data, CateDataItem parent = null) : base(parent)
        {
            _data = data;
        }
    }
    /// <summary>
    /// UsersPage.xaml 的交互逻辑
    /// </summary>
    public partial class UsersPage : Page
    {
        private MainWindow m_Main;
        public UsersPage(MainWindow main)
        {
            m_Main = main;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            var sys = new CateDataItem( new CateData() { name = "System", desc = "System Edit", type = PNItemType.BOLE | PNItemType.NOEDIT | PNItemType.NOADD | PNItemType.NODELETE} );
            var sys_roles = new CateDataItem(new CateData() { name = "Roles", desc = "Roles Edit", type = PNItemType.BOLE | PNItemType.NOEDIT | PNItemType.NODELETE }, sys);
            var role1 = new SYSRole(new sys_role_rpc() { name = "Role1", desc = "Role1" }, sys_roles);
            var role2 = new SYSRole(new sys_role_rpc() { name = "Role2", desc = "Role2" }, sys_roles);
            var sys_groups = new CateDataItem(new CateData() { name = "Groups", desc = "Groups Edit", type = PNItemType.BOLE | PNItemType.NOEDIT | PNItemType.NODELETE }, sys);
            var group1 = new SYSGroup(new sys_group_rpc() { name = "Group1", desc = "Group1" }, sys_groups);
            var group2 = new SYSGroup(new sys_group_rpc() { name = "Group2", desc = "Group2" }, sys_groups);
            (group1.Data as sys_group_rpc).group_roles.Add(role1.Data as sys_role_rpc);
            (group2.Data as sys_group_rpc).group_roles.Add(role1.Data as sys_role_rpc);
            (group2.Data as sys_group_rpc).group_roles.Add(role2.Data as sys_role_rpc);
            var sys_users = new CateDataItem(new CateData() { name = "Users", desc = "Users Edit", type = PNItemType.BOLE | PNItemType.NOEDIT | PNItemType.NODELETE }, sys);
            // Get roles
            PNTreeViewItemList categories = new PNTreeViewItemList();
            categories.Add(sys);

            treeView.ItemsSource = categories;
        }
        private void treeView_ClickAdd(object sender, ctrls.PNRoutedEventArgs e)
        {

        }

        private void treeView_ClickDelete(object sender, ctrls.PNRoutedEventArgs e)
        {

        }

        private void treeView_ClickEdit(object sender, ctrls.PNRoutedEventArgs e)
        {

        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = treeView.SelectedItem as PNTreeViewItem;
            var editor = item.CreateEditor(item.CloneData());
            if (editor == null)
            {
                frame.Navigate(null);
                return;
            }

            var page = new frames.EditorPage()
            {
                Editor = editor,
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
            item.IsNew = false;
        }
    }
}
