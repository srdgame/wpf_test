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
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public string TestValue
        {
            get { return GetValue(TestValueProperty) as string; }
            set { SetValue(TestValueProperty, value); }
        }
        public static readonly DependencyProperty TestValueProperty =
            DependencyProperty.Register(
                "TestValue",
                typeof(string),
                typeof(UserControl1),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public UserControl1()
        {
            InitializeComponent();
            SetBinding(TestValueProperty, "TestValue");
        }
    }
}
