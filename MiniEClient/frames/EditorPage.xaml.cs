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

namespace wpf_test.frames
{
    /// <summary>
    /// EditorPage.xaml 的交互逻辑
    /// </summary>
    public partial class EditorPage : Page
    {
        private bool _is_editable = false;
        public bool IsEditable
        {
            get { return _is_editable; }
            set
            {
                if (_is_editable != value)
                {
                    _is_editable = value;
                    frame.IsEnabled = value;
                    button.Content = value ? "Save" : "Edit";
                    RoutedEventArgs e = new RoutedEventArgs(value ? EditEvent : SaveEvent);
                    e.Source = this;
                    RaiseEvent(e);
                }
            }
        }
        public UserControl Editor { get; set; }
        public object EditorData { get { return Editor.DataContext; } }
        public EditorPage()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent EditEvent =
            EventManager.RegisterRoutedEvent(
            "Edit", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(EditorPage));

        public event RoutedEventHandler Edit
        {
            add { AddHandler(EditorPage.EditEvent, value, false); }
            remove { RemoveHandler(EditorPage.EditEvent, value); }
        }

        public static readonly RoutedEvent SaveEvent =
            EventManager.RegisterRoutedEvent(
            "Save", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(EditorPage));

        public event RoutedEventHandler Save
        {
            add { AddHandler(EditorPage.SaveEvent, value, false); }
            remove { RemoveHandler(EditorPage.SaveEvent, value); }
        }
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            frame.Navigate(Editor);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            IsEditable = !IsEditable;
        }
    }
}
