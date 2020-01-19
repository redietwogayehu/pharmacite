using System;
using System.Collections.Generic;

namespace PharmaCite_Pharmacy_Managment_System.Models
{
    public partial class Pharmacist
    {
        public Pharmacist()
        {
            MedicinesSold = new HashSet<MedicinesSold>();
        }

        public long PharmacistId { get; set; }
        public string PharmacistName { get; set; }
        public long HiredBy { get; set; }
        public DateTime HiredDate { get; set; }
        public string Pwd { get; set; }

        public virtual Manager HiredByNavigation { get; set; }
        public virtual ICollection<MedicinesSold> MedicinesSold { get; set; }
    }
}
