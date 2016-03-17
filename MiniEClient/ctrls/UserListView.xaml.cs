using System;
using System.Collections;
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

namespace MiniEClient.ctrls
{
    /// <summary>
    /// UserListView.xaml 的交互逻辑
    /// </summary>
    public partial class UserListView : UserControl
    {
        public static readonly DependencyProperty ItemsSelectedProperty =
            DependencyProperty.Register("ItemsSelected", typeof(IList), typeof(UserListView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public IList ItemsSelected
        {
            get { return GetValue(ItemsSelectedProperty) as IList; }
            set { SetValue(ItemsSelectedProperty, value); }
        }
        public UserListView()
        {
            InitializeComponent();
        }
        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            //ItemsSelected.Add(item);
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            var item = listView.SelectedItem;
            ItemsSelected.Remove(item);
        }
    }
}
