using System;
using System.Collections.Generic;

#nullable disable

namespace Final.models
{
    public partial class Original
    {
        public Original()
        {
            Categories = new HashSet<Category>();
        }

        public int OriginalId { get; set; }
        public string OriginalName { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
