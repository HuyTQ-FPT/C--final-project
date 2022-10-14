using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Final.models;

namespace Final.Controller
{
    internal class ProductDAO
    {
        public List<Product> GetAll()
        {
            List<Product> Products;
            try
            {
                using prnSQLContext context = new prnSQLContext();
                context.Originals.ToList();
                context.Categories.ToList();
                Products = context.Products.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Products;
        }

        public List<String> getAllProductNameByCateID(int cateID)
        {
            try
            {
                using prnSQLContext context = new prnSQLContext();
                return context.Products.Where(x => x.CateId == cateID).Select(x => x.ProductName).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal void updateProductInfo(int cateId, string pName, int quan)
        {
            using prnSQLContext context = new prnSQLContext();
            Product p = new Product();
            try
            {
                p = context.Products.Where(x => x.CateId == cateId && x.ProductName == pName).FirstOrDefault();
            }catch{ }
            if (p == null)
            {
                p = new Product
                {
                    ProductName = pName,
                    CateId = cateId,
                    QuantityInStock = quan,
                };
                context.Products.Add(p);
                context.SaveChanges();
            }
            else
            {
                p.QuantityInStock += quan;
                context.Entry<Product>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }

        internal int getIdByNameAndCateId(int cateId, string pName)
        {
            using prnSQLContext context = new prnSQLContext();
            Product p = context.Products.Where(x => x.CateId == cateId && x.ProductName == pName).FirstOrDefault();
            return p.ProductId;
        }

        internal decimal getQuantityByNameAndCateId(int cateId, string pName)
        {
            using prnSQLContext context = new prnSQLContext();
            Product p = context.Products.Where(x => x.CateId == cateId && x.ProductName == pName).FirstOrDefault();
            if (p == null) return 0;
            return p.QuantityInStock;
        }

        internal List<Product> getAllProductbyOrignalName(string text)
        {
            using prnSQLContext context = new prnSQLContext();
            context.Originals.ToList();
            context.Categories.ToList();
            List<Product> products = context.Products.Where(x=> x.Cate.Original.OriginalName.Contains(text)).ToList();
            return products;
        }

        internal List<Product> getAllProductbyCateName(string text)
        {
            using prnSQLContext context = new prnSQLContext();
            context.Originals.ToList();
            context.Categories.ToList();
            List<Product> products = context.Products.Where(x => x.Cate.CateName.Contains(text)).ToList();
            return products;
        }

        internal List<Product> getAllProductbyProductName(string text)
        {
            using prnSQLContext context = new prnSQLContext();
            context.Originals.ToList();
            context.Categories.ToList();
            List<Product> products = context.Products.Where(x => x.ProductName.Contains(text)).ToList();
            return products;
        }
    }
}
