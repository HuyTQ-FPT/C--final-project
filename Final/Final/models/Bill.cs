using System;
using System.Collections.Generic;

#nullable disable

namespace Final.models
{
    public partial class Bill
    {
        public Bill()
        {
            BillDetails = new HashSet<BillDetail>();
        }

        public int BillId { get; set; }
        public string Username { get; set; }
        public DateTime? Date { get; set; }
        public Boolean Status { get; set; }

        public virtual Account UsernameNavigation { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
