using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocaCraft.Commands;
using LocaCraft.Services;
using System.Windows.Input;

namespace LocaCraft.ViewModels
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        #region Variables
        [ObservableProperty]
        private BaseViewModel _currentViewModel;

        private readonly NavigationStore _navigationStore;

        public ICommand NavigateToHome { get; }
        #endregion

        #region RELAYCOMMANDS

        private RelayCommand _navigateToRealEstateListCommand;

        public IRelayCommand NavigateToRealEstateListCommand
        {
            get
            {
                return _navigateToRealEstateListCommand ??= new RelayCommand(NavigateToRealEstateList);
            }
        }

        #endregion

        #region Constructor
        public MainWindowViewModel(NavigationStore navigationServices)
        {
            _navigationStore = navigationServices;
            CurrentViewModel = _navigationStore.CurrentViewModel;
            NavigateToHome = new CommandNavigation<RealEstateListViewModel>(new NavigationService<RealEstateListViewModel>(_navigationStore, () => new RealEstateListViewModel(_navigationStore)));
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }
        #endregion

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModel = _navigationStore.CurrentViewModel;
        }

        public void NavigateToRealEstateList()
        {
            NavigateToHome.Execute(null);
        }
    }
}
