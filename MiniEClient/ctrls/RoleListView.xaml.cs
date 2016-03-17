using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace MiniEClient.ctrls
{
    /// <summary>
    /// RoleListView.xaml 的交互逻辑
    /// </summary>
    public partial class RoleListView : UserControl
    {
        public RoleListView()
        {
            InitializeComponent();
        }

        static bool FindItem(IList list, object item)
        {
            foreach( object i in list)
            {
                if (EqualsChecker.Check(item, i))
                    return true;
                //if (item.Compare(i))
                //    return true;
            }
            return false;
        }
        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new diags.RoleListSelectorDlg() { ItemsSource = ItemsSource };
            bool? dialogResult = dlg.ShowDialog();
            if ((dialogResult.HasValue == true) &&
                (dialogResult.Value == true))
            {
                foreach(object item in dlg.SelectedItems)
                {
                    //if (!ItemsSelected.Contains(item))
                    if (!FindItem(ItemsSelected, item))
                    {
                        ItemsSelected.Add(item);
                        ItemsSelectedList.Add(item);
                    }
                }
            }
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            var item = listView.SelectedItem;
            ItemsSelected.Remove(item);
            ItemsSelectedList.Remove(item);
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(RoleListView),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnItemsSourceChanged)));

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = d as RoleListView;
        }

        public IEnumerable ItemsSource
        {
            get { return GetValue(ItemsSourceProperty) as IEnumerable; }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSelectedProperty =
            DependencyProperty.Register("ItemsSelected", typeof(IList), typeof(RoleListView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnSelectedItemsChanged)));
        
        public IList ItemsSelected
        {
            get { return GetValue(ItemsSelectedProperty) as IList; }
            set { SetValue(ItemsSelectedProperty, value); }
        }

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = d as RoleListView;
            thisControl.ItemsSelectedList = new ObservableCollection<object>();
            foreach (object item in thisControl.ItemsSelected)
            {
                thisControl.ItemsSelectedList.Add(item);
            }
        }

        private static readonly DependencyProperty ItemsSelectedListProperty =
            DependencyProperty.Register("ItemsSelectedList", typeof(ObservableCollection<object>), typeof(RoleListView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        private ObservableCollection<object> ItemsSelectedList
        {
            get { return GetValue(ItemsSelectedListProperty) as ObservableCollection<object>; }
            set { SetValue(ItemsSelectedListProperty, value); }
        }
    }
}
