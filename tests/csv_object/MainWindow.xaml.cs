using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace csv_object
{
    public class TestData : ICloneable
    {
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public List<TestData> children { get; set; }
        public DateTime time { get; set; }
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            using (var csv = new CsvReader(new StreamReader("C:\ttt\test.csv")))
            {
                var list = csv.GetRecords<TestData>();
            }
        }
    }
}
