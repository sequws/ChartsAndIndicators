using NLog;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Sample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private string _title = "ChartAndIndicators example App";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private readonly IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);

            //logger.Info("MainWindow Start!");
        }

        private void Navigate(string uri)
        {
            _regionManager.RequestNavigate("ContentRegion", uri);
        }

        #region commands
        public DelegateCommand<string> NavigateCommand { get; set; }

        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(ExecuteCloseCommand, CanExecuteCloseCommand));

        void ExecuteCloseCommand()
        {
            App.Current.Shutdown();
        }

        bool CanExecuteCloseCommand()
        {
            return true;
        }
        #endregion
    }
}
