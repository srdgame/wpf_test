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
using minie.irpc;

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
        public override object Data { get { return _data; } }
        public override string DisplayName { get { return _data.name; } }
        public override string Id { get { return _data.id; } }
        public override string Tips { get { return _data.desc; } }
        public override PNItemType Type { get { return _data.type; } }
        public override string Editor { get { return string.Empty; } }

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
        public Func<PNTreeViewItem, PNTreeViewItem> Creator { get; set; }
    }
    /// <summary>
    /// UsersPage.xaml 的交互逻辑
    /// </summary>
    public partial class UsersPage : Page
    {
        private PNTreeViewItemList _node_list;
        private List<sys_role_rpc> _role_list;
        private MainWindow m_Main;
        public UsersPage()
        {
            var i = sys_role_rpc_equals.instance;
            m_Main = App.Current.MainWindow as MainWindow;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var sys = new CateDataItem( new CateData() { name = "System", desc = "System Edit", type = PNItemType.BOLE | PNItemType.NOEDIT | PNItemType.NOADD | PNItemType.NODELETE} );
            var sys_roles = new CateDataItem(new CateData() { name = "Roles", desc = "Roles Edit", type = PNItemType.BOLE | PNItemType.NOEDIT | PNItemType.NODELETE }, sys);
            sys_roles.Creator = CreateRole; 
            var sys_groups = new CateDataItem(new CateData() { name = "Groups", desc = "Groups Edit", type = PNItemType.BOLE | PNItemType.NOEDIT | PNItemType.NODELETE }, sys);
            sys_groups.Creator = CreateGroup; 
            var sys_users = new CateDataItem(new CateData() { name = "Users", desc = "Users Edit", type = PNItemType.BOLE | PNItemType.NOEDIT | PNItemType.NODELETE }, sys);
            sys_users.Creator = CreateUser;

            var roles = m_Main.Client.get_roles();
            foreach (var item in roles)
            {
                new SYSRole(item, sys_roles);
            }
            var groups = m_Main.Client.get_groups();
            foreach( var item in groups)
            {
                new SYSGroup(item, sys_groups);
                foreach (var user in item.group_users)
                {
                    new SYSUser(user, sys_users);
                }
            }

            PNTreeViewItemList treeList = new PNTreeViewItemList();
            treeList.Add(sys);

            treeView.ItemsSource = treeList;


            // Get roles
            _role_list = roles;
            var node_list = (App.Current as App).Client.Proxy.get_nodes();
            _node_list = new PNTreeViewItemList();
            foreach (cm_node_rpc node in node_list)
            {
                _node_list.Add(new CMNode(node));

            }
        }

        private PNTreeViewItem CreateUser(PNTreeViewItem item)
        {
            return new SYSUser(new sys_user_rpc()
            {
                id = Guid.NewGuid().ToString()
            }, item);
        }

        private PNTreeViewItem CreateGroup(PNTreeViewItem item)
        {
            return new SYSGroup(new sys_group_rpc()
            {
                id = Guid.NewGuid().ToString(),
                name = "New Group",
            }, item);
        }

        private PNTreeViewItem CreateRole(PNTreeViewItem item)
        {
            return new SYSRole(new sys_role_rpc()
            {
                name = "New Role",
            }, item);
        }

        private void treeView_ClickAdd(object sender, PNRoutedEventArgs e)
        {
            var item = e.SourceItem as CateDataItem;
            var new_item = item.Creator(item);
            new_item.IsNew = true;
            new_item.IsSelected = true;
        }

        private void treeView_ClickDelete(object sender, PNRoutedEventArgs e)
        {

        }

        private void treeView_ClickEdit(object sender, PNRoutedEventArgs e)
        {
            var page = frame.Content as EditorPage;
            if (page != null)
                page.IsEditable = true;
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
            if (editor as SYSGroupEditor != null)
            {
                (editor as SYSGroupEditor).NodeList = _node_list;
                (editor as SYSGroupEditor).RoleList = _role_list;
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
        private int SaveGroup(sys_group_rpc group, bool isNew)
        {
            var data = group.CleanupEx();

            //var data = group.Clone() as sys_group_rpc;
            //var group_users = data.group_users;
            //var group_roles = data.group_roles;
            //if (group_users.Count == 0)
            //{
            //    //group_users.Add(m_Main.Client.get_current_user_info().Clone() as sys_user_rpc);
            //    group_users.Add(new sys_user_rpc()
            //    {
            //        id = m_Main.Client.get_current_user_info().id,
            //    });
            //}

            //data.group_users = new List<sys_user_rpc>();
            //foreach (var item in group_users)
            //{
            //    var user = new sys_user_rpc()
            //    {
            //        id = item.id,
            //    };
            //    data.group_users.Add(user);
            //}
            //data.group_roles = new List<sys_role_rpc>();
            //foreach (var item in group_roles)
            //{
            //    var role = new sys_role_rpc()
            //    {
            //        name = item.name,
            //        desc = item.desc,
            //    };
            //    data.group_roles.Add(role);
            //}
            //if (data.node == null)
            //{
            //    var gl = m_Main.Client.get_current_user_info().user_groups;
            //    //data.node = gl.First().node.Clone() as cm_node_rpc;
            //    data.node = new cm_node_rpc()
            //    {
            //        id = gl.First().node.id,
            //    };
            //}
            //if (data.node != null)
            //{
            //    var node = data.node;
            //    data.node = new cm_node_rpc()
            //    {
            //        id = node.id,
            //    };
            //}

            return isNew ? m_Main.Client.add_sys_group(data) : m_Main.Client.update_sys_group(data);
        }

        private int SaveUser(sys_user_rpc user, bool isNew)
        {
            var data = user.CleanupEx();
            return isNew ? m_Main.Client.add_sys_user(data) : m_Main.Client.update_sys_user(data);
        }

        private int SaveRole(sys_role_rpc role, bool isNew)
        {
            return -1;
            //var data = role.CleanupEx();
            //return isNew ? m_Main.Client.add_sys_role(data) : m_Main.Client.update_sys_role(data);
        }
        private void OnSave(object sender, RoutedEventArgs e)
        {
            var page = frame.Content as EditorPage;
            var item = treeView.SelectedItem as PNTreeViewItem;

            if (page.EditorData as sys_group_rpc != null)
            {
                if (SaveGroup(page.EditorData as sys_group_rpc, item.IsNew) != 0)
                {
                    MessageBox.Show("Cannot save data to server", "Error");
                    page.Editor.DataContext = item.CloneData();
                    return;
                }
            }
            if (page.EditorData as sys_role_rpc != null)
            {
                if (SaveRole(page.EditorData as sys_role_rpc, item.IsNew) != 0)
                {
                    MessageBox.Show("Cannot save data to server", "Error");
                    page.Editor.DataContext = item.CloneData();
                    return;
                }
            }
            if (page.EditorData as sys_user_rpc != null)
            {
                if (SaveUser(page.EditorData as sys_user_rpc, item.IsNew) != 0)
                {
                    MessageBox.Show("Cannot save data to server", "Error");
                    page.Editor.DataContext = item.CloneData();
                    return;
                }
            }
            item.UpdateData(page.EditorData);
            item.UpdateGUI();
            item.IsNew = false;
        }

    }
}
