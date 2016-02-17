using System;
using System.Collections.Generic;
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

namespace wpf_test.frames
{
    /// <summary>
    /// DistMgr.xaml 的交互逻辑
    /// </summary>
    public partial class DistMgr : Page
    {
        private MainWindow m_Main;
        public DistMgr(MainWindow main)
        {
            m_Main = main;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = "某某物业";
            TreeViewItem subitem = new TreeViewItem();
            TreeViewItem subitem2 = new TreeViewItem();
            subitem.Header = "某某小区";
            subitem2.Header = "某某小区2";
            item.Items.Add(subitem);
            item.Items.Add(subitem2);
            this.treeView.Items.Add(item);
            item.IsExpanded = true;
            subitem.IsSelected = true;
        }

        private void Button_Click_Add_Building(object sender, RoutedEventArgs e)
        {
            Window dlg = new diags.BuildingDlg();
            dlg.Owner = m_Main;
            dlg.ShowDialog();
        }

        private void button_dist_add_Click(object sender, RoutedEventArgs e)
        {
            Window dlg = new diags.DistDlg();
            dlg.Owner = m_Main;
            dlg.ShowDialog();
        }
    }
}
