using CommunityToolkit.Mvvm.ComponentModel;
using LocaSuite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocaSuite.ViewModels
{
    public class RealEstateCardItemViewModel : ObservableObject
    {
        #region ATTRIBUTES
        public RealEstateAssetModel RealEstateAssetModel { get; private set; }

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
    }
}
