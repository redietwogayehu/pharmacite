using System;
using System.Collections.Generic;

namespace PharmaCite_Pharmacy_Managment_System.Models
{
    public partial class MedicinesSold
    {
        public long SoldId { get; set; }
        public long SoldBy { get; set; }
        public long SoldMedicine { get; set; }
        public DateTime SoldDate { get; set; }
        public long SoldTo { get; set; }

        public virtual Pharmacist SoldByNavigation { get; set; }
        public virtual MedicinesStored SoldMedicineNavigation { get; set; }
        public virtual Users SoldToNavigation { get; set; }
    }
}
