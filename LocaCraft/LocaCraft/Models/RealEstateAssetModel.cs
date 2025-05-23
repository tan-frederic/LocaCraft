﻿using System;
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
        public LeaseModel Lease { get; set; }
    }
}
