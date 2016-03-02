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

namespace MiniEClient.ctrls
{
    /// <summary>
    /// CMNodeEditor.xaml 的交互逻辑
    /// </summary>
    public partial class CMNodeEditor : UserControl
    {

        public static readonly DependencyProperty CategoriesProperty =
            DependencyProperty.Register("Categories", typeof(ObservableCollection<CMNodeCategory>), typeof(CMNodeEditor), new FrameworkPropertyMetadata(null));

        public ObservableCollection<CMNodeCategory> Categories
        {
            get { return (ObservableCollection<CMNodeCategory>)GetValue(CategoriesProperty); }
            set { SetValue(CategoriesProperty, value); }
        }

        public static readonly DependencyProperty NodeListProperty =
            DependencyProperty.Register("NodeList", typeof(IEnumerable<object>), typeof(CMNodeEditor), new FrameworkPropertyMetadata(null));

        public IEnumerable<object> NodeList
        {
            get { return (IEnumerable<object>)GetValue(NodeListProperty); }
            set { SetValue(NodeListProperty, value); }
        }
        public CMNodeEditor()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           // categoryList.ItemsSource = Categories;
        }
    }
}
