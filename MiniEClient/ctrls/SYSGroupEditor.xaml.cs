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
    /// CMEntranceEditor.xaml 的交互逻辑
    /// </summary>
    public partial class SYSGroupEditor : UserControl
    {
        public static readonly DependencyProperty NodeListProperty =
            DependencyProperty.Register("NodeList", typeof(IEnumerable<object>), typeof(SYSGroupEditor), new FrameworkPropertyMetadata(null));

        public IEnumerable<object> NodeList
        {
            get { return (IEnumerable<object>)GetValue(NodeListProperty); }
            set { SetValue(NodeListProperty, value); }
        }
        public static readonly DependencyProperty RoleListProperty =
            DependencyProperty.Register("RoleList", typeof(IEnumerable), typeof(SYSGroupEditor), new FrameworkPropertyMetadata(null));

        public IEnumerable RoleList
        {
            get { return (IEnumerable<object>)GetValue(RoleListProperty); }
            set { SetValue(RoleListProperty, value); }
        }
        public SYSGroupEditor()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var list = new ObservableCollection<object>();
            list.Add(new sys_role_rpc() { name = "T1", desc = "Desc1" });
            list.Add(new sys_role_rpc() { name = "T1", desc = "Desc1" });
            RoleList = list;
        }
        
    }
}
