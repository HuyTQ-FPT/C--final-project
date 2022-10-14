using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.models;

namespace Final.Controller
{
    internal class BillDAO
    {
        internal void createNewBill(string username, int status)
        {
            using prnSQLContext context = new prnSQLContext();
            Bill b = new Bill
            {
                Username = username,
                Status = status==1,
                Date = DateTime.Now,
            };
            context.Bills.Add(b);
            context.SaveChanges();


        }

        internal int getLastBillId()
        {
            using prnSQLContext context = new prnSQLContext();
            return context.Bills.OrderBy(x => x.BillId).Last().BillId;
        }
    }
}
