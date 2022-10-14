using System;
using System.Collections.Generic;

#nullable disable

namespace Final.models
{
    public partial class Product
    {
        public Product()
        {
            BillDetails = new HashSet<BillDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CateId { get; set; }
        public int QuantityInStock { get; set; }

        public virtual Category Cate { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
