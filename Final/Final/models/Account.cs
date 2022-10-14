using System;
using System.Collections.Generic;

#nullable disable

namespace Final.models
{
    public partial class Account
    {
        public Account()
        {
            Bills = new HashSet<Bill>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Role { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
