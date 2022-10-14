using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.models;

namespace Final.Controller
{
    internal class OriginalDAO
    {
        public int getIdByOriginalName(string originalName)
        {
            using prnSQLContext context = new prnSQLContext();
            Original origin = new Original();
            try {
                origin = context.Originals.Where(x => x.OriginalName == originalName).ToList().FirstOrDefault();
            }
            catch { }
            if(origin == null)
            {
                origin = new Original
                {
                    OriginalName = originalName,
                };
                context.Originals.Add(origin);  
                context.SaveChanges();
            }
            try
            {
                
                return context.Originals.Where(x => x.OriginalName == originalName).ToList().FirstOrDefault().OriginalId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<string> getAllOriginalName()
        {
            try
            {
                using prnSQLContext context = new prnSQLContext();
                return context.Originals.Select(x=> x.OriginalName).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
