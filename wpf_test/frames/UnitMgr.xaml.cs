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
using wpf_test.data;

namespace wpf_test.frames
{
    /// <summary>
    /// DistMgr.xaml 的交互逻辑
    /// </summary>
    public partial class UnitMgr : Page
    {
        private MainWindow m_Main;
        public UnitMgr(MainWindow main)
        {
            m_Main = main;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<PropertyNodeItem> itemList = new ObservableCollection<PropertyNodeItem>();

            PropertyNodeItem haidian = new PropertyNodeItem(PropertyNodeType.BOLE)
            {
                DisplayName = "海淀区",
                Tips = "海淀行政区.",
            };

            PropertyNodeItem zgceast = new PropertyNodeItem(PropertyNodeType.BOLE)
            {
                DisplayName = "中关村东路",
                Tips = "中关村东路下辖小区.",
            };
            PropertyNodeItem zdhbuilding = new PropertyNodeItem(PropertyNodeType.LEAF)
            {
                DisplayName = "自动化大厦",
                Tips = "中关村东路95号.",
            };
            zgceast.Children.Add(zdhbuilding);
            haidian.Children.Add(zgceast);

            PropertyNodeItem cg = new PropertyNodeItem(PropertyNodeType.LEAF)
            {
                DisplayName = "翠宫",
                Tips = "翠宫饭店.",
            };
            haidian.Children.Add(cg);
            itemList.Add(haidian);
            

            this.treeView.ItemsSource = itemList;
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
