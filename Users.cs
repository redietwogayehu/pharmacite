using System;
using System.Collections.Generic;

namespace PharmaCite_Pharmacy_Managment_System.Models
{
    public partial class Users
    {
        public Users()
        {
            MedicinesSold = new HashSet<MedicinesSold>();
            UserCompliants = new HashSet<UserCompliants>();
        }

        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public long Age { get; set; }
        public DateTime RegisteredDate { get; set; }

        public virtual ICollection<MedicinesSold> MedicinesSold { get; set; }
        public virtual ICollection<UserCompliants> UserCompliants { get; set; }
    }
}
