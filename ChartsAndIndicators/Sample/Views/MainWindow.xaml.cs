using MahApps.Metro.Controls;
using Prism.Regions;
using System.Windows;

namespace Sample.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();

            regionManager.RegisterViewWithRegion("ContentRegion", typeof(DemoView1));
        }
    }
}
