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

namespace MiniEClient.frames
{
    public class Message : data.ViewModelBase
    {
        public string Icon { get; set; }
        public string Content { get; set; }
        public HorizontalAlignment HA { get; set; }
    }
    /// <summary>
    /// Messages.xaml 的交互逻辑
    /// </summary>
    public partial class MessagesPage : Page
    {
        public MessagesPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Message> list = new ObservableCollection<Message>();
            list.Add(new Message() { Icon = "/image/icon/add.png", Content = "Test1", HA = HorizontalAlignment.Right });
            list.Add(new Message() { Icon = "/image/icon/delete.png", Content = "Test2", HA = HorizontalAlignment.Left });
            msgList.ItemsSource = list;
        }
    }
}
