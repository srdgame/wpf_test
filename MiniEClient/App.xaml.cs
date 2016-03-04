using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using minie.irpc;

namespace MiniEClient
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public minie_backend_client Client { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            var window = new diags.LoginWindow();
            bool? dialogResult = window.ShowDialog();
            if ((dialogResult.HasValue == true) &&
                (dialogResult.Value == true))
            {
                base.OnStartup(e);
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            }
            else
            {
                this.Shutdown();
            }
        }
    }
}
