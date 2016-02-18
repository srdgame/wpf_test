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

namespace treeview
{
    /// <summary>
    /// ItemShowControl.xaml 的交互逻辑
    /// </summary>
    public partial class ItemShowControl : UserControl
    {
        private PropertyNodeItem _item;
        public ItemShowControl()
        {
            InitializeComponent();
            //this.DataContext = new PropertyNodeItem();
        }
        public void Bind(PropertyNodeItem item)
        {
            this.DataContext = null;
            this._item = item;
            this.DataContext = item.Clone();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            PropertyNodeItem item = this.DataContext as PropertyNodeItem;
            this._item.DisplayName = item.DisplayName;
        }
    }
}
