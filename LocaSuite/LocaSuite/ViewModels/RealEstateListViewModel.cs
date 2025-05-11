using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocaSuite.DataServices;
using LocaSuite.Models;
using LocaSuite.Views;
using System.Collections.ObjectModel;

namespace LocaSuite.ViewModels
{
    public partial class RealEstateListViewModel : ObservableObject
    {
        #region ATTRIBUTES
        public ObservableCollection<RealEstateAssetModel> RealEstateAssetModels { get; set; } = new ObservableCollection<RealEstateAssetModel>();

        private readonly RealEstateDataService _service = new RealEstateDataService();
        private NewRealEstateView? _newRealEstateView;

        private List<RealEstateAssetModel> _listRealEstateModel;
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
    }
}
