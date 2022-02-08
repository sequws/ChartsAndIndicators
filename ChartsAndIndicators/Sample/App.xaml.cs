using ControlzEx.Theming;
using Prism.Ioc;
using Prism.Modularity;
using Sample.Views;
using System.Threading;
using System.Windows;

namespace Sample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            var lang = Sample.Properties.Settings.Default.AppLanguage;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);

            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<DemoView1>("Demo1View"); register in MainWindow with region

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Set the application theme to Dark.Green
            //ThemeManager.Current.ChangeTheme(this, "Dark.Green");
        }
    }
}
