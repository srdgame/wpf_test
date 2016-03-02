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
using System.Windows.Shapes;
using wpf_test.data;

namespace wpf_test.diags
{
    /// <summary>
    /// RoleListSelectorDlg.xaml 的交互逻辑
    /// </summary>
    public partial class RoleListSelectorDlg : Window
    {
        public RoleListSelectorDlg()
        {
            InitializeComponent();
        }
        public IEnumerable ItemsSource
        {
            get { return listView.ItemsSource; }
            set { listView.ItemsSource = value;  }
        }
        public object SelectedItem
        {
            get { return listView.SelectedItem; }
            set { listView.SelectedItem = value; }
        }
        public IList SelectedItems
        {
            get { return listView.SelectedItems; }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
