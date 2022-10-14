using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.models;

namespace Final.Controller
{
    internal class CategoryDAO
    {
        public int getIdByCateNameAndOriginalId(string cateName, int orginID)
        {
            using prnSQLContext context = new prnSQLContext();
            Category cate= new Category();
            try
            {
                cate = context.Categories.Where(x => x.CateName == cateName && x.OriginalId == orginID).ToList().FirstOrDefault();
            }
            catch { }
            if (cate == null)
            {
                cate = new Category
                {
                    OriginalId = orginID,
                    CateName = cateName
                };
                context.Categories.Add(cate);
                context.SaveChanges();
            }
            try
            {
                return context.Categories.Where(x=> x.CateName==cateName && x.OriginalId==orginID).ToList().FirstOrDefault().CateId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<String> getAllCatenameByOriginalID(int originID)
        {
            try
            {
                using prnSQLContext context = new prnSQLContext();
                return context.Categories.Where(x=>x.OriginalId==originID).Select(x => x.CateName).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
