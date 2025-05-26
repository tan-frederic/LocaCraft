using CommunityToolkit.Mvvm.ComponentModel;
using LocaCraft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocaCraft.ViewModels
{
    partial class NewLeaseViewModel : BaseViewModel
    {
        #region ATTRIBUTES
        // Define any attributes needed for the NewLeaseViewModel here
        public int LeaseId { get; set; }
        public string LeaseDocumentPath { get; set; }

        [ObservableProperty]
        private float _monthlyRent;
        [ObservableProperty]
        private float _monthlyExpenses;
        [ObservableProperty]
        private float _deposit;
        public List<TenantModel>? Tenants { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        #endregion

        #region CONSTRUCTOR
        public NewLeaseViewModel()
        {
            _monthlyRent = 0.0f;
        }
        #endregion

    }
}
