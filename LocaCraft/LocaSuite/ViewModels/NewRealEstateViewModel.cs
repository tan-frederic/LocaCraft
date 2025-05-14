using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocaSuite.DataServices;
using LocaSuite.Models;
using System.ComponentModel;
using System.Windows;

namespace LocaSuite.ViewModels
{
    public partial class NewRealEstateViewModel : ObservableObject, IDataErrorInfo
    {
        #region ACTIONS
        public Action CloseAction { get; set; } = null!;
        #endregion


        #region VARIABLES
        private readonly RealEstateDataService _service = new RealEstateDataService();
        public RealEstateAssetModel NewAsset { get; } = new();

        public bool ShouldValidate { get; set; } = false;

        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _address;
        [ObservableProperty]
        private string _addressComplement;
        [ObservableProperty]
        private string _postalCode;
        [ObservableProperty]
        private string _city;


        #endregion

        #region DATA ERROR
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (!ShouldValidate)
                    return null;

                switch (columnName)
                {
                    case nameof(Name):
                        if (string.IsNullOrWhiteSpace(Name))
                            return "Name is required.";
                        break;
                    case nameof(Address):
                        if (string.IsNullOrWhiteSpace(Address))
                            return "Address is required.";
                        break;
                    case nameof(PostalCode):
                        if (string.IsNullOrWhiteSpace(PostalCode))
                            return "Postal code is required.";
                        if (PostalCode.Length != 5)
                            return "Postal code must be 5 digits.";
                        if (!int.TryParse(PostalCode, out _))
                            return "Postal code must be a number.";
                        break;
                    case nameof(City):
                        if (string.IsNullOrWhiteSpace(City))
                            return "City is required.";
                        break;
                    default:
                        return null; // No error for other properties
                }
                return null; // No error
            }
        }
        #endregion

        private void ForceValidation()
        {
            ShouldValidate = true;
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Address));
            OnPropertyChanged(nameof(PostalCode));
            OnPropertyChanged(nameof(City));
        }

        [RelayCommand]
        public async Task AddNewRealEstate()
        {
            ShouldValidate = true;
            RealEstateAssetModel newRealEstate = new RealEstateAssetModel()
            {
                Id = _service.GenerateRealEstateId(),
                Name = Name,
                Address = Address,
                AddressComplement = AddressComplement,
                PostalCode = PostalCode,
                City = City,
                Country = "France"
            };

            ForceValidation();
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Address) || string.IsNullOrWhiteSpace(PostalCode) ||
                string.IsNullOrWhiteSpace(City))
                return;

            await _service.AddRealEstateAsset(newRealEstate);
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
    }
}
