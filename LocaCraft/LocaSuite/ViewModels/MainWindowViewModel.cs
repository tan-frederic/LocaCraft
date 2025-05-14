using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocaCraft.Views;

namespace LocaCraft.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        #region Variables
        [ObservableProperty]
        private object _currentView;
        #endregion


        #region Constructor
        public MainWindowViewModel()
        {
            _currentView = new RealEstateListView();
        }
        #endregion

        [RelayCommand]
        public void ShowRealEstateList()
        {
            _currentView = new RealEstateListView();
        }
    }
}
