using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocaCraft.Views;

namespace LocaCraft.ViewModels
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        #region Variables
        [ObservableProperty]
        private BaseViewModel _currentView;
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            _currentView = new RealEstateListViewModel();
        }
        #endregion

        [RelayCommand]
        public void ShowRealEstateList()
        {
            _currentView = new RealEstateListViewModel();
        }
    }
}
