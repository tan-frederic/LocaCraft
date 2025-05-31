using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocaCraft.DataServices;
using LocaCraft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace LocaCraft.ViewModels
{
    partial class NewLeaseViewModel : BaseViewModel
    {
        #region ATTRIBUTES
        #region ACTIONS
        public Action CloseAction { get; set; } = null!;
        #endregion

        private readonly RealEstateDataService _service = new RealEstateDataService();
        // Define any attributes needed for the NewLeaseViewModel here
        public int LeaseId { get; set; }
        public string LeaseDocumentPath { get; set; }

        [ObservableProperty]
        private float _monthlyRent;
        [ObservableProperty]
        private float _monthlyExpenses;
        [ObservableProperty]
        private float _deposit;
        [ObservableProperty]
        private DateTime _startDate;
        [ObservableProperty]
        private DateTime _endDate;

        public List<TenantModel>? Tenants { get; set; }
        public bool ShouldValidate { get; set; } = false;

        private RealEstateAssetModel _assetModel;
        private RealEstateDetailsViewModel _realEstateDetailsViewModel;
        #endregion

        #region CONSTRUCTOR
        public NewLeaseViewModel(RealEstateDetailsViewModel realEstateDetailsViewModel, RealEstateAssetModel assetModel)
        {
            _monthlyRent = 0.0f;
            _assetModel = assetModel;
            _realEstateDetailsViewModel = realEstateDetailsViewModel;
        }
        #endregion

        [RelayCommand]
        public async Task SaveNewLease()
        {
            LeaseModel newLease = new LeaseModel
            {
                MonthlyRent = MonthlyRent,
                MonthlyExpenses = MonthlyExpenses,
                Deposit = Deposit,
                StartDate = StartDate,
                EndDate = EndDate,
                Tenants = Tenants
            };
            _assetModel.AddNewLease(newLease);
            await _service.UpdateRealEstateAsset(_assetModel);
            CloseAction?.Invoke();
        }

        [RelayCommand]
        public void AskToClose()
        {
            if (MessageBox.Show("Are you sure you want to close?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                CloseAction?.Invoke();
            }
        }

        private void ForceValidation()
        {
            ShouldValidate = true;
            OnPropertyChanged(nameof(MonthlyRent));
            OnPropertyChanged(nameof(MonthlyExpenses));
            OnPropertyChanged(nameof(Deposit));
            OnPropertyChanged(nameof(StartDate));
            OnPropertyChanged(nameof(EndDate));
            OnPropertyChanged(nameof(Tenants));
        }
    }
}
