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

namespace binding_test
{
    class ListViewData
    {
        public string name { get; set; }
        public string desc { get; set; }
    }
    /// <summary>
    /// ListView.xaml 的交互逻辑
    /// </summary>
    public partial class ListViewCtrl : UserControl
    {
        public List<object> ItemsList { get; set; }
        public ListViewCtrl()
        {
            //var list = new List<object>();
            //list.Add(new ListViewData() { name = "T1", desc = "TDDD" });
            //list.Add(new ListViewData() { name = "T2", desc = "TDDD2" });
            //ItemsList = list;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //var list = new List<object>();
            //list.Add(new ListViewData() { name = "T1", desc = "TDDD" });
            //list.Add(new ListViewData() { name = "T2", desc = "TDDD2" });
            //ItemsList = list;
        }
    }
}
