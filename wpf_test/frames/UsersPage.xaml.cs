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
using wpf_test.data;

namespace wpf_test.frames
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

        public override System.Type Editor
        {
            get
            {
                throw new NotImplementedException();
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
        public ObservableCollection<sys_role_permission_rpc> PersmissionList;
        private MainWindow m_Main;
        public UsersPage(MainWindow main)
        {
            m_Main = main;
            PersmissionList = new ObservableCollection<sys_role_permission_rpc>();
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            ad 广告操作
            community 社区操作
            entrance 编辑门禁
            role 角色管理操作
            user 用户操作  
            */
            PersmissionList.Add(new sys_role_permission_rpc() { name = "ad", desc = "广告操作" });
            PersmissionList.Add(new sys_role_permission_rpc() { name = "community", desc = "社区操作" });
            PersmissionList.Add(new sys_role_permission_rpc() { name = "entrance", desc = "编辑门禁" });
            PersmissionList.Add(new sys_role_permission_rpc() { name = "role", desc = "角色管理操作" });
            PersmissionList.Add(new sys_role_permission_rpc() { name = "user", desc = "用户操作" });

            var sys = new CateDataItem( new CateData() { name = "System", desc = "System Edit", type = PNItemType.BOLE | PNItemType.NOEDIT } );
            var sys_roles = new CateDataItem(new CateData() { name = "Roles", desc = "Roles Edit", type = PNItemType.BOLE | PNItemType.NOEDIT }, sys);
            var role1 = new SYSRole(new sys_role_rpc() { name = "Role1", desc = "Role1" }, sys_roles);
            var role2 = new SYSRole(new sys_role_rpc() { name = "Role2", desc = "Role2" }, sys_roles);
            var sys_groups = new CateDataItem(new CateData() { name = "Groups", desc = "Groups Edit", type = PNItemType.BOLE | PNItemType.NOEDIT }, sys);
            var sys_users = new CateDataItem(new CateData() { name = "Users", desc = "Users Edit", type = PNItemType.BOLE | PNItemType.NOEDIT }, sys);
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

        }

    }
}
