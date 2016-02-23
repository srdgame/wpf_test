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
    /// <summary>
    /// DistMgr.xaml 的交互逻辑
    /// </summary>
    public partial class UnitMgr : Page
    {
        private ObservableCollection<CMNodeCategory> _categories;
        private ObservableCollection<PropertyNodeItem<CMNodeBase>> _item_list;
        private MainWindow m_Main;

        public UnitMgr(MainWindow main)
        {
            m_Main = main;
            InitializeComponent();
            _categories = new ObservableCollection<CMNodeCategory>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var itemList = new ObservableCollection<PropertyNodeItem<CMNodeBase>>();

            var haidian = new PropertyNodeItem<CMNodeBase>(PropertyNodeType.BOLE, new CMNodeBase()
            {
                id = Guid.NewGuid().ToString(),
                name = "海淀区",
                desc = "海淀行政区.",
                category = 400,
                address = "Address....."
            });

            var zgceast = new PropertyNodeItem<CMNodeBase>(PropertyNodeType.BOLE, new CMNodeBase()
            {
                id = Guid.NewGuid().ToString(),
                name = "中关村东路",
                desc = "中关村东路下辖小区.",
                category = 400,
                address = "Address....."
            });
            var zdhbuilding = new PropertyNodeItem<CMNodeBase>(PropertyNodeType.LEAF, new CMNodeBase()
            {
                id = Guid.NewGuid().ToString(),
                name = "自动化大厦",
                desc = "中关村东路95号.",
                category = 300,
                address = "Address....."
            });
            zgceast.Children.Add(zdhbuilding);
            haidian.Children.Add(zgceast);

            var cg = new PropertyNodeItem<CMNodeBase>(PropertyNodeType.LEAF, new CMNodeBase()
            {
                id = Guid.NewGuid().ToString(),
                name = "翠宫",
                desc = "翠宫饭店.",
                category = 300,
                address = "cuigong....."
            });
            haidian.Children.Add(cg);
            itemList.Add(haidian);
            var chaoyang = new PropertyNodeItem<CMNodeBase>(PropertyNodeType.BOLE, new CMNodeBase()
            {
                id = Guid.NewGuid().ToString(),
                name = "海区",
                desc = "海行政区.",
                category = 400,
                address = "Address....."
            });
            itemList.Add(chaoyang);
            _item_list = itemList;

            this.treeView.ItemsSource = itemList;
            _categories.Add(new CMNodeCategory() { name = "住户", value = 100 });
            _categories.Add(new CMNodeCategory() { name = "单元", value = 200 });
            _categories.Add(new CMNodeCategory() { name = "楼房", value = 300 });
            _categories.Add(new CMNodeCategory() { name = "小区", value = 400 });
        }

        private void treeView_ClickAdd(object sender, PNRoutedEventArgs e)
        {

        }

        private void treeView_ClickEdit(object sender, PNRoutedEventArgs e)
        {
            var page = frame.Content as EditorPage;
            page.IsEditable = true;
        }

        private void treeView_ClickDelete(object sender, PNRoutedEventArgs e)
        {

        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var v = treeView.SelectedValue as ICloneable;
            var page = new frames.EditorPage()
            {
                Editor = new CMNodeEditor()
                {
                    DataContext = v.Clone(),
                    Categories = _categories,
                    NodeList = _item_list,
                },
            };
            page.Save += OnSave;
            frame.Navigate(page);
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            var page = frame.Content as EditorPage;
            var item = treeView.SelectedItem as PropertyNodeItem<CMNodeBase>;
            item.Data = page.EditorData as CMNodeBase;
        }
    }
}
