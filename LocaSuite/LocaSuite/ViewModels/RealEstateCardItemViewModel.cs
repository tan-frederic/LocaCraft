using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocaSuite.DataServices;
using LocaSuite.Models;
using System.Windows;

namespace LocaSuite.ViewModels
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
        public void AskDeleteRealEstate()
        {
            if (MessageBox.Show("Are you sure you want to delete this estate?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //DeleteRealEstate();
            }
        }

        private async Task DeleteRealEstate()
        {
            await _service.DeleteRealEstateAsset(RealEstateAssetModel.Id);
        }

        [RelayCommand]
        public void OpenDetailRealEstate()
        {

        }
    }
}
