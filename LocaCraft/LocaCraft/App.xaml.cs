using LocaCraft.Services;
using LocaCraft.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace LocaCraft
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region VARIABLES
        private readonly NavigationStore _navigationStore;
        #endregion

        App()
        {
            // Initialize the application and set up any necessary resources or services here.
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _navigationStore.CurrentViewModel = new RealEstateListViewModel(_navigationStore);
            var mainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(_navigationStore)
            };
            mainWindow.Show();
        }
    }

}
