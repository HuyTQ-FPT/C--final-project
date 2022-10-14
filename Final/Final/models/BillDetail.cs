using System;
using System.Collections.Generic;

#nullable disable

namespace Final.models
{
    public partial class BillDetail
    {
        public int ProductId { get; set; }
        public int BillId { get; set; }
        public int QuantityUnit { get; set; }
        public int Tax { get; set; }

        public int Price { get { return Tax * QuantityUnit; } }
        public virtual Bill Bill { get; set; }
        public virtual Product Product { get; set; }
    }
}
