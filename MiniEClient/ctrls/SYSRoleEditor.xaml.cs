using System;
using System.Collections;
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
using MiniEClient.data;
using minie.irpc;

namespace MiniEClient.ctrls
{
    /// <summary>
    /// SYSRoleEditor.xaml 的交互逻辑
    /// </summary>
    public partial class SYSRoleEditor : UserControl
    {
        public static readonly DependencyProperty PermissionListProperty =
            DependencyProperty.Register("PermissionList", typeof(IEnumerable), typeof(SYSRoleEditor), new FrameworkPropertyMetadata(null));

        public IEnumerable PermissionList
        {
            get { return (IEnumerable)GetValue(PermissionListProperty); }
            set { SetValue(PermissionListProperty, value); }
        }

        public static readonly DependencyProperty SelectedListProperty =
            DependencyProperty.Register("SelectedList", typeof(IList), typeof(SYSRoleEditor),
                new FrameworkPropertyMetadata(null,  FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public IList SelectedList
        {
            get { return (IList)GetValue(SelectedListProperty); }
            set { SetValue(SelectedListProperty, value); }
        }
        private ObservableCollection<SYSPermission> _permission_List;
        private List<sys_permission_rpc> _permission_List2;
        public SYSRoleEditor()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _permission_List = new ObservableCollection<SYSPermission>();
            _permission_List2 = new List<sys_permission_rpc>();
            /*
            ad 广告操作
            community 社区操作
            entrance 编辑门禁
            role 角色管理操作
            user 用户操作  
            */
            _permission_List.Add(new SYSPermission(new sys_permission_rpc() { name = "ad", desc = "广告操作" }));
            _permission_List.Add(new SYSPermission(new sys_permission_rpc() { name = "community", desc = "社区操作" }));
            _permission_List.Add(new SYSPermission(new sys_permission_rpc() { name = "entrance", desc = "编辑门禁" }));
            _permission_List.Add(new SYSPermission(new sys_permission_rpc() { name = "role", desc = "角色管理操作" }));
            _permission_List.Add(new SYSPermission(new sys_permission_rpc() { name = "user", desc = "用户操作" }));
            _permission_List2.Add(new sys_permission_rpc() { name = "user", desc = "用户操作" });
            PermissionList = _permission_List;
            SelectedList = _permission_List2;
            //listView.ItemsSelected = SelectedList;
        }
    }
}
