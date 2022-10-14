using System;
using System.Collections.Generic;

#nullable disable

namespace Final.models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CateId { get; set; }
        public int OriginalId { get; set; }
        public string CateName { get; set; }

        public virtual Original Original { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
