using System;
using System.Collections.Generic;

namespace PharmaCite_Pharmacy_Managment_System.Models
{
    public partial class Manager
    {
        public Manager()
        {
            Pharmacist = new HashSet<Pharmacist>();
        }

        public long ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string Pwd { get; set; }

        public virtual ICollection<Pharmacist> Pharmacist { get; set; }
    }
}
