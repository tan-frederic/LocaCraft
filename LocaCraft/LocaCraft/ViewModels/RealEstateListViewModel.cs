using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocaCraft.DataServices;
using LocaCraft.Models;
using LocaCraft.Views;
using System.Collections.ObjectModel;

namespace LocaCraft.ViewModels
{
    public partial class RealEstateListViewModel : ObservableObject
    {
        #region ATTRIBUTES
        public ObservableCollection<RealEstateAssetModel> RealEstateAssetModels { get; set; } = new ObservableCollection<RealEstateAssetModel>();
        public ObservableCollection<RealEstateCardItemViewModel> RealEstatesDisplayed { get; } = new();

        private readonly RealEstateDataService _service = new RealEstateDataService();
        private NewRealEstateView? _newRealEstateView;

        [ObservableProperty]
        private string _searchText;

        private const int PageSize = 4;
        [ObservableProperty]
        private int _currentPage = 0;
        #endregion

        #region CONSTRUCTOR
        public RealEstateListViewModel()
        {

        }
        #endregion

        public async Task LoadRealEstateAsync()
        {
            RealEstateAssetModels.Clear();
            var data = await _service.GetRealEstateAssetsAsync();
            foreach (var realEstate in data)
                RealEstateAssetModels.Add(realEstate);
            UpdateRealEstateList();
        }

        /// <summary>
        /// Open the new real estate window.
        /// </summary>
        [RelayCommand]
        public void OpenNewRealEstateWindow()
        {
            if (_newRealEstateView == null || !_newRealEstateView.IsLoaded)
            {
                _newRealEstateView = new NewRealEstateView();
                _newRealEstateView.Closed += (s, e) => _newRealEstateView = null; // Add event handler to set _newRealEstateView to null when closed
                _newRealEstateView.Show(); // Show the new window
                _newRealEstateView.Focus(); // Set focus to the new window
            }
            else
            {
                _newRealEstateView.Focus(); // Bring the existing window to the front
            }

        }

        partial void OnSearchTextChanged(string searchText)
        {
            UpdateRealEstateList();
        }

        private void UpdateRealEstateList()
        {
            List<RealEstateAssetModel> filteredRealEstate;
            RealEstatesDisplayed.Clear();

            if (string.IsNullOrEmpty(SearchText))
            {
                filteredRealEstate = RealEstateAssetModels.ToList();
            }
            else
            {
                filteredRealEstate = RealEstateAssetModels
                    .Where(x => (x.Name?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (x.Address?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (x.AddressComplement?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (x.City?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (x.PostalCode?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false))
                    .ToList();
            }


            foreach (var realEstate in filteredRealEstate)
            {
                RealEstatesDisplayed.Add(new RealEstateCardItemViewModel(realEstate));
            }
        }
    }
}
