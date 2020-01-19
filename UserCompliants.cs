using System;
using System.Collections.Generic;

namespace PharmaCite_Pharmacy_Managment_System.Models
{
    public partial class UserCompliants
    {
        public long ComplaintId { get; set; }
        public long ComplaintBy { get; set; }
        public long ComplaintOn { get; set; }
        public DateTime ComplaintDate { get; set; }
        public string CompaintText { get; set; }

        public virtual Users ComplaintByNavigation { get; set; }
        public virtual MedicinesStored ComplaintOnNavigation { get; set; }
    }
}
