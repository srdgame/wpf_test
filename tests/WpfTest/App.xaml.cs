using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace treeview
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Application.Current.Resources.MergedDictionaries.Clear();
            //ResourceDictionary resource = (ResourceDictionary)Application.LoadComponent(
            //        new Uri("Dictionary1.xaml", UriKind.Relative));
            //Application.Current.Resources.MergedDictionaries.Add(resource);
        }
    }
}
