using LocaSuite.DataServices;
using LocaSuite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocaSuite.ViewModels
{
    class RealEstateDetailsViewModel
    {
        #region ATTRIBUTES
        public RealEstateAssetModel RealEstateAssetModel { get; private set; }

        private RealEstateDataService _service = new RealEstateDataService();

        #endregion

        #region CONSTRUCTOR
        public RealEstateDetailsViewModel(RealEstateAssetModel realEstateAssetModel)
        {
            RealEstateAssetModel = realEstateAssetModel;
        }
        #endregion
    }
}
