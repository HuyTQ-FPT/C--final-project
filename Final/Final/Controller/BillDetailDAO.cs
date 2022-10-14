using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.models;

namespace Final.Controller
{
    internal class BillDetailDAO
    {
        internal void createNewBillDetail(int proId, int lastBillId, int quan, int tax)
        {
            using prnSQLContext context = new prnSQLContext();
            BillDetail bd = new BillDetail
            {
                ProductId = proId,
                BillId = lastBillId,
                QuantityUnit = quan,
                Tax = tax
            };
            context.BillDetails.Add(bd);
            context.SaveChanges();
        }

        internal List<BillDetail> getAllBillInOut(int v)
        {
            using prnSQLContext context = new prnSQLContext();
            context.Categories.ToList();
            context.Originals.ToList();
            context.Accounts.ToList();
            context.Products.ToList();
            context.Bills.ToList();
            List<BillDetail> list = context.BillDetails.Where(x => x.Bill.Status == (v == 1)).ToList();
            return list;
        }

        internal List<BillDetail> getAllBillInOut(string text, int v)
        {
            using prnSQLContext context = new prnSQLContext();
            context.Categories.ToList();
            context.Originals.ToList();
            context.Accounts.ToList();
            context.Products.ToList();
            context.Bills.ToList();
            List<BillDetail> list = context.BillDetails.Where(x => x.Bill.Status == (v == 1) && x.Bill.Username == text).ToList();
            return list;
        }

        internal List<BillDetail> getAllBillInOut(int v, DateTime date)
        {
            using prnSQLContext context = new prnSQLContext();
            context.Categories.ToList();
            context.Originals.ToList();
            context.Accounts.ToList();
            context.Products.ToList();
            context.Bills.ToList();
            List<BillDetail> list = context.BillDetails.Where(x => x.Bill.Status == (v == 1) && x.Bill.Date == date).ToList();
            return list;
        }

        public int totalPriceImOut(int v, int productId)
        {
            int price = 0;
            using prnSQLContext context = new prnSQLContext();
            List<BillDetail> list = context.BillDetails.Where(x => x.Bill.Status == (v == 1) && x.ProductId == productId).ToList();
            foreach (BillDetail bd in list)
            {
                price += bd.Price;
            }
            return price;
        }

        public string mostImOut(int v, int productId)
        {
            //using prnSQLContext context = new prnSQLContext();
            //return context.BillDetails.GroupBy(x => x.Bill.Username)
            //    .Select(x => x.FirstOrDefault(y => y.))
            return "";

        }
    }
}
