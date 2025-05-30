﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocaCraft.Models
{
    public class LeaseModel
    {
        public int LeaseId { get; set; }
        public string LeaseName { get; set; }
        public string LeaseDocumentPath { get; set; }
        public float MonthlyRent { get; set; }
        public float MonthlyExpenses { get; set; }
        public float Deposit { get; set; }
        public List<TenantModel>? Tenants { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
