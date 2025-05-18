using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocaCraft.Commands;
using LocaCraft.DataServices;
using LocaCraft.Models;
using LocaCraft.Services;
using System.Windows;
using System.Windows.Input;

namespace LocaCraft.ViewModels
{
    public partial class RealEstateCardItemViewModel : BaseViewModel
    {
        #region ATTRIBUTES
        public ICommand NavigateToDetail { get; }

        public RealEstateAssetModel RealEstateAssetModel { get; private set; }

        private RealEstateDataService _service = new RealEstateDataService();

        #endregion

        #region CONSTRUCTOR
        public RealEstateCardItemViewModel(RealEstateAssetModel realEstateAssetModel, NavigationStore navigationStore)
        {
            RealEstateAssetModel = realEstateAssetModel;
            NavigateToDetail = new CommandNavigation<RealEstateDetailsViewModel>(new NavigationService<RealEstateDetailsViewModel>(navigationStore, () => new RealEstateDetailsViewModel(realEstateAssetModel, navigationStore)));
        }
        #endregion

        public void UpdateRealEstateCardItem(RealEstateAssetModel realEstateAssetModel)
        {
            RealEstateAssetModel = realEstateAssetModel;
            OnPropertyChanged(nameof(RealEstateAssetModel));
        }

        [RelayCommand]
        public async Task AskDeleteRealEstate()
        {
            if (MessageBox.Show("Are you sure you want to delete this estate?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                await _service.DeleteRealEstateAsset(RealEstateAssetModel.Id);
            }
        }

        [RelayCommand]
        public void OpenDetailRealEstate()
        {
            NavigateToDetail.Execute(RealEstateAssetModel);
        }
    }
}
