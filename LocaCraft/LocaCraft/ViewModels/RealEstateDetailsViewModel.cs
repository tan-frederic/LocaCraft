using LocaCraft.Commands;
using LocaCraft.DataServices;
using LocaCraft.Models;
using LocaCraft.Services;
using System.Windows.Input;

namespace LocaCraft.ViewModels
{
    public partial class RealEstateDetailsViewModel : BaseViewModel
    {
        #region ATTRIBUTES
        
        public RealEstateAssetModel RealEstateAssetModel { get; private set; }

        private RealEstateDataService _service = new RealEstateDataService();

        public ICommand NavigateToHome { get; }
        #endregion

        #region CONSTRUCTOR
        public RealEstateDetailsViewModel(RealEstateAssetModel realEstateAssetModel, NavigationStore navigationStore)
        {
            RealEstateAssetModel = realEstateAssetModel;
            NavigateToHome = new CommandNavigation<RealEstateListViewModel>(new NavigationService<RealEstateListViewModel>(navigationStore, () => new RealEstateListViewModel(navigationStore)));
        }
        #endregion
    }
}
