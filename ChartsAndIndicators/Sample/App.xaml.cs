using ControlzEx.Theming;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
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

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(LineChartDemo));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<BarChartDemo>("BarChartDemo"); 
            containerRegistry.RegisterForNavigation<LineChartDemo>("LineChartDemo");
            containerRegistry.RegisterForNavigation<OhlcChartDemo>("OhlcChartDemo");
            containerRegistry.RegisterForNavigation<PieChartDemo>("PieChartDemo");
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Set the application theme to Dark.Green
            //ThemeManager.Current.ChangeTheme(this, "Dark.Green");
        }
    }
}
