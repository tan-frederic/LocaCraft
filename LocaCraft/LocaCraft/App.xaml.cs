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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            NavigationStore navigationServices = new NavigationStore();

            navigationServices.CurrentViewModel = new RealEstateListViewModel(navigationServices);
            var mainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(navigationServices)
            };
            mainWindow.Show();
        }
    }

}
