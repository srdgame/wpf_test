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

namespace WpfTest
{
    public class TestClone : ICloneable
    {
        public string Name { get; set; }
        private List<string> _nodes;
        public List<string> Nodes { get { return _nodes;  }  set { _nodes = value; } }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public TestClone()
        {
            _nodes = new List<string>();
            _nodes.Add("Test1");
            _nodes.Add("test2");
        }
    }
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Type t = Type.GetType("WpfTest.diags.Login");
            //object login = Activator.CreateInstance(t);
            //Window dlg = login as Window;
            //dlg.ShowDialog();
            Type t = Type.GetType("WpfTest.Window1");
            var w = Activator.CreateInstance(t);
            var dlg = w as Window1;
            dlg.ShowDialog();

            TestClone tc1 = new TestClone();
            var tcc = tc1.Clone() as TestClone;
            tcc.Nodes.Clear();

            InitializeComponent();
        }

    }
}
