using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocaCraft.Models
{
    public enum RentalStatus
    {
        Rented,
        Empty,
        UnderMaintenance,
        Leaving
    }

    public class RealEstateAssetModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string AddressComplement { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<LeaseModel> Lease { get; set; }

        #region LEASE
        /// <summary>
        /// Adds a new lease to the asset's lease list. 
        /// Initializes the list if it is null.
        /// </summary>
        /// <param name="newLease">The lease to add to the asset's lease list.</param>
        public void AddNewLease(LeaseModel newLease)
        {
            if (Lease == null)
                Lease = new List<LeaseModel>();
            Lease.Add(newLease);
        }

        /// <summary>
        /// Removes a lease from the asset's lease list by matching LeaseId.
        /// Does nothing if the lease to remove or the lease list is null.
        /// </summary>
        /// <param name="leaseToRemove">The lease to remove from the asset's lease list.</param>
        public void RemoveLease(LeaseModel leaseToRemove)
        {
            if (leaseToRemove == null || Lease == null)
                return;
            Lease.RemoveAll(l => l.LeaseId == leaseToRemove.LeaseId);
        }

        /// <summary>
        /// Updates an existing lease in the asset's lease list by matching LeaseId.
        /// If the lease is found, its properties are updated with the values from the provided updatedLease.
        /// Does nothing if the updated lease or the lease list is null.
        /// </summary>
        /// <param name="updatedLease">The lease containing updated values to apply.</param>
        public void UpdateLease(LeaseModel updatedLease)
        {
            if (updatedLease == null || Lease == null)
                return;
            var existingLease = Lease.FirstOrDefault(l => l.LeaseId == updatedLease.LeaseId);
            if (existingLease != null)
            {
                existingLease.LeaseDocumentPath = updatedLease.LeaseDocumentPath;
                existingLease.MonthlyRent = updatedLease.MonthlyRent;
                existingLease.MonthlyExpenses = updatedLease.MonthlyExpenses;
                existingLease.Deposit = updatedLease.Deposit;
                existingLease.StartDate = updatedLease.StartDate;
                existingLease.EndDate = updatedLease.EndDate;
                existingLease.Tenants = updatedLease.Tenants;
            }
        }

        /// <summary>
        /// Determines whether the current instance has an active lease.
        /// </summary>
        public bool HasLease()
        {
            return Lease != null && Lease.Count > 0;
        }

        /// <summary>
        /// Determines whether the specified lease exists in the current collection of leases.
        /// </summary>
        /// <param name="lease">The lease to check for in the collection. Cannot be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the specified lease exists in the collection; otherwise, <see langword="false"/>.</returns>
        public bool HasThisLease(LeaseModel lease)
        {
            if (lease == null || Lease == null)
                return false;
            return Lease.Any(l => l.LeaseId == lease.LeaseId);
        }
        #endregion
    }
}
