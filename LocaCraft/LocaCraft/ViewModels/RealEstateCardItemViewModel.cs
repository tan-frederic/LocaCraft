using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocaCraft.DataServices;
using LocaCraft.Models;
using System.Windows;

namespace LocaCraft.ViewModels
{
    public partial class RealEstateCardItemViewModel : ObservableObject
    {
        #region ATTRIBUTES
        public RealEstateAssetModel RealEstateAssetModel { get; private set; }

        private RealEstateDataService _service = new RealEstateDataService();

        #endregion

        #region CONSTRUCTOR
        public RealEstateCardItemViewModel(RealEstateAssetModel realEstateAssetModel)
        {
            RealEstateAssetModel = realEstateAssetModel;
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

        }
    }
}
